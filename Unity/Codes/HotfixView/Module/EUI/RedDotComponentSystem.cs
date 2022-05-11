using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    [ObjectSystem]
    public class RedDotComponentAwakeSystem: AwakeSystem<RedDotComponent>
    {
        public override void Awake(RedDotComponent me)
        {
           Game.Scene.GetComponent<ResourcesComponent>().LoadBundleAsync("RedDot".StringToAB()).Coroutine();
        }
    }

    [ObjectSystem]
    public class RedDotComponentDestroySystem: DestroySystem<RedDotComponent>
    {
        public override void Destroy(RedDotComponent me)
        {
            foreach (var List in me.RedDotNodeParentsDict.Values)
            {
                List.Dispose();
            }
            me.RedDotNodeParentsDict.Clear();
            me.ToParentDict.Clear();
            me.RedDotNodeRetainCount.Clear();
            me.RedDotMonoViewDict.Clear();
            me.RedDotNodeNeedShowSet.Clear();
            me.RetainViewCount.Clear();
        }
    }

    [FriendClass(typeof(RedDotComponent))]
    public static class RedDotComponentSystem
    {
        public static void AddRedDotNode(this RedDotComponent me, string parent, string target, bool isNeedShowNum)
        {
            if (string.IsNullOrEmpty(target))
            {
                Log.Error($"target is null");
                return;
            }
            
            if (string.IsNullOrEmpty(parent))
            {
                Log.Error($"parent is null");
                return;
            }

            if (me.ToParentDict.ContainsKey(target))
            {
                Log.Error($"{target} is already exist!");
                return;
            }

            me.ToParentDict.Add(target, parent);

            if ( !me.RedDotNodeRetainCount.ContainsKey(target) )
            {
                me.RedDotNodeRetainCount.Add(target, 0);
            }

            if (!me.RedDotNodeNeedShowSet.Contains(parent) && isNeedShowNum)
            {
                me.RedDotNodeNeedShowSet.Add(parent);
            } 
            
            if (!me.RetainViewCount.ContainsKey(target))
            {
                me.RetainViewCount.Add(target, 0);
            }
            
            if (!me.RedDotNodeRetainCount.ContainsKey(parent))
            {
                me.RedDotNodeRetainCount.Add(parent, 0);
            }

            if (me.RedDotNodeParentsDict.TryGetValue(parent, out ListComponent<string> list))
            {
                list.Add(target);
                return;
            }

            var listComponent = ListComponent<string>.Create();
            listComponent.Add(target);
            me.RedDotNodeParentsDict.Add(parent, listComponent);
        }

        public static void  RemoveRedDotNode(this RedDotComponent me, string target)
        {
            if (!me.ToParentDict.TryGetValue(target, out string parent))
            {
                return ;
            }

            if (!me.IsLeafNode(target))
            {
                Log.Error("can not remove parent node!");
                return ;
            }

            me.UpdateLogicNodeRetainCount(target, false);
            me.ToParentDict.Remove(target);
            if (!string.IsNullOrEmpty(parent))
            {
                me.RedDotNodeParentsDict[parent].Remove(target);
                if ( me.RedDotNodeParentsDict[parent].Count <= 0 )
                {
                    me.RedDotNodeParentsDict[parent].Dispose();
                    me.RedDotNodeParentsDict.Remove(parent);
                    me.RedDotNodeNeedShowSet.Remove(parent);
                }
            }
            me.RedDotNodeRetainCount.Remove(target);
        }

        public static void AddRedDotView(this RedDotComponent me, string target, RedDotMonoView monoView)
        {
            if (!me.RedDotNodeRetainCount.TryGetValue(target, out int retainCount))
            {
                Log.Error("redDot Node never added :" + target);
                return;
            }

            me.RedDotMonoViewDict[target] = monoView;

            if (retainCount == 0)
            {
                return;
            }
            monoView.Show(me.GetORedDotGameObjectFromPool());
        }
        
        public static void RemoveRedDotView(this RedDotComponent me, string target, out RedDotMonoView monoView)
        {
            if (me.RedDotMonoViewDict.TryGetValue(target, out monoView))
            {
                me.RedDotMonoViewDict.Remove(target);
            }

            if (monoView == null || !monoView.isRedDotActive)
            {
                return;
            }
            me.RecycleRedDotGameObject(monoView.Recovery());
        }
        
        public static bool IsLeafNode(this RedDotComponent me, string target)
        {
            return !me.RedDotNodeParentsDict.ContainsKey(target);
        }

        public static bool HideRedDotNode(this RedDotComponent me, string target)
        {
            if (!me.IsLeafNode(target))
            {
                Log.Error("can not hide parent node!"+target);
                return false;
            }

            me.UpdateLogicNodeRetainCount(target, false);
            return true;
        }

        public static bool ShowRedDotNode(this RedDotComponent me, string target)
        {
            if (!me.IsLeafNode(target))
            {
                Log.Error("can not show parent node : " + target);
                return false;
            }

            me.UpdateLogicNodeRetainCount(target);
            return true;
        }
        
        private static void UpdateLogicNodeRetainCount(this RedDotComponent me, string target, bool isRaiseRetainCount = true)
        {
            if (!me.RedDotNodeRetainCount.ContainsKey(target))
            {
                Log.Error($"redDot logic node {target} is not exist!");
                return;
            }
            
            if (!me.IsLeafNode(target))
            {
                Log.Error($"redDot logic node {target} is not leaf node!");
                return;
            }

            if (isRaiseRetainCount)
            {
                if (me.RedDotNodeRetainCount[target] == 1)
                {
                    Log.Error($"redDot logic node {target} RetainCount is already one!");
                    return;
                }

                me.RedDotNodeRetainCount[target] += 1;
                if (me.RedDotNodeRetainCount[target] != 1)
                {
                    Log.Error($"redDot logic node {target} RetainCount is {me.RedDotNodeRetainCount[target]}, number error!");
                    return;
                }
            }
            else
            {
                if (me.RedDotNodeRetainCount[target] != 1)
                {
                    Log.Error($"redDot logic node {target} is not show status, RetainCount is {me.RedDotNodeRetainCount[target]}");
                    return;
                }
                me.RedDotNodeRetainCount[target] += -1;
            }
            
            int curr = me.RedDotNodeRetainCount[target];

            if ( curr < 0 || curr > 1 )
            {
                Log.Error("count is error, redDot node is logic error!");
                return;
            }
            
            if (me.RedDotMonoViewDict.TryGetValue(target, out RedDotMonoView redDotMonoView))
            {
                if (isRaiseRetainCount)
                {
                    redDotMonoView.Show(me.GetORedDotGameObjectFromPool());
                }
                else
                {
                    me.RecycleRedDotGameObject(redDotMonoView.Recovery());
                }
            }
            bool isParentExist = me.ToParentDict.TryGetValue(target, out string parent);
            while (isParentExist)
            {
                me.RedDotNodeRetainCount[parent] += isRaiseRetainCount ?  1 : -1;
                
                if (me.RedDotNodeRetainCount[parent] >= 1 && isRaiseRetainCount )
                {
                    if (me.RedDotMonoViewDict.TryGetValue(parent, out redDotMonoView))
                    {
                        if (!redDotMonoView.isRedDotActive)
                        {
                            redDotMonoView.Show(me.GetORedDotGameObjectFromPool());
                        }
                    }
                }
                
                if (me.RedDotNodeRetainCount[parent] == 0 && !isRaiseRetainCount )
                {
                    if (me.RedDotMonoViewDict.TryGetValue(parent, out redDotMonoView))
                    {
                        me.RecycleRedDotGameObject(redDotMonoView.Recovery());
                    }
                }
                isParentExist = me.ToParentDict.TryGetValue(parent, out parent);
            }
        }

        public static void RefreshRedDotViewCount(this RedDotComponent me, string target, int Count)
        {
            if (!me.IsLeafNode(target))
            {
                Log.Error("can not refresh parent node view count");
                return;
            }
            
            me.RedDotMonoViewDict.TryGetValue(target, out RedDotMonoView redDotMonoView);

            me.RetainViewCount[target] = Count;

            if (me.RedDotNodeNeedShowSet.Contains(target) && redDotMonoView != null)
            {
                redDotMonoView.RefreshRedDotCount(me.RetainViewCount[target]);
            }
            
            bool isParentExist = me.ToParentDict.TryGetValue(target, out string parent);

            while (isParentExist)
            {
                var viewCount = 0;
                
                foreach (var childNode in me.RedDotNodeParentsDict[parent])
                {
                    viewCount += me.RetainViewCount[childNode];
                }

                me.RetainViewCount[parent] = viewCount;
                
                if (me.RedDotMonoViewDict.TryGetValue(parent, out redDotMonoView))
                {
                    if (me.RedDotNodeNeedShowSet.Contains(parent))
                    {
                        redDotMonoView.RefreshRedDotCount(me.RetainViewCount[parent]);
                    }
                }
                isParentExist = me.ToParentDict.TryGetValue(parent, out parent);
            }
        }
        
        public static GameObject GetORedDotGameObjectFromPool(this RedDotComponent me)
        {
            return GameObjectPoolHelper.GetObjectFromPool("RedDot",true,5);
        }

        public static void RecycleRedDotGameObject(this RedDotComponent me, GameObject go)
        {
            GameObjectPoolHelper.ReturnTransformToPool(go.transform);
        }
    }
}