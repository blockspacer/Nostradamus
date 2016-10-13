// automatically generated by the FlatBuffers compiler, do not modify

namespace Nostradamus.Schema
{

using System;
using FlatBuffers;

public struct BallDesc : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static BallDesc GetRootAsBallDesc(ByteBuffer _bb) { return GetRootAsBallDesc(_bb, new BallDesc()); }
  public static BallDesc GetRootAsBallDesc(ByteBuffer _bb, BallDesc obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public BallDesc __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public ActorId? Id { get { int o = __p.__offset(4); return o != 0 ? (ActorId?)(new ActorId()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  public Vector3? InitialPosition { get { int o = __p.__offset(6); return o != 0 ? (Vector3?)(new Vector3()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }

  public static Offset<BallDesc> CreateBallDesc(FlatBufferBuilder builder,
      Offset<ActorId> idOffset = default(Offset<ActorId>),
      Offset<Vector3> initialPositionOffset = default(Offset<Vector3>)) {
    builder.StartObject(2);
    BallDesc.AddInitialPosition(builder, initialPositionOffset);
    BallDesc.AddId(builder, idOffset);
    return BallDesc.EndBallDesc(builder);
  }

  public static void StartBallDesc(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddId(FlatBufferBuilder builder, Offset<ActorId> idOffset) { builder.AddOffset(0, idOffset.Value, 0); }
  public static void AddInitialPosition(FlatBufferBuilder builder, Offset<Vector3> initialPositionOffset) { builder.AddOffset(1, initialPositionOffset.Value, 0); }
  public static Offset<BallDesc> EndBallDesc(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    builder.Required(o, 4);  // id
    builder.Required(o, 6);  // initialPosition
    return new Offset<BallDesc>(o);
  }
};


}