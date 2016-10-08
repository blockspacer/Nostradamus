// automatically generated by the FlatBuffers compiler, do not modify

namespace Nostradamus.Schema
{

using System;
using FlatBuffers;

public struct Event : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static Event GetRootAsEvent(ByteBuffer _bb) { return GetRootAsEvent(_bb, new Event()); }
  public static Event GetRootAsEvent(ByteBuffer _bb, Event obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public Event __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public ActorId? ActorId { get { int o = __p.__offset(4); return o != 0 ? (ActorId?)(new ActorId()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  public MessageEnvelope? Args { get { int o = __p.__offset(6); return o != 0 ? (MessageEnvelope?)(new MessageEnvelope()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }

  public static Offset<Event> CreateEvent(FlatBufferBuilder builder,
      Offset<ActorId> actorIdOffset = default(Offset<ActorId>),
      Offset<MessageEnvelope> argsOffset = default(Offset<MessageEnvelope>)) {
    builder.StartObject(2);
    Event.AddArgs(builder, argsOffset);
    Event.AddActorId(builder, actorIdOffset);
    return Event.EndEvent(builder);
  }

  public static void StartEvent(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddActorId(FlatBufferBuilder builder, Offset<ActorId> actorIdOffset) { builder.AddOffset(0, actorIdOffset.Value, 0); }
  public static void AddArgs(FlatBufferBuilder builder, Offset<MessageEnvelope> argsOffset) { builder.AddOffset(1, argsOffset.Value, 0); }
  public static Offset<Event> EndEvent(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    builder.Required(o, 4);  // actorId
    builder.Required(o, 6);  // args
    return new Offset<Event>(o);
  }
};


}