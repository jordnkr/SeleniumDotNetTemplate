using System.Diagnostics;

namespace SeleniumDotNetTemplate.Shared
{
    public static class CommandLineUtil
    {
        public static string RunCommand(string command)
        {
            // if need to pass auth token (after --output): --extra-headers {"Cookie":""}
            ProcessStartInfo info = new ProcessStartInfo("cmd.exe", command);

            // Configure: no displayed terminal. allow output to be captured.
            info.CreateNoWindow = true;
            info.UseShellExecute = false;
            info.RedirectStandardOutput = true;

            Process process = Process.Start(info);
            string output = "";
            process.OutputDataReceived += new DataReceivedEventHandler((sender, e) => output += e.Data);
            process.BeginOutputReadLine();
            process.WaitForExit();
            process.Close();

            return output;
        }
    }
}
