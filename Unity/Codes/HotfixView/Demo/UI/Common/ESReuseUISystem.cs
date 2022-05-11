namespace ET
{
    public static class ESReuseUISystem
    {
        public static void TestFunction(this ESReuseUI me,string content)
        {
            me.ELabel_testText.text = content;
        }
    }
}