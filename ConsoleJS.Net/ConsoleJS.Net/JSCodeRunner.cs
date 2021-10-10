using System;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualBasic;

namespace ConsoleJS.Net
{
    public class JSCodeRunner
    {
        public JSCodeRunner(string envPath = "", bool requirePackageManager = true)
        {
            m_envPath = envPath;
            if (!"node".ExistsInPath())
                throw new ApplicationException("Node isnt installed. Please double check your PATH.");
            if (!"npm".ExistsInPath() && !"yarn".ExistsInPath() && requirePackageManager)
                throw new ApplicationException("Both NPM and YARN arent installed. Please double check your PATH.");
            if (!requirePackageManager) return;
            if ("yarn".ExistsInPath()) m_useYarn = true;
            if (!Directory.Exists(m_envPath)) Directory.CreateDirectory(m_envPath);
        }

        private bool m_useYarn = false;
        private string m_envPath = string.Empty;

        public string Evaluate(string script)
        {
            script = script.Replace("\"", "\\\"");

            var nodeProcess = new Process();
            nodeProcess.StartInfo.FileName = "node";
            nodeProcess.StartInfo.Arguments = $" -e \"{script}\"";
            nodeProcess.StartInfo.UseShellExecute = false;
            nodeProcess.StartInfo.RedirectStandardOutput = true;
            nodeProcess.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + m_envPath;
            nodeProcess.Start();

            var output = nodeProcess.StandardOutput.ReadToEnd();
            nodeProcess.WaitForExit();
            return output;
        }

        public string InstallPackages(params string[] npmPackages)
        {
            var packageManagerProcess = new Process();
            packageManagerProcess.StartInfo.FileName = m_useYarn ? "yarn" : "npm";
            packageManagerProcess.StartInfo.Arguments = $" add {string.Join(" ", npmPackages)}";
            packageManagerProcess.StartInfo.UseShellExecute = false;
            packageManagerProcess.StartInfo.RedirectStandardOutput = true;
            packageManagerProcess.StartInfo.RedirectStandardError = true;
            packageManagerProcess.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + m_envPath;
            packageManagerProcess.Start();
            var output = packageManagerProcess.StandardOutput.ReadToEnd();
            packageManagerProcess.WaitForExit();
            return output;
        }
    }
}