using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace ProxyActivator
{
    class WlanManager
    {
        private static WlanManager instance = null;
        public static WlanManager Instance {
            get { 
                if (instance == null) {
                    instance = new WlanManager(); 
                } 
                return instance; 
            }
        }

        #region Public Variables
        public Boolean ProxyActivated = false;
        public static String WifiSSID = "AP-BKTM";
        #endregion 

        #region Private Variables
        private WlanClient wlan = new WlanClient();
        #endregion

        #region Private Methods
        private List<String> GetConnectedSSIDs()
        {
            List<String> list = new List<String>();
            foreach (WlanClient.WlanInterface wlanInterface in wlan.Interfaces)
            {
                Wlan.Dot11Ssid ssid = wlanInterface.CurrentConnection.wlanAssociationAttributes.dot11Ssid;
                list.Add(new String(Encoding.ASCII.GetChars(ssid.SSID, 0, (int)ssid.SSIDLength)));
            }
            return list;
        }

        [DllImport("wininet.dll")]
        public static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);
        public const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
        public const int INTERNET_OPTION_REFRESH = 37;
        bool settingsReturn, refreshReturn;
        #endregion

        #region Public Methods
        public Boolean IsConnectedToAnySSID()
        {
            try
            {
                Int32 ConnectedSSIDs = this.GetConnectedSSIDs().Count;
                if (ConnectedSSIDs != 0)
                    return true;
                else
                    return false;
            }
            catch {
                return false;
            }
        }
        public Boolean IsConnectedToSSID(string ssidName)
        {
            if ( this.GetConnectedSSIDs().Contains(ssidName) ) return true;
            return false;
        }
        public void ActivateProxy(string ip, int port, bool enabled = true)
        {
            this.ProxyActivated = enabled;
            const string userRoot = "HKEY_CURRENT_USER";
            const string subkey = "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings";
            const string keyName = userRoot + "\\" + subkey;

            Registry.SetValue(keyName, "ProxyServer", ip + ":" + port);
            Registry.SetValue(keyName, "ProxyEnable", enabled ? "1" : "0");

            // These lines implement the Interface in the beginning of program 
            // They cause the OS to refresh the settings, causing IP to realy update
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
        }
        public void DeactivateProxy()
        {
            this.ActivateProxy("", 0, false);

        }
        #endregion
    }
}
