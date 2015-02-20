using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

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
