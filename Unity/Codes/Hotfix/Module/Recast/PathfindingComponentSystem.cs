using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    [FriendClass(typeof(PathfindingComponent))]
    public static class PathfindingComponentSystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<PathfindingComponent, string>
        {
            public override void Awake(PathfindingComponent me, string name)
            {
                me.Name = name;
                me.NavMesh = NavmeshComponent.Instance.Get(name);

                if (me.NavMesh == 0)
                {
                    throw new Exception($"nav load fail: {name}");
                }
            }
        }

        [ObjectSystem]
        public class DestroySystem: DestroySystem<PathfindingComponent>
        {
            public override void Destroy(PathfindingComponent me)
            {
                me.Name = string.Empty;
                me.NavMesh = 0;
            }
        }
        
        public static void Find(this PathfindingComponent me, Vector3 start, Vector3 target, List<Vector3> result)
        {
            if (me.NavMesh == 0)
            {
                Log.Debug("寻路| Find 失败 pathfinding ptr is zero");
                throw new Exception($"pathfinding ptr is zero: {me.DomainScene().Name}");
            }

            me.StartPos[0] = -start.x;
            me.StartPos[1] = start.y;
            me.StartPos[2] = start.z;

            me.EndPos[0] = -target.x;
            me.EndPos[1] = target.y;
            me.EndPos[2] = target.z;
            //Log.Debug($"start find path: {me.GetParent<Unit>().Id}");
            int n = Recast.RecastFind(me.NavMesh, PathfindingComponent.extents, me.StartPos, me.EndPos, me.Result);
            for (int i = 0; i < n; ++i)
            {
                int index = i * 3;
                result.Add(new Vector3(-me.Result[index], me.Result[index + 1], me.Result[index + 2]));
            }
            //Log.Debug($"finish find path: {me.GetParent<Unit>().Id} {result.ListToString()}");
        }

        public static void FindWithAdjust(this PathfindingComponent me, Vector3 start, Vector3 target, List<Vector3> result,float adjustRaduis)
        {
            me.Find(start, target, result);
            for (int i = 0; i < result.Count; i++)
            {
                Vector3 adjust = me.FindRandomPointWithRaduis(result[i], adjustRaduis);
                result[i] = adjust;
            }
        }
        
        public static Vector3 FindRandomPointWithRaduis(this PathfindingComponent me, Vector3 pos, float raduis)
        {
            if (me.NavMesh == 0)
            {
                throw new Exception($"pathfinding ptr is zero: {me.DomainScene().Name}");
            }

            if (raduis > PathfindingComponent.FindRandomNavPosMaxRadius * 0.001f)
            {
                throw new Exception($"pathfinding raduis is too large，cur: {raduis}, max: {PathfindingComponent.FindRandomNavPosMaxRadius}");
            }
            
            int degrees = RandomHelper.RandomNumber(0, 360);
            float r = RandomHelper.RandomNumber(0, (int) (raduis * 1000)) / 1000f;

            float x = r * Mathf.Cos(MathHelper.DegToRad(degrees));
            float z = r * Mathf.Sin(MathHelper.DegToRad(degrees));

            Vector3 findpos = new Vector3(pos.x + x, pos.y, pos.z + z);

            return me.RecastFindNearestPoint(findpos);
        }
        
        /// <summary>
        /// 以pos为中心各自在宽和高的左右 前后两个方向延伸
        /// </summary>
        /// <param name="me"></param>
        /// <param name="pos"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static Vector3 FindRandomPointWithRectangle(this PathfindingComponent me, Vector3 pos, int width, int height)
        {
            if (me.NavMesh == 0)
            {
                throw new Exception($"pathfinding ptr is zero: {me.DomainScene().Name}");
            }

            if (width > PathfindingComponent.FindRandomNavPosMaxRadius * 0.001f || height > PathfindingComponent.FindRandomNavPosMaxRadius * 0.001f)
            {
                throw new Exception($"pathfinding rectangle is too large，width: {width} height: {height}, max: {PathfindingComponent.FindRandomNavPosMaxRadius}");
            }
            
            float x = RandomHelper.RandomNumber(-width, width);
            float z = RandomHelper.RandomNumber(-height, height);

            Vector3 findpos = new Vector3(pos.x + x, pos.y, pos.z + z);

            return me.RecastFindNearestPoint(findpos);
        }
        
        public static Vector3 FindRandomPointWithRaduis(this PathfindingComponent me, Vector3 pos, float minRadius, float maxRadius)
        {
            if (me.NavMesh == 0)
            {
                throw new Exception($"pathfinding ptr is zero: {me.DomainScene().Name}");
            }

            if (maxRadius > PathfindingComponent.FindRandomNavPosMaxRadius * 0.001f)
            {
                throw new Exception($"pathfinding raduis is too large，cur: {maxRadius}, max: {PathfindingComponent.FindRandomNavPosMaxRadius}");
            }
            
            int degrees = RandomHelper.RandomNumber(0, 360);
            float r = RandomHelper.RandomNumber((int) (minRadius * 1000), (int) (maxRadius * 1000)) / 1000f;

            float x = r * Mathf.Cos(MathHelper.DegToRad(degrees));
            float z = r * Mathf.Sin(MathHelper.DegToRad(degrees));

            Vector3 findpos = new Vector3(pos.x + x, pos.y, pos.z + z);

            return me.RecastFindNearestPoint(findpos);
        }

        public static Vector3 RecastFindNearestPoint(this PathfindingComponent me, Vector3 pos)
        {
            if (me.NavMesh == 0)
            {
                throw new Exception($"pathfinding ptr is zero: {me.DomainScene().Name}");
            }

            me.StartPos[0] = -pos.x;
            me.StartPos[1] = pos.y;
            me.StartPos[2] = pos.z;

            int ret = Recast.RecastFindNearestPoint(me.NavMesh, PathfindingComponent.extents, me.StartPos, me.EndPos);
            if (ret == 0)
            {
                throw new Exception($"RecastFindNearestPoint fail, 可能是位置配置有问题: sceneName:{me.DomainScene().Name} {pos} {me.Name} {me.GetParent<Unit>().Id} {me.GetParent<Unit>().Config.Id} {me.EndPos.ArrayToString()}");
            }
            
            return new Vector3(-me.EndPos[0], me.EndPos[1], me.EndPos[2]);
        }
    }
}