using System;
using System.Collections.Generic;
using System.Linq;

namespace ET
{
    public static class WaitTypeError
    {
        public const int Success = 0;
        public const int Destroy = 1;
        public const int Cancel = 2;
        public const int Timeout = 3;
    }
    
    public interface IWaitType
    {
        int Error
        {
            get;
            set;
        }
    }

    [FriendClass(typeof(ObjectWait))]
    public static class ObjectWaitSystem
    {
        [ObjectSystem]
        public class ObjectWaitAwakeSystem: AwakeSystem<ObjectWait>
        {
            public override void Awake(ObjectWait me)
            {
                me.tcss.Clear();
            }
        }

        [ObjectSystem]
        public class ObjectWaitDestroySystem: DestroySystem<ObjectWait>
        {
            public override void Destroy(ObjectWait me)
            {
                foreach (object v in me.tcss.Values.ToArray())
                {
                    ((IDestroyRun) v).SetResult();
                }
            }
        }

        private interface IDestroyRun
        {
            void SetResult();
        }

        private class ResultCallback<K>: IDestroyRun where K : struct, IWaitType
        {
            private ETTask<K> tcs;

            public ResultCallback()
            {
                this.tcs = ETTask<K>.Create(true);
            }

            public bool IsDisposed
            {
                get
                {
                    return this.tcs == null;
                }
            }

            public ETTask<K> Task => this.tcs;

            public void SetResult(K k)
            {
                var t = tcs;
                this.tcs = null;
                t.SetResult(k);
            }

            public void SetResult()
            {
                var t = tcs;
                this.tcs = null;
                t.SetResult(new K() { Error = WaitTypeError.Destroy });
            }
        }
        
        public static async ETTask<T> Wait<T>(this ObjectWait me, ETCancellationToken cancellationToken = null) where T : struct, IWaitType
        {
            ResultCallback<T> tcs = new ResultCallback<T>();
            Type type = typeof (T);
            me.tcss.Add(type, tcs);

            void CancelAction()
            {
                me.Notify(new T() { Error = WaitTypeError.Cancel });
            }

            T ret;
            try
            {
                cancellationToken?.Add(CancelAction);
                ret = await tcs.Task;
            }
            finally
            {
                cancellationToken?.Remove(CancelAction);    
            }
            return ret;
        }

        public static async ETTask<T> Wait<T>(this ObjectWait me, int timeout, ETCancellationToken cancellationToken = null) where T : struct, IWaitType
        {
            ResultCallback<T> tcs = new ResultCallback<T>();
            async ETTask WaitTimeout()
            {
                bool retV = await TimerComponent.Instance.WaitAsync(timeout, cancellationToken);
                if (!retV)
                {
                    return;
                }
                if (tcs.IsDisposed)
                {
                    return;
                }
                me.Notify(new T() { Error = WaitTypeError.Timeout });
            }
            
            WaitTimeout().Coroutine();
            
            me.tcss.Add(typeof (T), tcs);
            
            void CancelAction()
            {
                me.Notify(new T() { Error = WaitTypeError.Cancel });
            }
            
            T ret;
            try
            {
                cancellationToken?.Add(CancelAction);
                ret = await tcs.Task;
            }
            finally
            {
                cancellationToken?.Remove(CancelAction);    
            }
            return ret;
        }

        public static void Notify<T>(this ObjectWait me, T obj) where T : struct, IWaitType
        {
            Type type = typeof (T);
            if (!me.tcss.TryGetValue(type, out object tcs))
            {
                return;
            }

            me.tcss.Remove(type);
            ((ResultCallback<T>) tcs).SetResult(obj);
        }
    }

    public class ObjectWait: Entity, IAwake, IDestroy
    {
        public Dictionary<Type, object> tcss = new Dictionary<Type, object>();
    }
}