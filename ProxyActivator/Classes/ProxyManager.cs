using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ProxyActivator
{
    class ProxyManager
    {
        private static ProxyManager instance = null;
        public static ProxyManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProxyManager();
                }
                return instance;
            }
        }

        public Boolean ProxyActivated = false;

        public State ProxyToggleSystem(Boolean enable)
        {
            if (enable)
            {
                WlanManager.Instance.ActivateProxy("172.17.1.1", 3128);
                return new State("Aktiviert", Color.Green);
            }
            else
            {
                WlanManager.Instance.DeactivateProxy();
                return new State("Deaktiviert", Color.Orange);
            }
        }

        public State ProxyToggleGithub(Boolean enable)
        {
            if (!Utils.AppDataRoamingFolderExists("GitHub"))
                return new State("Nicht installiert", Color.Red);
            else
            {
                State ret = null;
                String path = Environment.ExpandEnvironmentVariables(@"C:\Users\%USERNAME%\.gitconfig");
                IniFile file = new IniFile(path);
                if (enable)
                {
                    file.IniWriteValue("http", "proxy", "http://172.17.1.1:3128");
                    ret = new State("Aktiviert", Color.Green);
                }
                else
                {
                    file.IniWriteValue("http", "proxy", "");
                    ret = new State("Deaktiviert", Color.Red);
                }
                Utils.RestartApplicationIfRunning("GitHub");
                return ret;
            }
        }

        public State ProxyToggleSpotify(Boolean enable)
        {
            if (!Utils.AppDataRoamingFolderExists("Spotify"))
                return new State("Nicht installiert", Color.Red);
            else
            {
                return new State("NICHT ENTHALTEN", Color.Orange);
            }
        }

        public State ProxyToggleOwncloud(Boolean enable)
        {
            if (!Utils.AppDataLocalFolderExists("owncloud"))
                return new State("Nicht installiert", Color.Red);
            else
            {
                string ProcessExe = Utils.KillProcessAndGetExePathWait("owncloud");
                State ret = null;
                String path = Environment.ExpandEnvironmentVariables(Environment.SpecialFolder.LocalApplicationData + @"\owncloud\config.cfg");
                IniFile file = new IniFile(path);

                if (enable)
                {
                    file.IniWriteValue("Proxy", "type", "3");
                    file.IniWriteValue("Proxy", "host", "172.17.1.1");
                    file.IniWriteValue("Proxy", "port", "3128");
                    file.IniWriteValue("Proxy", "needsAuth", "false");
                    file.IniWriteValue("Proxy", "user", "");
                    file.IniWriteValue("Proxy", "pass", "@ByteArray()");
                    ret = new State("Aktiviert", Color.Green);
                }
                else
                {
                    file.IniWriteValue("Proxy", "type", "0");
                    ret = new State("Deaktiviert", Color.Red);
                }
                Utils.StartExecutable(ProcessExe);
                return ret;
            }
        }
    }
}
