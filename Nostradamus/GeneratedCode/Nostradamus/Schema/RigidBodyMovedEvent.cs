// automatically generated by the FlatBuffers compiler, do not modify

namespace Nostradamus.Schema
{

using System;
using FlatBuffers;

public struct RigidBodyMovedEvent : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static RigidBodyMovedEvent GetRootAsRigidBodyMovedEvent(ByteBuffer _bb) { return GetRootAsRigidBodyMovedEvent(_bb, new RigidBodyMovedEvent()); }
  public static RigidBodyMovedEvent GetRootAsRigidBodyMovedEvent(ByteBuffer _bb, RigidBodyMovedEvent obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public RigidBodyMovedEvent __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Vector3? Position { get { int o = __p.__offset(4); return o != 0 ? (Vector3?)(new Vector3()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  public Quaternion? Rotation { get { int o = __p.__offset(6); return o != 0 ? (Quaternion?)(new Quaternion()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  public Vector3? LinearVelocity { get { int o = __p.__offset(8); return o != 0 ? (Vector3?)(new Vector3()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  public Vector3? AngularVelocity { get { int o = __p.__offset(10); return o != 0 ? (Vector3?)(new Vector3()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }

  public static Offset<RigidBodyMovedEvent> CreateRigidBodyMovedEvent(FlatBufferBuilder builder,
      Offset<Vector3> positionOffset = default(Offset<Vector3>),
      Offset<Quaternion> rotationOffset = default(Offset<Quaternion>),
      Offset<Vector3> linearVelocityOffset = default(Offset<Vector3>),
      Offset<Vector3> angularVelocityOffset = default(Offset<Vector3>)) {
    builder.StartObject(4);
    RigidBodyMovedEvent.AddAngularVelocity(builder, angularVelocityOffset);
    RigidBodyMovedEvent.AddLinearVelocity(builder, linearVelocityOffset);
    RigidBodyMovedEvent.AddRotation(builder, rotationOffset);
    RigidBodyMovedEvent.AddPosition(builder, positionOffset);
    return RigidBodyMovedEvent.EndRigidBodyMovedEvent(builder);
  }

  public static void StartRigidBodyMovedEvent(FlatBufferBuilder builder) { builder.StartObject(4); }
  public static void AddPosition(FlatBufferBuilder builder, Offset<Vector3> positionOffset) { builder.AddOffset(0, positionOffset.Value, 0); }
  public static void AddRotation(FlatBufferBuilder builder, Offset<Quaternion> rotationOffset) { builder.AddOffset(1, rotationOffset.Value, 0); }
  public static void AddLinearVelocity(FlatBufferBuilder builder, Offset<Vector3> linearVelocityOffset) { builder.AddOffset(2, linearVelocityOffset.Value, 0); }
  public static void AddAngularVelocity(FlatBufferBuilder builder, Offset<Vector3> angularVelocityOffset) { builder.AddOffset(3, angularVelocityOffset.Value, 0); }
  public static Offset<RigidBodyMovedEvent> EndRigidBodyMovedEvent(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<RigidBodyMovedEvent>(o);
  }
};


}