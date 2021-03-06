﻿using Lidgren.Network;
using NLog;
using Nostradamus.Client;
using Nostradamus.Server;
using Nostradamus.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Nostradamus.Networking
{
    public sealed class ReliableUdpServer : Disposable
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly ServerSimulator simulator;
        private readonly NetServer server;
        private readonly SortedList<ClientId, Client> clients = new SortedList<ClientId, Client>();
        private bool stopRequest;

        public ReliableUdpServer(ServerSimulator simulator, int listeningPort, string appIdentifier = "Nostradamus")
        {
            this.simulator = simulator;

            var serverConf = new NetPeerConfiguration(appIdentifier);
            serverConf.Port = listeningPort;

            server = new NetServer(serverConf);
        }

        protected override void DisposeManaged()
        {
            server.Shutdown(string.Empty);

            simulator.Dispose();

            base.DisposeManaged();
        }

        public void Start()
        {
            if (server.Status != NetPeerStatus.NotRunning)
                throw new InvalidOperationException("Already started");

            var timer = Stopwatch.StartNew();

            server.Start();
        }

        public void Stop()
        {
            if (stopRequest)
                throw new InvalidOperationException("Already stopped");

            stopRequest = true;
        }

        public void Update()
        {
            for (var msg = server.ReadMessage(); msg != null; msg = server.ReadMessage())
                OnServerMessage(msg);

            simulator.Simulate();

            var deltaSyncFrame = simulator.DeltaSyncFrame;
            SendMessageToAll(deltaSyncFrame);

            logger.Debug("Simulate to {0}ms", simulator.Time);
        }

        private void OnServerMessage(NetIncomingMessage msg)
        {
            switch (msg.MessageType)
            {
                case NetIncomingMessageType.StatusChanged:
                    {
                        var status = (NetConnectionStatus)msg.ReadByte();

                        OnServerMessage_StatusChanged(msg, status);
                    }
                    break;

                case NetIncomingMessageType.Data:
                    {
                        var envelope = Serializer.Deserialize<MessageEnvelope>(msg.Data);

                        if (envelope.Message is CommandFrame)
                        {
                            OnServerMessage_CommandFrame(msg, (CommandFrame)envelope.Message);
                        }
                        else if (envelope.Message is Login)
                        {
                            OnServerMessage_Login(msg, (Login)envelope.Message);
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

        private void OnServerMessage_StatusChanged(NetIncomingMessage msg, NetConnectionStatus status)
        {
            logger.Trace("Status of {0} changed to {1}", msg.SenderConnection, status);
        }

        private void OnServerMessage_Login(NetIncomingMessage msg, Login message)
        {
            Client client;
            if (!clients.TryGetValue(message.ClientId, out client))
            {
                client = new Client(message.ClientId, msg.SenderConnection);
                clients.Add(message.ClientId, client);
            }
            else
                client.Connection = msg.SenderConnection;

            var frame = simulator.FullSyncFrame;
            SendMessage(client, frame);

            logger.Debug("Client {0} login", message.ClientId);
        }

        private void OnServerMessage_CommandFrame(NetIncomingMessage msg, CommandFrame frame)
        {
            simulator.ReceiveCommandFrame(frame);
        }

        private void SendMessage(Client client, object message)
        {
            var bytes = Serializer.Serialize(new MessageEnvelope(message));

            var msg = server.CreateMessage();
            msg.Write(bytes);

            client.Connection.SendMessage(msg, NetDeliveryMethod.ReliableOrdered, 0);
        }

        private void SendMessageToAll(object message)
        {
            var bytes = Serializer.Serialize(new MessageEnvelope(message));

            var msg = server.CreateMessage();
            msg.Write(bytes);

            foreach (var client in clients.Values)
            {
                client.Connection.SendMessage(msg, NetDeliveryMethod.ReliableOrdered, 0);
            }
        }

        class Client
        {
            public readonly ClientId Id;
            public NetConnection Connection;

            public Client(ClientId id, NetConnection connection)
            {
                Id = id;
                Connection = connection;
            }
        }
    }
}
