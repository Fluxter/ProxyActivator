using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using Microsoft.Win32;


namespace ProxyActivator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Programm wurde gestartet.";
            notifyIcon.Visible = true;
            CheckVersion();

            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;

            ShowBalloonTipText(
                "ProxyActivator started.",
                "The Proxy Activator is now running in the background.",
                ToolTipIcon.Info, 600
            );

            ContextMenu menue = new ContextMenu();
            menue.MenuItems.Add(new MenuItem("About..", überToolStripMenuItem1_Click));
            menue.MenuItems.Add(new MenuItem("Exit", beendenToolStripMenuItem_Click));
            notifyIcon.ContextMenu = menue;

            String res = WLanAPManager.Instance.LoadAPsFromFile();
            if (res.Length != 0)
                MessageBox.Show("Couldnt load save file. \n \nError message: " + res, "Error reading save file", MessageBoxButtons.OK, MessageBoxIcon.Error);



            List<WLanAP> wlanaps = WLanAPManager.Instance.GetWLanAPs();
            if (wlanaps.Count != 0)
            {
                dataGridView1.Show();
                dataGridView1.ColumnCount = 3;
                dataGridView1.Columns[0].Name = "AP Name";
                dataGridView1.Columns[1].Name = "Proxy IP";
                dataGridView1.Columns[2].Name = "Proxy Port";
                foreach (WLanAP ap in WLanAPManager.Instance.GetWLanAPs())
                {
                    dataGridView1.Rows.Add(ap.APName, ap.proxyIP, ap.proxyPort);
                }
            }
            else dataGridView1.Hide();
            
        }

        private void ShowBalloonTipText(string title, string text, ToolTipIcon icon, int time)
        {
            notifyIcon.BalloonTipText = text;
            notifyIcon.BalloonTipTitle = title;
            notifyIcon.BalloonTipIcon = icon;
            notifyIcon.ShowBalloonTip(time);
        }
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                //ShowBalloonTipText("Proxy Activator läuft noch", "Das Programm läuft im Hintergrund weiter", ToolTipIcon.Info, 500);
                this.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }


        private void SetText(ref Label label, State state)
        {
            label.ForeColor = state.Color;
            label.Text = state.Text;
        }

        private void ChangeConnectedStatus(Color col, string text)
        {
            L_Connected.ForeColor = col;
            L_Connected.Text = text;
        }

        private void WLanCheck_Tick(object sender, EventArgs e)
        {
            // Proxy muss eingeschaltet werden
            if (WlanManager.Instance.IsConnectedToAnySSID())
            {
                //
                Boolean alreadyConfigured = false;
                Boolean anyDefinedAPConfigured = false;

                WLanAP connectedNotConfiguredAP = null;
                
                foreach (WLanAP ap in WLanAPManager.Instance.GetWLanAPs())
                {
                    if (WlanManager.Instance.IsConnectedToSSID(ap.APName))
                    {
                        if (ProxyManager.Instance.ConfiguredProxyName.Equals(ap.APName))
                        {
                            alreadyConfigured = true;
                        }
                        anyDefinedAPConfigured = true;
                        connectedNotConfiguredAP = ap;
                        break;
                    }
                }

                if (connectedNotConfiguredAP != null)
                {
                    if (!alreadyConfigured)
                    {
                        ChangeConnectedStatus(Color.Green, "Connected to " + connectedNotConfiguredAP.APName + ". Proxy enabled. (" + connectedNotConfiguredAP.proxyIP + ":" + connectedNotConfiguredAP.proxyPort + ")");
                        ProxyToggleAll(true, connectedNotConfiguredAP.proxyIP, connectedNotConfiguredAP.proxyPort, connectedNotConfiguredAP.APName);
                        ProxyManager.Instance.ConfiguredProxyName = connectedNotConfiguredAP.APName;

                        L_Proxy_Github.ForeColor = ProxyManager.Instance.ProxyStateGithub.Color;
                        L_Proxy_Github.Text = ProxyManager.Instance.ProxyStateGithub.Text;

                        L_Proxy_System.ForeColor = ProxyManager.Instance.ProxyStateSystem.Color;
                        L_Proxy_System.Text = ProxyManager.Instance.ProxyStateSystem.Text;

                        L_Proxy_Owncloud.ForeColor = ProxyManager.Instance.ProxyStateOwncloud.Color;
                        L_Proxy_Owncloud.Text = ProxyManager.Instance.ProxyStateOwncloud.Text;

                        L_Proxy_Spotify.ForeColor = ProxyManager.Instance.ProxyStateSpotify.Color;
                        L_Proxy_Spotify.Text = ProxyManager.Instance.ProxyStateSpotify.Text;
                    }
                }
                else if (!anyDefinedAPConfigured)
                {
                    ChangeConnectedStatus(Color.Red, "Not connected to any defined wireless network.");
                    ProxyToggleAll(false);
                }

                //
            }
            // Proxy muss ausgeschaltet werden
            else
            {
                ChangeConnectedStatus(Color.Orange, "Not connected to any wireless network");

                ProxyToggleAll(false);
            }
                
        }

        private void ProxyToggleAll(bool active, string ip = "", int port= 0 , string proxyName = "")
        {
            if (!active)
            {
                // Soll ausschalten
                if (ProxyManager.Instance.ConfiguredProxyName.Length != 0)
                {
                    ShowBalloonTipText("Deactivated", "Proxy was deactivated.", ToolTipIcon.Info, 2000);
                    ProxyManager.Instance.ProxyToggleAll(active, ip, port, proxyName);
                }
            }
            else
            {
                ShowBalloonTipText("Activated", "Proxy was activated. \nAP Name: " + proxyName + "\nProxy: " + ip + ":" + port + ".", ToolTipIcon.Info, 2000);
                ProxyManager.Instance.ProxyToggleAll(active, ip, port, proxyName);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void notifyIcon_MouseClick_1(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.Hide();
                this.ShowInTaskbar = false;
                this.Visible = false;
                this.WindowState = FormWindowState.Minimized;
            }
            else
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
                this.Visible = true;
                this.Focus();
            }
        }

        private void programmMitWindowsStartenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RegistryKey AutostartKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                AutostartKey.SetValue("FS-ProxyActivator-Start", Application.ExecutablePath.ToString());
                AutostartKey.Close();
                MessageBox.Show("Das Programm wird nun automatisch mit Windows gestartet.", "Erfolgreich", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch
            {
                MessageBox.Show("Konnte die Registry nicht beschreiben.\nWurde das Programm als Administrator gestartet?", "Fehler beim Eintrag", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void programmNichtMitWindowsStartenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RegistryKey AutostartKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                AutostartKey.DeleteValue("FS-ProxyActivator-Start");
                AutostartKey.Close();
                MessageBox.Show("Das Programm wird nun nicht mehr mit Windows gestartet.", "Erfolgreich", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch
            {
                MessageBox.Show("Konnte die Registry nicht beschreiben.\nWurde das Programm als Administrator gestartet?", "Fehler beim Eintrag", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Sind Sie sicher, dass Sie den Proxy Activator schließen möchten?", "Schließen", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
            {
                ProxyToggleAll(false);
                Application.Exit();
            }
            else
            {
                return;
            }
        }

        private void überToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string text = "Dieses Programm wurde für das Berufskolleg für Technik und Medien geschrieben.";
            text += "\nEs dient dazu, dass der Systemproxy von Windows Betriebssystemen\nautomatisch den Schul-proxy einschaltet, sobald man";
            text += "sich im Schul-Netzwerk befindet. Dies soll das ständige umstellen für Heim und Schul-Netz vereinfachen.";
            text += "\n\nEntwickler: Marcel Kallen (marcel.kallen@fursystems.de)\nAuf Github: https://github.com/Levitas/ProxyActivator";
            MessageBox.Show(text, "Über Proxy Activator", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void proxyAktivierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void proxyDeaktivierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProxyManager.Instance.ProxyToggleAll(false);
            MessageBox.Show("Alle Proxy Einstellungen wurden gelöscht.", "Erfolgreich", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void VersionCheckTimer_Tick(object sender, EventArgs e)
        {
            CheckVersion();
        }


        private Boolean VersionCheckInProgress = false;
        private void CheckVersion()
        {
            if (VersionCheckInProgress)
                return;
            Uri link = new Uri("http://proxyactivator.fursystems.de/bin/version.txt");
            System.Net.WebClient client = new System.Net.WebClient();
            client.DownloadDataCompleted += delegate(object sender, System.Net.DownloadDataCompletedEventArgs e)
                {
                    try
                    {
                        this.VersionCheckInProgress = false;
                        string data = System.Text.Encoding.UTF8.GetString(e.Result);
                        if (new Version(data).CompareTo(Global.Version) > 0)
                        {
                            VersionCheckTimer.Enabled = false;
                            toolStripStatusLabel1.Text = "Es wurde ein Update gefunden.";
                            DialogResult result = MessageBox.Show("Es ist eine neue Version verfügbar: " + data + "\nDeine Version: " + Global.Version + "\n \nMöchten Sie diese herunterladen?", "Neue Version", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (DialogResult.Yes == result)
                            {
                                System.Diagnostics.Process.Start("http://proxyactivator.fursystems.de/bin/ProxyActivator.exe");
                            }
                        }
                        else
                        {
                            
                            toolStripStatusLabel1.Text = "Kein Update verfügbar";
                        }
                    }
                    catch (Exception ex)
                    {
                        toolStripStatusLabel1.Text = "Fehler bei Update Überprüfung.";
                    }
                };
            client.DownloadDataAsync(link);
            VersionCheckInProgress = true;
            toolStripStatusLabel1.Text = "Überprüfe auf Updates.";

        }

        private void aufUpdatesPrüfenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckVersion();
            toolStripStatusLabel1.Text = "Manuelle Update Überprüfung gestartet.";
        }

        private void manuellToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
