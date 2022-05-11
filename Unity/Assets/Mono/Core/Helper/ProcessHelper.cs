using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Path = System.IO.Path;

namespace ET
{
    public static class ProcessHelper
    {
        public static Process Run(string exe, string arguments, string workingDirectory = ".")
        {
            //Log.Debug($"Process Run exe:{exe} ,arguments:{arguments} ,workingDirectory:{workingDirectory}");
            try
            {
                bool redirectStandardOutput = true;
                bool redirectStandardError = true;
                bool useShellExecute = false;
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    redirectStandardOutput = false;
                    redirectStandardError = false;
                    useShellExecute = true;
                }
                ProcessStartInfo info = new ProcessStartInfo
                {
                    FileName = exe,
                    Arguments = arguments,
                    CreateNoWindow = true,
                    UseShellExecute = useShellExecute,
                    WorkingDirectory = workingDirectory,
                    RedirectStandardOutput = redirectStandardOutput,
                    RedirectStandardError = redirectStandardError,
                };

                Process process = Process.Start(info);
                
                WaitExitAsync(process);

                return process;
            }
            catch (Exception e)
            {
                throw new Exception($"dir: {Path.GetFullPath(workingDirectory)}, command: {exe} {arguments}", e);
            }
        }
        
        private static async void WaitExitAsync(Process process)
        {
            await process.WaitForExitAsync();
#if NOT_UNITY
            Log.Info($"process exit, exitcode: {process.ExitCode} {process.StandardOutput.ReadToEnd()} {process.StandardError.ReadToEnd()}");
#endif
        }
        
#if !NOT_UNITY
        private static async Task WaitForExitAsync(this Process me)
        {
            if (!me.HasExited)
            {
                return;
            }

            try
            {
                me.EnableRaisingEvents = true;
            }
            catch (InvalidOperationException)
            {
                if (me.HasExited)
                {
                    return;
                }
                throw;
            }

            var tcs = new TaskCompletionSource<bool>();

            void Handler(object s, EventArgs e) => tcs.TrySetResult(true);
            
            me.Exited += Handler;

            try
            {
                if (me.HasExited)
                {
                    return;
                }
                await tcs.Task;
            }
            finally
            {
                me.Exited -= Handler;
            }
        }
#endif
    }
}