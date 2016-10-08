// automatically generated by the FlatBuffers compiler, do not modify

namespace Nostradamus.Schema
{

using System;
using FlatBuffers;

public struct ActorId : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static ActorId GetRootAsActorId(ByteBuffer _bb) { return GetRootAsActorId(_bb, new ActorId()); }
  public static ActorId GetRootAsActorId(ByteBuffer _bb, ActorId obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public ActorId __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int Value { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string Description { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
  public ArraySegment<byte>? GetDescriptionBytes() { return __p.__vector_as_arraysegment(6); }

  public static Offset<ActorId> CreateActorId(FlatBufferBuilder builder,
      int value = 0,
      StringOffset descriptionOffset = default(StringOffset)) {
    builder.StartObject(2);
    ActorId.AddDescription(builder, descriptionOffset);
    ActorId.AddValue(builder, value);
    return ActorId.EndActorId(builder);
  }

  public static void StartActorId(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddValue(FlatBufferBuilder builder, int value) { builder.AddInt(0, value, 0); }
  public static void AddDescription(FlatBufferBuilder builder, StringOffset descriptionOffset) { builder.AddOffset(1, descriptionOffset.Value, 0); }
  public static Offset<ActorId> EndActorId(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<ActorId>(o);
  }
};


}