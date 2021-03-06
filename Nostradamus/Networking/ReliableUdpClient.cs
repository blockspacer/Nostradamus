﻿using FlatBuffers.Schema;
using Lidgren.Network;
using NLog;
using Nostradamus.Client;
using Nostradamus.Server;
using Nostradamus.Utils;
using System;
using System.Diagnostics;
using System.Net;

namespace Nostradamus.Networking
{
    public class ReliableUdpClient : Disposable
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly ClientSimulator simulator;
        private readonly IPEndPoint serverEndPoint;
        private readonly NetClient client;
        private bool isStarted;
        private bool hasStopRequest;

        public ReliableUdpClient(ClientSimulator simulator, IPEndPoint serverEndPoint, string appIdentifier = "Nostradamus", int simulateLatency = 0, int simulateLoss = 0)
        {
            this.simulator = simulator;
            this.serverEndPoint = serverEndPoint;

            var clientConf = new NetPeerConfiguration(appIdentifier);
            clientConf.AutoFlushSendQueue = true;
            clientConf.SimulatedRandomLatency = simulateLatency;
            clientConf.SimulatedLoss = simulateLoss;

            client = new NetClient(clientConf);
        }

        protected override void DisposeManaged()
        {
            client.Shutdown(string.Empty);

            simulator.Dispose();

            base.DisposeManaged();
        }

        public void Start()
        {
            if (client.Status != NetPeerStatus.NotRunning)
                throw new InvalidOperationException("Already started");

            var timer = Stopwatch.StartNew();

            client.Start();
            client.Connect(serverEndPoint);
        }

        public void Stop()
        {
            if (hasStopRequest)
                throw new InvalidOperationException("Already stopped");

            hasStopRequest = true;
        }

        public void Update()
        {
            if (hasStopRequest)
                return;

            for (var msg = client.ReadMessage(); msg != null; msg = client.ReadMessage())
                OnClientMessage(msg);

            if (isStarted)
            {
                simulator.Simulate();

                var commandFrame = simulator.CommandFrame;
                if (commandFrame != null)
                    SendMessage(commandFrame);
            }
        }

        private void OnClientMessage(NetIncomingMessage msg)
        {
            switch (msg.MessageType)
            {
                case NetIncomingMessageType.StatusChanged:
                    {
                        var status = (NetConnectionStatus)msg.ReadByte();

                        OnClientMessage_StatusChanged(msg, status);
                    }
                    break;

                case NetIncomingMessageType.Data:
                    {
                        var envelope = (MessageEnvelope)SerializerSet.Instance.Deserialize(typeof(MessageEnvelope), msg.Data);

                        if (envelope.Message is DeltaSyncFrame)
                        {
                            OnServerMessage_DeltaSyncFrame(msg, (DeltaSyncFrame)envelope.Message);
                        }
                        else if (envelope.Message is FullSyncFrame)
                        {
                            OnServerMessage_FullSyncFrame(msg, (FullSyncFrame)envelope.Message);
                        }
                        else
                            logger.Error("Unexpected message: {0}", envelope.Message);
                    }
                    break;

                case NetIncomingMessageType.DebugMessage:
                    logger.Debug(msg.ReadString());
                    break;

                case NetIncomingMessageType.VerboseDebugMessage:
                    logger.Info(msg.ReadString());
                    break;

                case NetIncomingMessageType.WarningMessage:
                    logger.Warn(msg.ReadString());
                    break;

                case NetIncomingMessageType.ErrorMessage:
                    logger.Error(msg.ReadString());
                    break;

                default:
                    logger.Trace("Unhandled message '{0}'", msg);
                    break;
            }
        }

        private void OnServerMessage_DeltaSyncFrame(NetIncomingMessage msg, DeltaSyncFrame message)
        {
            simulator.ReceiveDeltaSyncFrame(message);
        }

        private void OnServerMessage_FullSyncFrame(NetIncomingMessage msg, FullSyncFrame message)
        {
            simulator.ReceiveFullSyncFrame(message);

            isStarted = true;
        }

        private void OnClientMessage_StatusChanged(NetIncomingMessage msg, NetConnectionStatus status)
        {
            logger.Trace("Status of {0} changed to {1}", msg.SenderConnection, status);

            switch (status)
            {
                case NetConnectionStatus.Connected:
                    SendMessage(new Login(simulator.ClientId));
                    break;

                case NetConnectionStatus.Disconnected:
                    Stop();
                    break;
            }
        }

        private void SendMessage(object message)
        {
            var bytes = SerializerSet.Instance.Serialize(typeof(MessageEnvelope), new MessageEnvelope(message));

            var msg = client.CreateMessage();
            msg.Write(bytes);

            client.SendMessage(msg, NetDeliveryMethod.ReliableOrdered, 0);
        }
    }
}
