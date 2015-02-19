namespace ProxyActivator
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.WLanCheck = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.L_Connected = new System.Windows.Forms.Label();
            this.L_ProxyState = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.programmMitWindowsStartenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.programmNichtMitWindowsStartenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Proxy Activator";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick_1);
            // 
            // WLanCheck
            // 
            this.WLanCheck.Enabled = true;
            this.WLanCheck.Interval = 2000;
            this.WLanCheck.Tick += new System.EventHandler(this.WLanCheck_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "W-Lan Status";
            // 
            // L_Connected
            // 
            this.L_Connected.AutoSize = true;
            this.L_Connected.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.L_Connected.Location = new System.Drawing.Point(174, 20);
            this.L_Connected.Name = "L_Connected";
            this.L_Connected.Size = new System.Drawing.Size(47, 13);
            this.L_Connected.TabIndex = 1;
            this.L_Connected.Text = "Lade...";
            // 
            // L_ProxyState
            // 
            this.L_ProxyState.AutoSize = true;
            this.L_ProxyState.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.L_ProxyState.Location = new System.Drawing.Point(174, 33);
            this.L_ProxyState.Name = "L_ProxyState";
            this.L_ProxyState.Size = new System.Drawing.Size(47, 13);
            this.L_ProxyState.TabIndex = 3;
            this.L_ProxyState.Text = "Lade...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Proxy Status";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(230, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Created by Marcel Kallen (admin@kallensrv.de)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.L_Connected);
            this.groupBox1.Controls.Add(this.L_ProxyState);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(22, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(330, 59);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Informationen";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 24);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(468, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(468, 24);
            this.menuStrip2.TabIndex = 7;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programmMitWindowsStartenToolStripMenuItem,
            this.programmNichtMitWindowsStartenToolStripMenuItem});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.dateiToolStripMenuItem.Text = "Datei";
            // 
            // programmMitWindowsStartenToolStripMenuItem
            // 
            this.programmMitWindowsStartenToolStripMenuItem.Name = "programmMitWindowsStartenToolStripMenuItem";
            this.programmMitWindowsStartenToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.programmMitWindowsStartenToolStripMenuItem.Text = "Programm mit Windows starten";
            this.programmMitWindowsStartenToolStripMenuItem.Click += new System.EventHandler(this.programmMitWindowsStartenToolStripMenuItem_Click);
            // 
            // programmNichtMitWindowsStartenToolStripMenuItem
            // 
            this.programmNichtMitWindowsStartenToolStripMenuItem.Name = "programmNichtMitWindowsStartenToolStripMenuItem";
            this.programmNichtMitWindowsStartenToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.programmNichtMitWindowsStartenToolStripMenuItem.Text = "Programm nicht mit Windows starten";
            this.programmNichtMitWindowsStartenToolStripMenuItem.Click += new System.EventHandler(this.programmNichtMitWindowsStartenToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 201);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.menuStrip2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Proxy Activator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Timer WLanCheck;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label L_Connected;
        private System.Windows.Forms.Label L_ProxyState;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem programmMitWindowsStartenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem programmNichtMitWindowsStartenToolStripMenuItem;
    }
}

