﻿using Nostradamus.Client;
using Nostradamus.Examples;
using Nostradamus.Physics;
using Nostradamus.Server;
using NUnit.Framework;

namespace Nostradamus.Tests
{
    public class ServerSimulatorTest
    {
        [Test]
        public void TestExampleScene()
        {
            var simulator = new ServerSimulator();
            simulator.RegisterActorFactory<ExampleSceneDesc, ExampleScene>(desc => new ExampleScene());
            simulator.RegisterActorFactory<BallDesc, Ball>(desc => new Ball());
            simulator.RegisterActorFactory<CubeDesc, Cube>(desc => new Cube());

            var sceneDesc = new ExampleSceneDesc(50, 50);
            var scene = simulator.CreateScene<ExampleScene>(sceneDesc);

            // Time: 0 - 50
            simulator.Simulate();

            var frame0 = simulator.DeltaSyncFrame;
            Assert.AreEqual(0, frame0.Time);
            Assert.AreEqual(50, frame0.DeltaTime);
            Assert.AreEqual(3, frame0.Events.Count);

            var ball50 = (RigidBodySnapshot)scene.Ball.Snapshot;
            AssertHelper.AreApproximate(-2.6f, ball50.Position.X);
            AssertHelper.AreApproximate(2.575475f, ball50.Position.Y);
            AssertHelper.AreApproximate(-2.6f, ball50.Position.Z);

            var cube50 = (RigidBodySnapshot)scene.Cube.Snapshot;
            AssertHelper.AreApproximate(1.1f, cube50.Position.X);
            AssertHelper.AreApproximate(1.07547498f, cube50.Position.Y);
            AssertHelper.AreApproximate(1.1f, cube50.Position.Z);

            var killBallCommand = new KickBallCommand(1, 0, 1);
            var clientId = new ClientId(1);
            var command = new Command(clientId, scene.Ball.Desc.Id, 1, 20, 20, killBallCommand);
            var clientFrame = new CommandFrame(clientId);
            clientFrame.Commands.Add(command);
            simulator.ReceiveCommandFrame(clientFrame);

            // Time: 50 - 100
            simulator.Simulate();

            var frame50 = simulator.DeltaSyncFrame;
            Assert.AreEqual(50, frame50.Time);
            Assert.AreEqual(50, frame50.DeltaTime);
            Assert.AreEqual(2, frame50.Events.Count);

            var ball100 = (RigidBodySnapshot)scene.Ball.Snapshot;
            AssertHelper.AreApproximate(-2.3499999f, ball100.Position.X);
            AssertHelper.AreApproximate(2.52669716f, ball100.Position.Y);
            AssertHelper.AreApproximate(-2.3499999f, ball100.Position.Z);

            var cube100 = (RigidBodySnapshot)scene.Cube.Snapshot;
            AssertHelper.AreApproximate(1.1f, cube100.Position.X);
            AssertHelper.AreApproximate(1.02669704f, cube100.Position.Y);
            AssertHelper.AreApproximate(1.1f, cube100.Position.Z);
        }
    }
}
