using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class EntityTreeHelper
    {
        public class TreeNode
        {
            public int layer;
            public string node;
            public static TreeNode Create(int l, string n)
            {
                return new TreeNode() { layer = l, node = n };
            }
        }
        public static TreeNode CreateNode(this List<TreeNode> list, int layer, string node)
        {
            var n = TreeNode.Create(layer, node);
            list.Add(n);
            return n;
        }
        public static List<TreeNode> GetClientEntity()
        {
            var res = new List<TreeNode>(64);
            var layer = 0;
            var n = res.CreateNode(layer, "Game.Scene");
            GatherEntities(res, Game.Scene, n);
            return res;
        }

        private static void GatherEntities(List<TreeNode> res, Entity ent, TreeNode parent)
        {
            if (ent.Children.Count > 0)
            {
                foreach (var c in ent.Children.Values)
                {
                    GatherEntities(res, c, res.CreateNode(parent.layer + 1, "♦" + c.GetType().Name));
                }
            }
            if (ent.Components.Count > 0)
            {
                foreach (var c in ent.Components.Values)
                {
                    GatherEntities(res, c, res.CreateNode(parent.layer + 1, "○" + c.GetType().Name));
                }
            }
        }
    }
}
