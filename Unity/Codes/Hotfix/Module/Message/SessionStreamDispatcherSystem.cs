using System;
using System.Collections.Generic;
using System.IO;

namespace ET
{
    [ObjectSystem]
    public class SessionStreamDispatcherAwakeSystem: AwakeSystem<SessionStreamDispatcher>
    {
        public override void Awake(SessionStreamDispatcher me)
        {
            SessionStreamDispatcher.Instance = me;
            me.Load();
        }
    }

    [ObjectSystem]
    public class SessionStreamDispatcherLoadSystem: LoadSystem<SessionStreamDispatcher>
    {
        public override void Load(SessionStreamDispatcher me)
        {
            me.Load();
        }
    }

    [ObjectSystem]
    public class SessionStreamDispatcherDestroySystem: DestroySystem<SessionStreamDispatcher>
    {
        public override void Destroy(SessionStreamDispatcher me)
        {
            SessionStreamDispatcher.Instance = null;
        }
    }
    
    [FriendClass(typeof(SessionStreamDispatcher))]
    public static class SessionStreamDispatcherSystem
    {
        public static void Load(this SessionStreamDispatcher me)
        {
            me.Dispatchers = new ISessionStreamDispatcher[100];
            
            List<Type> types = Game.EventSystem.GetTypes(typeof (SessionStreamDispatcherAttribute));

            foreach (Type type in types)
            {
                object[] attrs = type.GetCustomAttributes(typeof (SessionStreamDispatcherAttribute), false);
                if (attrs.Length == 0)
                {
                    continue;
                }
                
                SessionStreamDispatcherAttribute sessionStreamDispatcherAttribute = attrs[0] as SessionStreamDispatcherAttribute;
                if (sessionStreamDispatcherAttribute == null)
                {
                    continue;
                }

                if (sessionStreamDispatcherAttribute.Type >= 100)
                {
                    Log.Error("session dispatcher type must < 100");
                    continue;
                }
                
                ISessionStreamDispatcher iSessionStreamDispatcher = Activator.CreateInstance(type) as ISessionStreamDispatcher;
                if (iSessionStreamDispatcher == null)
                {
                    Log.Error($"sessionDispatcher {type.Name} 需要继承 ISessionDispatcher");
                    continue;
                }

                me.Dispatchers[sessionStreamDispatcherAttribute.Type] = iSessionStreamDispatcher;
            }
        }

        public static void Dispatch(this SessionStreamDispatcher me, int type, Session session, MemoryStream memoryStream)
        {
            ISessionStreamDispatcher sessionStreamDispatcher = me.Dispatchers[type];
            if (sessionStreamDispatcher == null)
            {
                throw new Exception("maybe your NetInnerComponent or NetOuterComponent not set SessionStreamDispatcherType");
            }
            sessionStreamDispatcher.Dispatch(session, memoryStream);
        }
    }
}