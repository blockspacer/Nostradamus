// automatically generated by the FlatBuffers compiler, do not modify

namespace Nostradamus.Schema
{

using System;
using FlatBuffers;

public struct Command : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static Command GetRootAsCommand(ByteBuffer _bb) { return GetRootAsCommand(_bb, new Command()); }
  public static Command GetRootAsCommand(ByteBuffer _bb, Command obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public Command __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public ClientId? ClientId { get { int o = __p.__offset(4); return o != 0 ? (ClientId?)(new ClientId()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  public ActorId? ActorId { get { int o = __p.__offset(6); return o != 0 ? (ActorId?)(new ActorId()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  public int Sequence { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Time { get { int o = __p.__offset(10); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int DeltaTime { get { int o = __p.__offset(12); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public MessageEnvelope? Args { get { int o = __p.__offset(14); return o != 0 ? (MessageEnvelope?)(new MessageEnvelope()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }

  public static Offset<Command> CreateCommand(FlatBufferBuilder builder,
      Offset<ClientId> clientIdOffset = default(Offset<ClientId>),
      Offset<ActorId> actorIdOffset = default(Offset<ActorId>),
      int sequence = 0,
      int time = 0,
      int deltaTime = 0,
      Offset<MessageEnvelope> argsOffset = default(Offset<MessageEnvelope>)) {
    builder.StartObject(6);
    Command.AddArgs(builder, argsOffset);
    Command.AddDeltaTime(builder, deltaTime);
    Command.AddTime(builder, time);
    Command.AddSequence(builder, sequence);
    Command.AddActorId(builder, actorIdOffset);
    Command.AddClientId(builder, clientIdOffset);
    return Command.EndCommand(builder);
  }

  public static void StartCommand(FlatBufferBuilder builder) { builder.StartObject(6); }
  public static void AddClientId(FlatBufferBuilder builder, Offset<ClientId> clientIdOffset) { builder.AddOffset(0, clientIdOffset.Value, 0); }
  public static void AddActorId(FlatBufferBuilder builder, Offset<ActorId> actorIdOffset) { builder.AddOffset(1, actorIdOffset.Value, 0); }
  public static void AddSequence(FlatBufferBuilder builder, int sequence) { builder.AddInt(2, sequence, 0); }
  public static void AddTime(FlatBufferBuilder builder, int time) { builder.AddInt(3, time, 0); }
  public static void AddDeltaTime(FlatBufferBuilder builder, int deltaTime) { builder.AddInt(4, deltaTime, 0); }
  public static void AddArgs(FlatBufferBuilder builder, Offset<MessageEnvelope> argsOffset) { builder.AddOffset(5, argsOffset.Value, 0); }
  public static Offset<Command> EndCommand(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    builder.Required(o, 4);  // clientId
    builder.Required(o, 6);  // actorId
    builder.Required(o, 14);  // args
    return new Offset<Command>(o);
  }
};


}
