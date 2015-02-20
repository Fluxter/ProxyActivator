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
            notifyIcon.Visible = true;

            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;

            ShowBalloonTipText(
                "Proxy Activator gestartet",
                "Der BK-TM Proxy Activator läuft nun im Hintergrund.\nKontakt: admin@kallensrv.de",
                ToolTipIcon.Info, 600
            );

            ContextMenu menue = new ContextMenu();
            menue.MenuItems.Add(new MenuItem("Über..", überToolStripMenuItem1_Click));
            menue.MenuItems.Add(new MenuItem("Exit", beendenToolStripMenuItem_Click));
            notifyIcon.ContextMenu = menue;
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

        private void ActivateAllProxies()
        {
            if (ProxyManager.Instance.ProxyActivated)
                return;

            ProxyManager.Instance.ProxyActivated = true;
            L_Connected.ForeColor = Color.Green;
            L_Connected.Text = "Verbunden";

            ShowBalloonTipText("Wlan \"" + WlanManager.WifiSSID + "\" Wlan verbunden!", "Alle Proxys wurden eingerichtet.", ToolTipIcon.Info, 2000);

            this.ToggleProxies(true);
        }
        private void DeactivateAllProxies()
        {
            if (ProxyManager.Instance.ProxyActivated)
                return;

            ProxyManager.Instance.ProxyActivated = false;
            L_Connected.ForeColor = Color.Orange;
            L_Connected.Text = "Nicht verbunden";

            this.ToggleProxies(false);

            ShowBalloonTipText("Wlan \"" + WlanManager.WifiSSID + "\" Wlan getrennt!", "Alle Proxies wurden ausgeschaltet", ToolTipIcon.Warning, 2000);
        }

        private void SetText(ref Label label, State state)
        {
            label.ForeColor = state.Color;
            label.Text = state.Text;
        }

        private void ToggleProxies(Boolean enable)
        {
            // Systemproxy
            this.SetText(ref L_Proxy_System, ProxyManager.Instance.ProxyToggleSystem(enable));
            // Github Proxy
            this.SetText(ref L_Proxy_Github, ProxyManager.Instance.ProxyToggleGithub(enable));
            // Spotify Proxy
            this.SetText(ref L_Proxy_Spotify, ProxyManager.Instance.ProxyToggleSpotify(enable));
            // Owncloud Proxy
            this.SetText(ref L_Proxy_Owncloud, ProxyManager.Instance.ProxyToggleOwncloud(enable));
        }

        private void WLanCheck_Tick(object sender, EventArgs e)
        {
            if (!ProxyManager.Instance.ProxyActivated)
            {
                if (WlanManager.Instance.IsConnectedToAnySSID())
                {
                    if (WlanManager.Instance.IsConnectedToSSID(WlanManager.WifiSSID))
                    {
                        ActivateAllProxies();
                    }
                    else
                    {
                        L_Connected.ForeColor = Color.Orange;
                        L_Connected.Text = "Nicht mit Schulnetz verbunden";
                    }
                }
                else 
                {
                    L_Connected.ForeColor = Color.Red;
                    L_Connected.Text = "Nicht verbunden";
                }
            }
            else
            {
                if (!WlanManager.Instance.IsConnectedToAnySSID())
                {
                    DeactivateAllProxies();
                }
                else 
                {
                    if (!WlanManager.Instance.IsConnectedToSSID(WlanManager.WifiSSID))
                    {
                        DeactivateAllProxies();
                    }
                }
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
                AutostartKey.SetValue("MK-BKTM-Autostart", Application.ExecutablePath.ToString());
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
                AutostartKey.DeleteValue("MK-BKTM-Autostart");
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
                WlanManager.Instance.DeactivateProxy();
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
            text += "\n\nEntwickler: Marcel Kallen (admin@kallensrv.de)\nAuf Github: https://github.com/Levitas/ProxyActivator";
            MessageBox.Show(text, "Über Proxy Activator", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
