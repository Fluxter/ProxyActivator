using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace ProxyActivator
{
    class WLanAPManager
    {
        private static WLanAPManager _Instance = null;

        public static WLanAPManager Instance
        {
            get { if (_Instance == null) _Instance = new WLanAPManager(); return _Instance; }
        }

        private List<WLanAP> WLanAPs = new List<WLanAP>();

        public List<WLanAP> GetWLanAPs()
        {
            return this.WLanAPs;
        }
        public String LoadAPsFromFile(string file = Global.APSaveFile)
        {
            try
            {
                if (File.Exists(file))
                {
                    string xmltext = File.ReadAllText(file);
                    using (XmlReader reader = XmlReader.Create(file))
                    {
                        WLanAP ap = null;
                        while(reader.Read())
                        {
                            switch (reader.NodeType)
                            {
                                case XmlNodeType.Element:
                                    switch (reader.Name)
                                    {
                                        case "AccessPoint":
                                            ap = new WLanAP();
                                            this.WLanAPs.Add(ap);
                                            break;
                                        case "APName":
                                            ap.APName = reader.ReadString();
                                            break;
                                        case "proxyIP":
                                            ap.proxyIP = reader.ReadString();
                                            break;
                                        case "proxyPort":
                                            ap.proxyPort = Convert.ToInt32(reader.ReadString());
                                            break;
                                    }
                                    break;
                            }
                        }
                    }
   
                }
                else
                {
                    File.Create(file);
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
