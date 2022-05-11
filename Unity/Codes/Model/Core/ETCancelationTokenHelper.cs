namespace ET
{
    public static class ETCancelationTokenHelper
    {
        public static async ETTask CancelAfter(this ETCancellationToken me, long afterTimeCancel)
        {
            if (me.IsCancel())
            {
                return;
            }

            await TimerComponent.Instance.WaitAsync(afterTimeCancel);
            
            if (me.IsCancel())
            {
                return;
            }
            
            me.Cancel();
        }
    }
}