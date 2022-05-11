using System;
using System.Collections.Generic;

namespace ET
{
    [ObjectSystem]
    public class MessageDispatcherComponentAwakeSystem: AwakeSystem<MessageDispatcherComponent>
    {
        public override void Awake(MessageDispatcherComponent me)
        {
            MessageDispatcherComponent.Instance = me;
            me.Load();
        }
    }

    [ObjectSystem]
    public class MessageDispatcherComponentLoadSystem: LoadSystem<MessageDispatcherComponent>
    {
        public override void Load(MessageDispatcherComponent me)
        {
            me.Load();
        }
    }

    [ObjectSystem]
    public class MessageDispatcherComponentDestroySystem: DestroySystem<MessageDispatcherComponent>
    {
        public override void Destroy(MessageDispatcherComponent me)
        {
            MessageDispatcherComponent.Instance = null;
            me.Handlers.Clear();
        }
    }

    /// <summary>
    /// 消息分发组件
    /// </summary>
    [FriendClass(typeof(MessageDispatcherComponent))]
    public static class MessageDispatcherComponentHelper
    {
        public static void Load(this MessageDispatcherComponent me)
        {
            me.Handlers.Clear();

            List<Type> types = Game.EventSystem.GetTypes(typeof (MessageHandlerAttribute));

            foreach (Type type in types)
            {
                IMHandler iMHandler = Activator.CreateInstance(type) as IMHandler;
                if (iMHandler == null)
                {
                    Log.Error($"message handle {type.Name} 需要继承 IMHandler");
                    continue;
                }

                Type messageType = iMHandler.GetMessageType();
                ushort opcode = OpcodeTypeComponent.Instance.GetOpcode(messageType);
                if (opcode == 0)
                {
                    Log.Error($"消息opcode为0: {messageType.Name}");
                    continue;
                }

                me.RegisterHandler(opcode, iMHandler);
            }
        }

        public static void RegisterHandler(this MessageDispatcherComponent me, ushort opcode, IMHandler handler)
        {
            if (!me.Handlers.ContainsKey(opcode))
            {
                me.Handlers.Add(opcode, new List<IMHandler>());
            }

            me.Handlers[opcode].Add(handler);
        }

        public static void Handle(this MessageDispatcherComponent me, Session session, ushort opcode, object message)
        {
            List<IMHandler> actions;
            if (!me.Handlers.TryGetValue(opcode, out actions))
            {
                Log.Error($"消息没有处理: {opcode} {message}");
                return;
            }

            foreach (IMHandler ev in actions)
            {
                try
                {
                    ev.Handle(session, message);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }
    }
}