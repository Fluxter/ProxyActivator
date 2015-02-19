﻿using System;
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
            this.WindowState = FormWindowState.Minimized;
            notifyIcon.Visible = true;
            this.Hide();
            ShowBalloonTipText(
                "Proxy Activator gestartet",
                "Der Proxy Activator von Marcel Kallen läuft nun im Hintergrund.\nKontakt: admin@kallensrv.de",
                ToolTipIcon.Info, 1200
            );

            ContextMenu menue = new ContextMenu();
            menue.MenuItems.Add("E&xit");

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

        private void WLanCheck_Tick(object sender, EventArgs e)
        {
            if(WlanManager.Instance.ProxyActivated)
            {
                L_ProxyState.ForeColor = Color.Green;
                L_ProxyState.Text = "Im System initialisiert";
            }
            else
            {
                L_ProxyState.ForeColor = Color.Red;
                L_ProxyState.Text = "Nicht im System";
            }


            if (!WlanManager.Instance.ProxyActivated)
            {
                if (WlanManager.Instance.IsConnectedToAnySSID())
                {
                    if (WlanManager.Instance.IsConnectedToSSID(WlanManager.WifiSSID))
                    {
                        ShowBalloonTipText("Wlan \"" + WlanManager.WifiSSID + "\" Wlan verbunden!", "Der Systemproxy wurde automatisch eingestellt", ToolTipIcon.Info, 2000);
                        WlanManager.Instance.ActivateProxy("172.17.1.1", 3128);

                        L_Connected.ForeColor = Color.Green;
                        L_Connected.Text = "Verbunden";
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
                    L_Connected.ForeColor = Color.Orange;
                    L_Connected.Text = "Nicht verbunden";

                    ShowBalloonTipText("Wlan \"" + WlanManager.WifiSSID + "\" Wlan getrennt!", "Der Systemproxy wurde automatisch ausgeschaltet", ToolTipIcon.Warning, 2000);
                    WlanManager.Instance.DeactivateProxy();
                }
                else 
                {
                    if (!WlanManager.Instance.IsConnectedToSSID(WlanManager.WifiSSID))
                    {
                        ShowBalloonTipText("Wlan \"" + WlanManager.WifiSSID + "\" Wlan getrennt!", "Der Systemproxy wurde automatisch ausgeschaltet", ToolTipIcon.Warning, 2000);
                        WlanManager.Instance.DeactivateProxy();
                    }
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            WlanManager.Instance.DeactivateProxy();
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void notifyIcon_MouseClick_1(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.Hide();
                this.WindowState = FormWindowState.Minimized;
            }
            else
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.Focus();
            }
        }

        private void programmMitWindowsStartenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegistryKey AutostartKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            AutostartKey.SetValue("MK-BKTM-Autostart", Application.ExecutablePath.ToString());
            AutostartKey.Close();
            MessageBox.Show("Das Programm wird nun automatisch mit Windows gestartet.", "Erfolgreich", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void programmNichtMitWindowsStartenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegistryKey AutostartKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            AutostartKey.SetValue("MK-BKTM-Autostart", "");
            AutostartKey.Close();
            MessageBox.Show("Das Programm wird nun nicht mehr mit Windows gestartet.", "Erfolgreich", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}