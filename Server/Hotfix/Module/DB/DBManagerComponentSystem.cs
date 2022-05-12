using System;

namespace ET
{
    [FriendClass(typeof(DBManagerComponent))]
    public static class DBManagerComponentSystem
    {
        [ObjectSystem]
        public class DBManagerComponentAwakeSystem: AwakeSystem<DBManagerComponent>
        {
            public override void Awake(DBManagerComponent me)
            {
                DBManagerComponent.Instance = me;
            }
        }

        [ObjectSystem]
        public class DBManagerComponentDestroySystem: DestroySystem<DBManagerComponent>
        {
            public override void Destroy(DBManagerComponent me)
            {
                DBManagerComponent.Instance = null;
            }
        }
        
        public static DBComponent GetZoneDB(this DBManagerComponent me, int zone)
        {
            DBComponent dbComponent = me.DBComponents[zone];
            if (dbComponent != null)
            {
                return dbComponent;
            }

            StartZoneConfig startZoneConfig = StartZoneConfigCategory.Instance.Get(zone);
            if (startZoneConfig.DBConnection == "")
            {
                throw new Exception($"zone: {zone} not found mongo connect string");
            }

            dbComponent = me.AddChild<DBComponent, string, string, int>(startZoneConfig.DBConnection, startZoneConfig.DBName, zone);
            me.DBComponents[zone] = dbComponent;
            return dbComponent;
        }
    }
}