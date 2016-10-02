﻿using BulletSharp.Math;
using ProtoBuf;

namespace Nostradamus.Tests.Commnads
{
	[ProtoContract]
	public class MoveBallCommand : ICommandArgs
	{
		[ProtoMember(1)]
		public Vector3 InputAxis { get; set; }
	}
}
