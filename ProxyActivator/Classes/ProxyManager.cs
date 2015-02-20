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
                    ret = new State("Deaktiviert", Color.Green);
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
            if (!Utils.CheckForSoftwareInstallation("owncloud"))
                return new State("Nicht installiert", Color.Red);
            else
            {
                return new State("NICHT ENTHALTEN", Color.Orange);
            }
        }
    }
}
