using System;

namespace ET
{
    [ObjectSystem]
    public class MailBoxComponentAwakeSystem: AwakeSystem<MailBoxComponent>
    {
        public override void Awake(MailBoxComponent me)
        {
            me.MailboxType = MailboxType.MessageDispatcher;
        }
    }

    [ObjectSystem]
    public class MailBoxComponentAwake1System: AwakeSystem<MailBoxComponent, MailboxType>
    {
        public override void Awake(MailBoxComponent me, MailboxType mailboxType)
        {
            me.MailboxType = mailboxType;
        }
    }
}