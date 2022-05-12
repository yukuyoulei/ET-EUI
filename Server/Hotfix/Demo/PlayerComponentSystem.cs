using System.Linq;

namespace ET
{
    [FriendClass(typeof(PlayerComponent))]
    public static class PlayerComponentSystem
    {
        public class AwakeSystem : AwakeSystem<PlayerComponent>
        {
            public override void Awake(PlayerComponent me)
            {
            }
        }

        [ObjectSystem]
        public class PlayerComponentDestroySystem: DestroySystem<PlayerComponent>
        {
            public override void Destroy(PlayerComponent me)
            {
            }
        }
        
        public static void Add(this PlayerComponent me, Player player)
        {
            me.idPlayers.Add(player.Id, player);
        }

        public static Player Get(this PlayerComponent me,long id)
        {
            me.idPlayers.TryGetValue(id, out Player gamer);
            return gamer;
        }

        public static void Remove(this PlayerComponent me,long id)
        {
            me.idPlayers.Remove(id);
        }

        public static Player[] GetAll(this PlayerComponent me)
        {
            return me.idPlayers.Values.ToArray();
        }
    }
}