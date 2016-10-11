﻿namespace Nostradamus.Core2
{
    public abstract class SceneDesc : ActorDesc
    {

        public int SimulationDeltaTime;

        public int ReconciliationDeltaTime;

        protected internal override ISnapshotArgs InitSnapshot()
        {
            return new SceneSnapshot();
        }
    }
}
