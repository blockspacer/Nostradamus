﻿using BulletSharp;
using BulletSharp.Math;
using Nostradamus.Client;
using Nostradamus.Physics;
using Nostradamus.Server;

namespace Nostradamus.Examples
{
    public class ExampleSceneDesc : PhysicsSceneDesc
    {
        public ExampleSceneDesc(int simulationDeltaTime, int reconciliationDeltaTime)
            : base(new ActorId(1), simulationDeltaTime, reconciliationDeltaTime, 10000, 0.01f, DefaultGravity, CreateColliders())
        { }

        private static SceneColliderDesc[] CreateColliders()
        {
            var ground = new SceneColliderDesc()
            {
                Shape = new BoxShape(500, 1, 500),
                Transform = Matrix.Translation(0, -1, 0),
            };

            return new[] { ground };
        }
    }

    public class ExampleScene : PhysicsScene
    {
        private Cube cube;
        private Ball ball;

        protected internal override void OnUpdate()
        {
            if (cube == null && ball == null && Simulator is ServerSimulator)
            {
                ApplyEvent(new SceneInitializedEvent(1, 1));
            }

            base.OnUpdate();
        }

        protected internal override void OnLateUpdate()
        {
            base.OnLateUpdate();

            if (Simulator is ClientSimulator)
            {
                var stats = Simulator.StatsFrames.Current.AddStats<ExampleSceneStats>();
                if (ball != null)
                {
                    stats.BallPosition = ((RigidBodySnapshot)ball.Snapshot).Position;
                    stats.BallRigidBodyPosition = ball.RigidBody.CenterOfMassPosition;
                }
            }
        }

        protected override void OnEventApplied(IEventArgs @event)
        {
            if (@event is SceneInitializedEvent)
            {
                var s = (SceneSnapshot)Snapshot.Clone();

                var cubeId = new ActorId(2);
                var cubeDesc = new CubeDesc(cubeId, new Vector3(1.1f, 1.1f, 1.1f));
                cube = Context.CreateActor<Cube>(cubeDesc);
                AddActor(s, cube);

                var ballId = new ActorId(3);
                var ballDesc = new BallDesc(ballId, new Vector3(-2.6f, 2.6f, -2.6f));
                ball = Context.CreateActor<Ball>(ballDesc);
                AddActor(s, ball);

                Snapshot = s;
            }
            else
                base.OnEventApplied(@event);
        }

        public Cube Cube
        {
            get
            {
                if (cube == null)
                    cube = Simulator.GetActor(new ActorId(2)) as Cube;

                return cube;
            }
        }

        public Ball Ball
        {
            get
            {
                if (ball == null)
                    ball = Simulator.GetActor(new ActorId(3)) as Ball;

                return ball;
            }
        }
    }
}
