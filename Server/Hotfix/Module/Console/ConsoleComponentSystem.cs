using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ET
{
    [ObjectSystem]
    public class ConsoleComponentAwakeSystem: AwakeSystem<ConsoleComponent>
    {
        public override void Awake(ConsoleComponent me)
        {
            me.Load();
            
            me.Start().Coroutine();
        }
    }

    [ObjectSystem]
    public class ConsoleComponentLoadSystem: LoadSystem<ConsoleComponent>
    {
        public override void Load(ConsoleComponent me)
        {
            me.Load();
        }
    }

    [FriendClass(typeof(ConsoleComponent))]
    [FriendClass(typeof(ModeContex))]
    public static class ConsoleComponentSystem
    {
        public static void Load(this ConsoleComponent me)
        {
            me.Handlers.Clear();

            List<Type> types = EventSystem.Instance.GetTypes(typeof (ConsoleHandlerAttribute));

            foreach (Type type in types)
            {
                object[] attrs = type.GetCustomAttributes(typeof(ConsoleHandlerAttribute), false);
                if (attrs.Length == 0)
                {
                    continue;
                }

                ConsoleHandlerAttribute consoleHandlerAttribute = (ConsoleHandlerAttribute)attrs[0];

                object obj = Activator.CreateInstance(type);

                IConsoleHandler iConsoleHandler = obj as IConsoleHandler;
                if (iConsoleHandler == null)
                {
                    throw new Exception($"ConsoleHandler handler not inherit IConsoleHandler class: {obj.GetType().FullName}");
                }
                me.Handlers.Add(consoleHandlerAttribute.Mode, iConsoleHandler);
            }
        }
        
        public static async ETTask Start(this ConsoleComponent me)
        {
            me.CancellationTokenSource = new CancellationTokenSource();

            while (true)
            {
                try
                {
                    ModeContex modeContex = me.GetComponent<ModeContex>();
                    string line = await Task.Factory.StartNew(() =>
                    {
                        Console.Write($"{modeContex?.Mode ?? ""}> ");
                        return Console.In.ReadLine();
                    }, me.CancellationTokenSource.Token);
                    
                    line = line.Trim();

                    switch (line)
                    {
                        case "":
                            break;
                        case "exit":
                            me.RemoveComponent<ModeContex>();
                            break;
                        default:
                        {
                            string[] lines = line.Split(" ");
                            string mode = modeContex == null? lines[0] : modeContex.Mode;

                            if (!me.Handlers.TryGetValue(mode, out IConsoleHandler iConsoleHandler))
                            {
                                Log.Console($"not found command: {line}");
                                break;
                            }

                            if (modeContex == null)
                            {
                                modeContex = me.AddComponent<ModeContex>();
                                modeContex.Mode = mode;
                            }
                            await iConsoleHandler.Run(modeContex, line);
                            break;
                        }
                    }


                }
                catch (Exception e)
                {
                    Log.Console(e.ToString());
                }
            }
        }
    }
}