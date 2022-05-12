using System;

namespace ET
{
    [ObjectSystem]
    public class ActorMessageDispatcherComponentAwakeSystem: AwakeSystem<ActorMessageDispatcherComponent>
    {
        public override void Awake(ActorMessageDispatcherComponent me)
        {
            ActorMessageDispatcherComponent.Instance = me;
            me.Awake();
        }
    }

    [ObjectSystem]
    public class ActorMessageDispatcherComponentLoadSystem: LoadSystem<ActorMessageDispatcherComponent>
    {
        public override void Load(ActorMessageDispatcherComponent me)
        {
            me.Load();
        }
    }

    [ObjectSystem]
    public class ActorMessageDispatcherComponentDestroySystem: DestroySystem<ActorMessageDispatcherComponent>
    {
        public override void Destroy(ActorMessageDispatcherComponent me)
        {
            me.ActorMessageHandlers.Clear();
            ActorMessageDispatcherComponent.Instance = null;
        }
    }

    /// <summary>
    /// Actor消息分发组件
    /// </summary>
    [FriendClass(typeof(ActorMessageDispatcherComponent))]
    public static class ActorMessageDispatcherComponentHelper
    {
        public static void Awake(this ActorMessageDispatcherComponent me)
        {
            me.Load();
        }

        public static void Load(this ActorMessageDispatcherComponent me)
        {
            me.ActorMessageHandlers.Clear();

            var types = Game.EventSystem.GetTypes(typeof (ActorMessageHandlerAttribute));
            foreach (Type type in types)
            {
                object obj = Activator.CreateInstance(type);

                IMActorHandler imHandler = obj as IMActorHandler;
                if (imHandler == null)
                {
                    throw new Exception($"message handler not inherit IMActorHandler abstract class: {obj.GetType().FullName}");
                }

                Type messageType = imHandler.GetRequestType();
                
                Type handleResponseType = imHandler.GetResponseType();
                if (handleResponseType != null)
                {
                    Type responseType = OpcodeTypeComponent.Instance.GetResponseType(messageType);
                    if (handleResponseType != responseType)
                    {
                        throw new Exception($"message handler response type error: {messageType.FullName}");
                    }
                }

                me.ActorMessageHandlers.Add(messageType, imHandler);
            }
        }

        /// <summary>
        /// 分发actor消息
        /// </summary>
        public static async ETTask Handle(
            this ActorMessageDispatcherComponent me, Entity entity, object message, Action<IActorResponse> reply)
        {
            if (!me.ActorMessageHandlers.TryGetValue(message.GetType(), out IMActorHandler handler))
            {
                throw new Exception($"not found message handler: {message}");
            }

            await handler.Handle(entity, message, reply);
        }

        public static bool TryGetHandler(this ActorMessageDispatcherComponent me,Type type, out IMActorHandler actorHandler)
        {
            return me.ActorMessageHandlers.TryGetValue(type, out actorHandler);
        }
    }
}