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


        public string ConfiguredProxyName = "";

        public void ProxyToggleAll(bool active, string ip = "", int port = 0, string ProxyName = "")
        {
            this.ProxyToggleSystem(active, ip, port);
            this.ProxyToggleGithub(active, ip, port);
            this.ProxyToggleOwncloud(active, ip, port);
        }

        public State ProxyStateSystem = new State("No information.", Color.Black);
        public void ProxyToggleSystem(Boolean enable, string ip, int port)
        {
            if (enable)
            {
                WlanManager.Instance.ActivateProxy(ip, port);
                ProxyStateSystem = new State("Aktiviert", Color.Green);
            }
            else
            {
                WlanManager.Instance.DeactivateProxy();
                ProxyStateSystem = new State("Deaktiviert", Color.Orange);
            }
        }

        public State ProxyStateGithub = new State("No information.", Color.Black);
        public void ProxyToggleGithub(Boolean enable, String ip, Int32 port)
        {
            if (!Utils.AppDataRoamingFolderExists("GitHub"))
                ProxyStateGithub = new State("Nicht installiert", Color.Red);
            else
            {
                String path = Environment.ExpandEnvironmentVariables(@"C:\Users\%USERNAME%\.gitconfig");
                IniFile file = new IniFile(path);
                if (enable)
                {
                    file.IniWriteValue("http", "proxy", "http://" + ip + ":" + port);
                    ProxyStateGithub = new State("Aktiviert", Color.Green);
                }
                else
                {
                    file.IniWriteValue("http", "proxy", "");
                    ProxyStateGithub = new State("Deaktiviert", Color.Red);
                }
                Utils.RestartApplicationIfRunning("GitHub");
            }
        }

        public State ProxyStateSpotify = new State("No information.", Color.Black);
        public void ProxyToggleSpotify(Boolean enable, String ip, Int32 port)
        {
            if (!Utils.AppDataRoamingFolderExists("Spotify"))
                ProxyStateSpotify = new State("Not installed.", Color.Red);
            else
            {
                ProxyStateSpotify = new State("Not ready yet.", Color.Orange);
            }
        }

        public State ProxyStateOwncloud = new State("No information.", Color.Black);
        public void ProxyToggleOwncloud(Boolean enable, String ip, Int32 port)
        {
            if (!Utils.AppDataLocalFolderExists("owncloud"))
                ProxyStateOwncloud = new State("Nicht installiert", Color.Red);
            else
            {
                string ProcessExe = Utils.KillProcessAndGetExePathWait("owncloud");
                String path = Environment.ExpandEnvironmentVariables(Environment.SpecialFolder.LocalApplicationData + @"\owncloud\config.cfg");
                IniFile file = new IniFile(path);

                if (enable)
                {
                    file.IniWriteValue("Proxy", "type", "3");
                    file.IniWriteValue("Proxy", "host", ip);
                    file.IniWriteValue("Proxy", "port", port.ToString());
                    file.IniWriteValue("Proxy", "needsAuth", "false");
                    file.IniWriteValue("Proxy", "user", "");
                    file.IniWriteValue("Proxy", "pass", "@ByteArray()");
                    ProxyStateOwncloud = new State("Aktiviert", Color.Green);
                }
                else
                {
                    file.IniWriteValue("Proxy", "type", "0");
                    ProxyStateOwncloud = new State("Deaktiviert", Color.Red);
                }
                Utils.StartExecutable(ProcessExe);
            }
        }
    }
}
