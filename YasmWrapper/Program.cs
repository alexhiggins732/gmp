namespace YasmWrapper
{
    internal class Program
    {
        static string AppLocation = null!;
        static string logFileName= null!;
        static void Main(string[] args)
        {
            var asmArgs = args.Where(x => x.Contains(".asm")).ToList();
            
            var AppLocation = AppContext.BaseDirectory;
            Console.WriteLine($"Current Directory: {AppLocation}");
            logFileName = Path.Combine(AppLocation, "yasmwrapper.log");


            var exe = Path.GetFullPath(Path.Combine(AppLocation, "vsyasm.exe"));
            Log($"Handling args: {string.Join(" ", args)}");



            if (!File.Exists(exe))
            {
                var message = $"Error Could not find yasm.exe at {exe}";
                Log(message);
                throw new FileNotFoundException("Could not locate vsyasm.exe", exe);
            }
            Log($"Using exe: {exe}");
            var additionalArgList = args.Where(x => x != exe && !asmArgs.Contains(x)).ToList();
            var additionalArgs = string.Join(" ", additionalArgList);
            foreach(var asmArg in asmArgs)
            {
                var yasmArgs= $"{additionalArgs} {asmArg}";
                Log($"Execuing args: {yasmArgs}");
                var p = new System.Diagnostics.Process();
                p.StartInfo.FileName = exe;
                p.StartInfo.Arguments = yasmArgs;
                p.Start();
                p.WaitForExit();
                var exitCode = p.ExitCode;
                Log($"Task finished with code {exitCode}");
                if (exitCode != 0)
                {
                    throw new Exception($"Error code: {exitCode} executing {yasmArgs}");
                }


            }
        }

        static bool loggingEnabled = true;
        private static void Log(string message)
        {
            if (loggingEnabled)
            {
                File.AppendAllText(logFileName, message + Environment.NewLine);
            }
        }
    }
}