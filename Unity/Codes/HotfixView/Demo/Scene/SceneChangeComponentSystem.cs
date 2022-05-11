using UnityEngine.SceneManagement;

namespace ET
{
    public class SceneChangeComponentUpdateSystem: UpdateSystem<SceneChangeComponent>
    {
        public override void Update(SceneChangeComponent me)
        {
            if (!me.loadMapOperation.isDone)
            {
                return;
            }

            if (me.tcs == null)
            {
                return;
            }
            
            ETTask tcs = me.tcs;
            me.tcs = null;
            tcs.SetResult();
        }
    }
	
    
    public class SceneChangeComponentDestroySystem: DestroySystem<SceneChangeComponent>
    {
        public override void Destroy(SceneChangeComponent me)
        {
            me.loadMapOperation = null;
            me.tcs = null;
        }
    }

    [FriendClass(typeof(SceneChangeComponent))]
    public static class SceneChangeComponentSystem
    {
        public static async ETTask ChangeSceneAsync(this SceneChangeComponent me, string sceneName)
        {
            me.tcs = ETTask.Create(true);
            // 加载map
            me.loadMapOperation = SceneManager.LoadSceneAsync(sceneName);
            //this.loadMapOperation.allowSceneActivation = false;
            await me.tcs;
        }
        
        public static int Process(this SceneChangeComponent me)
        {
            if (me.loadMapOperation == null)
            {
                return 0;
            }
            return (int)(me.loadMapOperation.progress * 100);
        }
    }
}