using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Diagnostics;

namespace ProxyActivator
{
    class Utils
    {
        public static Boolean AppDataRoamingFolderExists(string FolderName)
        {
            String path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + FolderName;
            if (System.IO.Directory.Exists(path))
                return true;
            else
                return false;
        }

        public static Boolean AppDataLocalFolderExists(string FolderName)
        {
            String path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\" + FolderName;
            if (System.IO.Directory.Exists(path))
                return true;
            else
                return false;
        }

        public static String KillProcessAndGetExePathWait(string executableName)
        {
            Process[] processlist = Process.GetProcesses();
            foreach (Process theprocess in processlist)
            {
                if (theprocess.ProcessName.ToLower().Contains(executableName.ToLower()))
                {
                    string ExePath = theprocess.Modules[0].FileName;
                    theprocess.Kill();
                    theprocess.WaitForExit();
                    return ExePath;
                }
            }
            return "";
        }

        public static void StartExecutable(string path)
        {
            if ("" != path)
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = path;
                start.WindowStyle = ProcessWindowStyle.Hidden;
                start.CreateNoWindow = true;
                Process proc = Process.Start(start);
            }
        }

        /// <summary>
        /// Restarts a Application if its running
        /// </summary>
        /// <param name="ExecutableName">The .exe Name of the Program</param>
        public static void RestartApplicationIfRunning(string executableName, String arguments = "")
        {
            String path = KillProcessAndGetExePathWait(executableName);
            StartExecutable(path);
        }

        /// <summary>
        /// Check if a Software is installed.<br/>
        /// It will only be found if the software is listed in the software control panel of Windows.
        /// </summary>
        /// <param name="name">Name of the searched Software (or a substring of it)</param>
        /// <returns>true if found</returns>
        public static Boolean CheckForSoftwareInstallation(String name)
        {
            Boolean found = false;

            foreach (String sName in GetSoftwareList())
                if (sName.Contains(name))
                    found = true;
            return found;
        } 


        public static List<String> GetSoftwareList()
        {
            List<String> softwareList = new List<String>();
            string uninstallKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(uninstallKey))
            {
                foreach (string skName in rk.GetSubKeyNames())
                {
                    using (RegistryKey sk = rk.OpenSubKey(skName))
                    {
                        try
                        {

                            var displayName = sk.GetValue("DisplayName");
                            var size = sk.GetValue("EstimatedSize");

                            if (displayName != null)
                            {
                                softwareList.Add(displayName.ToString());
                            }
                        }
                        catch (Exception ex)
                        { }
                    }
                }
            }
            return softwareList;
        }
    }
}
