// Windows Reboot
// Copyright (C) 2009-2012 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace DustInTheWind.WindowsReboot.Presentation
{
    partial class WindowsRebootForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowsRebootForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.programToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goToTrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadInitialSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCurrentSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.loadDefaultSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.licenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelTimer = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripTrayIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.actionNowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lockComputerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logOffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sleepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hibernateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rebootToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shutDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.powerOffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.labelBlackLine1 = new System.Windows.Forms.Label();
            this.labelBlackLine2 = new System.Windows.Forms.Label();
            this.pictureBoxHeader = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxActionTime = new DustInTheWind.WindowsReboot.Presentation.CustomGroupBox(this.components);
            this.tabControlActionTime = new System.Windows.Forms.TabControl();
            this.tabPageFixedDate = new System.Windows.Forms.TabPage();
            this.fixedDateControl1 = new DustInTheWind.WindowsReboot.Presentation.FixedDateControl();
            this.tabPageDelay = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.numericUpDownSeconds = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMinutes = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownHours = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPageImmediate = new System.Windows.Forms.TabPage();
            this.labelImmediate = new System.Windows.Forms.Label();
            this.customGroupBoxStatusInfo = new DustInTheWind.WindowsReboot.Presentation.CustomGroupBox(this.components);
            this.tableLayoutPanelStatusInfo = new System.Windows.Forms.TableLayoutPanel();
            this.labelActionTime = new System.Windows.Forms.Label();
            this.labelCurrentTime = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBoxActionType = new DustInTheWind.WindowsReboot.Presentation.CustomGroupBox(this.components);
            this.tableLayoutPanelActionType = new System.Windows.Forms.TableLayoutPanel();
            this.checkBoxForceAction = new System.Windows.Forms.CheckBox();
            this.comboBoxAction = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxDisplayActionWarning = new System.Windows.Forms.CheckBox();
            this.groupBoxActionStart = new DustInTheWind.WindowsReboot.Presentation.CustomGroupBox(this.components);
            this.tableLayoutPanelActionStart = new System.Windows.Forms.TableLayoutPanel();
            this.buttonStopTimer = new System.Windows.Forms.Button();
            this.buttonStartTimer = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStripTrayIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHeader)).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
            this.groupBoxActionTime.SuspendLayout();
            this.tabControlActionTime.SuspendLayout();
            this.tabPageFixedDate.SuspendLayout();
            this.tabPageDelay.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHours)).BeginInit();
            this.tabPageImmediate.SuspendLayout();
            this.customGroupBoxStatusInfo.SuspendLayout();
            this.tableLayoutPanelStatusInfo.SuspendLayout();
            this.groupBoxActionType.SuspendLayout();
            this.tableLayoutPanelActionType.SuspendLayout();
            this.groupBoxActionStart.SuspendLayout();
            this.tableLayoutPanelActionStart.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programToolStripMenuItem,
            this.configurationToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(409, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // programToolStripMenuItem
            // 
            this.programToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.goToTrayToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.programToolStripMenuItem.Name = "programToolStripMenuItem";
            this.programToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.programToolStripMenuItem.Text = "&Program";
            // 
            // goToTrayToolStripMenuItem
            // 
            this.goToTrayToolStripMenuItem.Image = global::DustInTheWind.WindowsReboot.Properties.Resources.tray;
            this.goToTrayToolStripMenuItem.Name = "goToTrayToolStripMenuItem";
            this.goToTrayToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.goToTrayToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.goToTrayToolStripMenuItem.Text = "Go To &Tray";
            this.goToTrayToolStripMenuItem.Click += new System.EventHandler(this.goToTrayToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadInitialSettingsToolStripMenuItem,
            this.saveCurrentSettingsToolStripMenuItem,
            this.toolStripSeparator1,
            this.loadDefaultSettingsToolStripMenuItem,
            this.toolStripSeparator2,
            this.optionsToolStripMenuItem});
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.configurationToolStripMenuItem.Text = "&Configuration";
            // 
            // loadInitialSettingsToolStripMenuItem
            // 
            this.loadInitialSettingsToolStripMenuItem.Image = global::DustInTheWind.WindowsReboot.Properties.Resources.splat_blue;
            this.loadInitialSettingsToolStripMenuItem.Name = "loadInitialSettingsToolStripMenuItem";
            this.loadInitialSettingsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.loadInitialSettingsToolStripMenuItem.Text = "&Load Settings";
            this.loadInitialSettingsToolStripMenuItem.Click += new System.EventHandler(this.loadInitialSettingsToolStripMenuItem_Click);
            // 
            // saveCurrentSettingsToolStripMenuItem
            // 
            this.saveCurrentSettingsToolStripMenuItem.Image = global::DustInTheWind.WindowsReboot.Properties.Resources.splat_yellow;
            this.saveCurrentSettingsToolStripMenuItem.Name = "saveCurrentSettingsToolStripMenuItem";
            this.saveCurrentSettingsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.saveCurrentSettingsToolStripMenuItem.Text = "&Save Settings";
            this.saveCurrentSettingsToolStripMenuItem.Click += new System.EventHandler(this.saveCurrentSettingsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(183, 6);
            // 
            // loadDefaultSettingsToolStripMenuItem
            // 
            this.loadDefaultSettingsToolStripMenuItem.Image = global::DustInTheWind.WindowsReboot.Properties.Resources.splat_black;
            this.loadDefaultSettingsToolStripMenuItem.Name = "loadDefaultSettingsToolStripMenuItem";
            this.loadDefaultSettingsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.loadDefaultSettingsToolStripMenuItem.Text = "Load &Default Settings";
            this.loadDefaultSettingsToolStripMenuItem.Click += new System.EventHandler(this.loadDefaultSettingsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(183, 6);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.optionsToolStripMenuItem.Text = "&Options...";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.licenseToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // licenseToolStripMenuItem
            // 
            this.licenseToolStripMenuItem.Name = "licenseToolStripMenuItem";
            this.licenseToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.licenseToolStripMenuItem.Text = "&License";
            this.licenseToolStripMenuItem.Click += new System.EventHandler(this.licenseToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = global::DustInTheWind.WindowsReboot.Properties.Resources.help_about16;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // labelTimer
            // 
            this.labelTimer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTimer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.labelTimer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tableLayoutPanel6.SetColumnSpan(this.labelTimer, 2);
            this.labelTimer.Font = new System.Drawing.Font("Trebuchet MS", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTimer.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelTimer.Location = new System.Drawing.Point(0, 315);
            this.labelTimer.Margin = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.labelTimer.Name = "labelTimer";
            this.labelTimer.Size = new System.Drawing.Size(389, 40);
            this.labelTimer.TabIndex = 7;
            this.labelTimer.Text = "--  :  --  :  --  .  -";
            this.labelTimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStripTrayIcon;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            this.notifyIcon1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseMove);
            // 
            // contextMenuStripTrayIcon
            // 
            this.contextMenuStripTrayIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.actionNowToolStripMenuItem,
            this.toolStripSeparator3,
            this.toolStripMenuItem2});
            this.contextMenuStripTrayIcon.Name = "contextMenuStrip1";
            this.contextMenuStripTrayIcon.Size = new System.Drawing.Size(138, 76);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = global::DustInTheWind.WindowsReboot.Properties.Resources.win_reboot;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(137, 22);
            this.toolStripMenuItem1.Text = "Show";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // actionNowToolStripMenuItem
            // 
            this.actionNowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lockComputerToolStripMenuItem,
            this.logOffToolStripMenuItem,
            this.sleepToolStripMenuItem,
            this.hibernateToolStripMenuItem,
            this.rebootToolStripMenuItem,
            this.shutDownToolStripMenuItem,
            this.powerOffToolStripMenuItem});
            this.actionNowToolStripMenuItem.Name = "actionNowToolStripMenuItem";
            this.actionNowToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.actionNowToolStripMenuItem.Text = "Action Now";
            // 
            // lockComputerToolStripMenuItem
            // 
            this.lockComputerToolStripMenuItem.Image = global::DustInTheWind.WindowsReboot.Properties.Resources.lock_16x16;
            this.lockComputerToolStripMenuItem.Name = "lockComputerToolStripMenuItem";
            this.lockComputerToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.lockComputerToolStripMenuItem.Text = "Lock Computer";
            this.lockComputerToolStripMenuItem.Click += new System.EventHandler(this.lockComputerToolStripMenuItem_Click);
            // 
            // logOffToolStripMenuItem
            // 
            this.logOffToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("logOffToolStripMenuItem.Image")));
            this.logOffToolStripMenuItem.Name = "logOffToolStripMenuItem";
            this.logOffToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.logOffToolStripMenuItem.Text = "Log Off";
            this.logOffToolStripMenuItem.Click += new System.EventHandler(this.logOffToolStripMenuItem_Click);
            // 
            // sleepToolStripMenuItem
            // 
            this.sleepToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("sleepToolStripMenuItem.Image")));
            this.sleepToolStripMenuItem.Name = "sleepToolStripMenuItem";
            this.sleepToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.sleepToolStripMenuItem.Text = "Sleep";
            this.sleepToolStripMenuItem.Click += new System.EventHandler(this.sleepToolStripMenuItem_Click);
            // 
            // hibernateToolStripMenuItem
            // 
            this.hibernateToolStripMenuItem.Image = global::DustInTheWind.WindowsReboot.Properties.Resources.hibernate_16x16;
            this.hibernateToolStripMenuItem.Name = "hibernateToolStripMenuItem";
            this.hibernateToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.hibernateToolStripMenuItem.Text = "Hibernate";
            this.hibernateToolStripMenuItem.Click += new System.EventHandler(this.hibernateToolStripMenuItem_Click);
            // 
            // rebootToolStripMenuItem
            // 
            this.rebootToolStripMenuItem.Image = global::DustInTheWind.WindowsReboot.Properties.Resources.reboot_16x16;
            this.rebootToolStripMenuItem.Name = "rebootToolStripMenuItem";
            this.rebootToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.rebootToolStripMenuItem.Text = "Reboot";
            this.rebootToolStripMenuItem.Click += new System.EventHandler(this.rebootToolStripMenuItem_Click);
            // 
            // shutDownToolStripMenuItem
            // 
            this.shutDownToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("shutDownToolStripMenuItem.Image")));
            this.shutDownToolStripMenuItem.Name = "shutDownToolStripMenuItem";
            this.shutDownToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.shutDownToolStripMenuItem.Text = "Shut Down";
            this.shutDownToolStripMenuItem.Click += new System.EventHandler(this.shutDownToolStripMenuItem_Click);
            // 
            // powerOffToolStripMenuItem
            // 
            this.powerOffToolStripMenuItem.Image = global::DustInTheWind.WindowsReboot.Properties.Resources.poweroff_16x16;
            this.powerOffToolStripMenuItem.Name = "powerOffToolStripMenuItem";
            this.powerOffToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.powerOffToolStripMenuItem.Text = "Power Off";
            this.powerOffToolStripMenuItem.Click += new System.EventHandler(this.powerOffToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(134, 6);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(137, 22);
            this.toolStripMenuItem2.Text = "Exit";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // labelBlackLine1
            // 
            this.labelBlackLine1.BackColor = System.Drawing.Color.Black;
            this.labelBlackLine1.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelBlackLine1.Location = new System.Drawing.Point(0, 24);
            this.labelBlackLine1.Name = "labelBlackLine1";
            this.labelBlackLine1.Size = new System.Drawing.Size(409, 3);
            this.labelBlackLine1.TabIndex = 9;
            // 
            // labelBlackLine2
            // 
            this.labelBlackLine2.BackColor = System.Drawing.Color.Black;
            this.labelBlackLine2.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelBlackLine2.Location = new System.Drawing.Point(0, 87);
            this.labelBlackLine2.Name = "labelBlackLine2";
            this.labelBlackLine2.Size = new System.Drawing.Size(409, 3);
            this.labelBlackLine2.TabIndex = 10;
            // 
            // pictureBoxHeader
            // 
            this.pictureBoxHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBoxHeader.Image = global::DustInTheWind.WindowsReboot.Properties.Resources.header;
            this.pictureBoxHeader.Location = new System.Drawing.Point(0, 27);
            this.pictureBoxHeader.Name = "pictureBoxHeader";
            this.pictureBoxHeader.Size = new System.Drawing.Size(409, 60);
            this.pictureBoxHeader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxHeader.TabIndex = 8;
            this.pictureBoxHeader.TabStop = false;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 149F));
            this.tableLayoutPanel6.Controls.Add(this.groupBoxActionTime, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.customGroupBoxStatusInfo, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.groupBoxActionType, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.groupBoxActionStart, 1, 1);
            this.tableLayoutPanel6.Controls.Add(this.labelTimer, 0, 3);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 4;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(389, 355);
            this.tableLayoutPanel6.TabIndex = 21;
            // 
            // groupBoxActionTime
            // 
            this.groupBoxActionTime.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.tableLayoutPanel6.SetColumnSpan(this.groupBoxActionTime, 2);
            this.groupBoxActionTime.Controls.Add(this.tabControlActionTime);
            this.groupBoxActionTime.Location = new System.Drawing.Point(0, 0);
            this.groupBoxActionTime.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.groupBoxActionTime.Name = "groupBoxActionTime";
            this.groupBoxActionTime.Padding = new System.Windows.Forms.Padding(8, 10, 8, 8);
            this.groupBoxActionTime.Size = new System.Drawing.Size(389, 133);
            this.groupBoxActionTime.TabIndex = 18;
            this.groupBoxActionTime.TabStop = false;
            this.groupBoxActionTime.Text = "1) Choose when";
            this.groupBoxActionTime.TitleColor = System.Drawing.SystemColors.ControlText;
            this.groupBoxActionTime.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxActionTime.TitlePadding = new System.Windows.Forms.Padding(3, 0, 5, 0);
            // 
            // tabControlActionTime
            // 
            this.tabControlActionTime.Controls.Add(this.tabPageFixedDate);
            this.tabControlActionTime.Controls.Add(this.tabPageDelay);
            this.tabControlActionTime.Controls.Add(this.tabPageImmediate);
            this.tabControlActionTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlActionTime.Location = new System.Drawing.Point(8, 23);
            this.tabControlActionTime.Margin = new System.Windows.Forms.Padding(4);
            this.tabControlActionTime.Name = "tabControlActionTime";
            this.tabControlActionTime.SelectedIndex = 0;
            this.tabControlActionTime.Size = new System.Drawing.Size(373, 102);
            this.tabControlActionTime.TabIndex = 11;
            // 
            // tabPageFixedDate
            // 
            this.tabPageFixedDate.Controls.Add(this.fixedDateControl1);
            this.tabPageFixedDate.Location = new System.Drawing.Point(4, 22);
            this.tabPageFixedDate.Name = "tabPageFixedDate";
            this.tabPageFixedDate.Padding = new System.Windows.Forms.Padding(8);
            this.tabPageFixedDate.Size = new System.Drawing.Size(365, 76);
            this.tabPageFixedDate.TabIndex = 0;
            this.tabPageFixedDate.Text = "Fixed Date";
            this.tabPageFixedDate.UseVisualStyleBackColor = true;
            // 
            // fixedDateControl1
            // 
            this.fixedDateControl1.AutoSize = true;
            this.fixedDateControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fixedDateControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.fixedDateControl1.Location = new System.Drawing.Point(8, 8);
            this.fixedDateControl1.Name = "fixedDateControl1";
            this.fixedDateControl1.Size = new System.Drawing.Size(349, 52);
            this.fixedDateControl1.TabIndex = 5;
            this.fixedDateControl1.ViewModel = null;
            // 
            // tabPageDelay
            // 
            this.tabPageDelay.Controls.Add(this.tableLayoutPanel1);
            this.tabPageDelay.Location = new System.Drawing.Point(4, 22);
            this.tabPageDelay.Name = "tabPageDelay";
            this.tabPageDelay.Padding = new System.Windows.Forms.Padding(12);
            this.tabPageDelay.Size = new System.Drawing.Size(365, 76);
            this.tabPageDelay.TabIndex = 1;
            this.tabPageDelay.Text = "Delay";
            this.tabPageDelay.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.Controls.Add(this.numericUpDownSeconds, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDownMinutes, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDownHours, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(341, 39);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // numericUpDownSeconds
            // 
            this.numericUpDownSeconds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownSeconds.Location = new System.Drawing.Point(229, 16);
            this.numericUpDownSeconds.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownSeconds.Name = "numericUpDownSeconds";
            this.numericUpDownSeconds.Size = new System.Drawing.Size(109, 20);
            this.numericUpDownSeconds.TabIndex = 2;
            this.numericUpDownSeconds.ThousandsSeparator = true;
            // 
            // numericUpDownMinutes
            // 
            this.numericUpDownMinutes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownMinutes.Location = new System.Drawing.Point(116, 16);
            this.numericUpDownMinutes.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownMinutes.Name = "numericUpDownMinutes";
            this.numericUpDownMinutes.Size = new System.Drawing.Size(107, 20);
            this.numericUpDownMinutes.TabIndex = 2;
            this.numericUpDownMinutes.ThousandsSeparator = true;
            // 
            // numericUpDownHours
            // 
            this.numericUpDownHours.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownHours.Location = new System.Drawing.Point(3, 16);
            this.numericUpDownHours.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownHours.Name = "numericUpDownHours";
            this.numericUpDownHours.Size = new System.Drawing.Size(107, 20);
            this.numericUpDownHours.TabIndex = 2;
            this.numericUpDownHours.ThousandsSeparator = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(226, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Seconds:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Hours:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(113, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Minutes:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabPageImmediate
            // 
            this.tabPageImmediate.Controls.Add(this.labelImmediate);
            this.tabPageImmediate.Location = new System.Drawing.Point(4, 22);
            this.tabPageImmediate.Name = "tabPageImmediate";
            this.tabPageImmediate.Size = new System.Drawing.Size(365, 76);
            this.tabPageImmediate.TabIndex = 2;
            this.tabPageImmediate.Text = "Immediate";
            this.tabPageImmediate.UseVisualStyleBackColor = true;
            // 
            // labelImmediate
            // 
            this.labelImmediate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelImmediate.Location = new System.Drawing.Point(0, 0);
            this.labelImmediate.Margin = new System.Windows.Forms.Padding(4);
            this.labelImmediate.Name = "labelImmediate";
            this.labelImmediate.Size = new System.Drawing.Size(365, 76);
            this.labelImmediate.TabIndex = 0;
            this.labelImmediate.Text = "The action will be executed as soon as you press the\r\n\"Start timer\" button.";
            this.labelImmediate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customGroupBoxStatusInfo
            // 
            this.customGroupBoxStatusInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.customGroupBoxStatusInfo.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.tableLayoutPanel6.SetColumnSpan(this.customGroupBoxStatusInfo, 2);
            this.customGroupBoxStatusInfo.Controls.Add(this.tableLayoutPanelStatusInfo);
            this.customGroupBoxStatusInfo.Location = new System.Drawing.Point(0, 251);
            this.customGroupBoxStatusInfo.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.customGroupBoxStatusInfo.Name = "customGroupBoxStatusInfo";
            this.customGroupBoxStatusInfo.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.customGroupBoxStatusInfo.Size = new System.Drawing.Size(389, 56);
            this.customGroupBoxStatusInfo.TabIndex = 23;
            this.customGroupBoxStatusInfo.TabStop = false;
            this.customGroupBoxStatusInfo.TitleColor = System.Drawing.SystemColors.ControlText;
            this.customGroupBoxStatusInfo.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // tableLayoutPanelStatusInfo
            // 
            this.tableLayoutPanelStatusInfo.ColumnCount = 2;
            this.tableLayoutPanelStatusInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelStatusInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelStatusInfo.Controls.Add(this.labelActionTime, 1, 1);
            this.tableLayoutPanelStatusInfo.Controls.Add(this.labelCurrentTime, 1, 0);
            this.tableLayoutPanelStatusInfo.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanelStatusInfo.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanelStatusInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelStatusInfo.Location = new System.Drawing.Point(8, 9);
            this.tableLayoutPanelStatusInfo.Name = "tableLayoutPanelStatusInfo";
            this.tableLayoutPanelStatusInfo.RowCount = 2;
            this.tableLayoutPanelStatusInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelStatusInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelStatusInfo.Size = new System.Drawing.Size(373, 38);
            this.tableLayoutPanelStatusInfo.TabIndex = 15;
            // 
            // labelActionTime
            // 
            this.labelActionTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelActionTime.Location = new System.Drawing.Point(85, 19);
            this.labelActionTime.Margin = new System.Windows.Forms.Padding(8, 0, 3, 0);
            this.labelActionTime.Name = "labelActionTime";
            this.labelActionTime.Size = new System.Drawing.Size(285, 19);
            this.labelActionTime.TabIndex = 13;
            this.labelActionTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCurrentTime
            // 
            this.labelCurrentTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCurrentTime.Location = new System.Drawing.Point(85, 0);
            this.labelCurrentTime.Margin = new System.Windows.Forms.Padding(8, 0, 3, 0);
            this.labelCurrentTime.Name = "labelCurrentTime";
            this.labelCurrentTime.Size = new System.Drawing.Size(285, 19);
            this.labelCurrentTime.TabIndex = 13;
            this.labelCurrentTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 22);
            this.label5.Margin = new System.Windows.Forms.Padding(8, 0, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Action time:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 3);
            this.label4.Margin = new System.Windows.Forms.Padding(8, 0, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Current time:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBoxActionType
            // 
            this.groupBoxActionType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxActionType.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.groupBoxActionType.Controls.Add(this.tableLayoutPanelActionType);
            this.groupBoxActionType.Location = new System.Drawing.Point(0, 141);
            this.groupBoxActionType.Margin = new System.Windows.Forms.Padding(0, 4, 4, 4);
            this.groupBoxActionType.Name = "groupBoxActionType";
            this.groupBoxActionType.Padding = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.groupBoxActionType.Size = new System.Drawing.Size(236, 102);
            this.groupBoxActionType.TabIndex = 19;
            this.groupBoxActionType.TabStop = false;
            this.groupBoxActionType.Text = "2) Choose what";
            this.groupBoxActionType.TitleColor = System.Drawing.SystemColors.ControlText;
            this.groupBoxActionType.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxActionType.TitlePadding = new System.Windows.Forms.Padding(3, 0, 5, 0);
            // 
            // tableLayoutPanelActionType
            // 
            this.tableLayoutPanelActionType.AutoSize = true;
            this.tableLayoutPanelActionType.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanelActionType.ColumnCount = 2;
            this.tableLayoutPanelActionType.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelActionType.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelActionType.Controls.Add(this.checkBoxForceAction, 1, 1);
            this.tableLayoutPanelActionType.Controls.Add(this.comboBoxAction, 1, 0);
            this.tableLayoutPanelActionType.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanelActionType.Controls.Add(this.checkBoxDisplayActionWarning, 1, 2);
            this.tableLayoutPanelActionType.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanelActionType.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanelActionType.Name = "tableLayoutPanelActionType";
            this.tableLayoutPanelActionType.RowCount = 3;
            this.tableLayoutPanelActionType.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelActionType.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelActionType.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelActionType.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelActionType.Size = new System.Drawing.Size(230, 73);
            this.tableLayoutPanelActionType.TabIndex = 18;
            // 
            // checkBoxForceAction
            // 
            this.checkBoxForceAction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxForceAction.AutoSize = true;
            this.checkBoxForceAction.Checked = true;
            this.checkBoxForceAction.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxForceAction.Location = new System.Drawing.Point(72, 30);
            this.checkBoxForceAction.Name = "checkBoxForceAction";
            this.checkBoxForceAction.Size = new System.Drawing.Size(155, 17);
            this.checkBoxForceAction.TabIndex = 5;
            this.checkBoxForceAction.Text = "Force action";
            this.checkBoxForceAction.UseVisualStyleBackColor = true;
            // 
            // comboBoxAction
            // 
            this.comboBoxAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAction.FormattingEnabled = true;
            this.comboBoxAction.Location = new System.Drawing.Point(72, 3);
            this.comboBoxAction.Name = "comboBoxAction";
            this.comboBoxAction.Size = new System.Drawing.Size(155, 21);
            this.comboBoxAction.TabIndex = 3;
            this.comboBoxAction.SelectedIndexChanged += new System.EventHandler(this.comboBoxAction_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Action type:";
            // 
            // checkBoxDisplayActionWarning
            // 
            this.checkBoxDisplayActionWarning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxDisplayActionWarning.AutoSize = true;
            this.checkBoxDisplayActionWarning.Location = new System.Drawing.Point(72, 53);
            this.checkBoxDisplayActionWarning.Name = "checkBoxDisplayActionWarning";
            this.checkBoxDisplayActionWarning.Size = new System.Drawing.Size(155, 17);
            this.checkBoxDisplayActionWarning.TabIndex = 15;
            this.checkBoxDisplayActionWarning.Text = "Display warning";
            this.checkBoxDisplayActionWarning.UseVisualStyleBackColor = true;
            // 
            // groupBoxActionStart
            // 
            this.groupBoxActionStart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxActionStart.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.groupBoxActionStart.Controls.Add(this.tableLayoutPanelActionStart);
            this.groupBoxActionStart.Location = new System.Drawing.Point(244, 141);
            this.groupBoxActionStart.Margin = new System.Windows.Forms.Padding(4, 4, 0, 4);
            this.groupBoxActionStart.Name = "groupBoxActionStart";
            this.groupBoxActionStart.Padding = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.groupBoxActionStart.Size = new System.Drawing.Size(145, 102);
            this.groupBoxActionStart.TabIndex = 20;
            this.groupBoxActionStart.TabStop = false;
            this.groupBoxActionStart.Text = "3) Do it";
            this.groupBoxActionStart.TitleColor = System.Drawing.SystemColors.ControlText;
            this.groupBoxActionStart.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxActionStart.TitlePadding = new System.Windows.Forms.Padding(3, 0, 5, 0);
            // 
            // tableLayoutPanelActionStart
            // 
            this.tableLayoutPanelActionStart.ColumnCount = 1;
            this.tableLayoutPanelActionStart.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelActionStart.Controls.Add(this.buttonStopTimer, 0, 1);
            this.tableLayoutPanelActionStart.Controls.Add(this.buttonStartTimer, 0, 0);
            this.tableLayoutPanelActionStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelActionStart.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanelActionStart.Name = "tableLayoutPanelActionStart";
            this.tableLayoutPanelActionStart.RowCount = 2;
            this.tableLayoutPanelActionStart.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelActionStart.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelActionStart.Size = new System.Drawing.Size(139, 80);
            this.tableLayoutPanelActionStart.TabIndex = 5;
            // 
            // buttonStopTimer
            // 
            this.buttonStopTimer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStopTimer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonStopTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStopTimer.Location = new System.Drawing.Point(4, 43);
            this.buttonStopTimer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 4);
            this.buttonStopTimer.Name = "buttonStopTimer";
            this.buttonStopTimer.Size = new System.Drawing.Size(131, 33);
            this.buttonStopTimer.TabIndex = 4;
            this.buttonStopTimer.Text = "Stop";
            this.buttonStopTimer.UseVisualStyleBackColor = true;
            this.buttonStopTimer.Click += new System.EventHandler(this.buttonStopTimer_Click);
            // 
            // buttonStartTimer
            // 
            this.buttonStartTimer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStartTimer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonStartTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStartTimer.Location = new System.Drawing.Point(4, 4);
            this.buttonStartTimer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 3);
            this.buttonStartTimer.Name = "buttonStartTimer";
            this.buttonStartTimer.Size = new System.Drawing.Size(131, 33);
            this.buttonStartTimer.TabIndex = 0;
            this.buttonStartTimer.Text = "Start";
            this.buttonStartTimer.UseVisualStyleBackColor = true;
            this.buttonStartTimer.Click += new System.EventHandler(this.buttonStartTimer_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 87);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(409, 375);
            this.panel1.TabIndex = 22;
            // 
            // WindowsRebootForm
            // 
            this.AcceptButton = this.buttonStartTimer;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 462);
            this.Controls.Add(this.labelBlackLine2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBoxHeader);
            this.Controls.Add(this.labelBlackLine1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "WindowsRebootForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Windows Reboot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WindowsRebootForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.WindowsRebootForm_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStripTrayIcon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHeader)).EndInit();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.groupBoxActionTime.ResumeLayout(false);
            this.tabControlActionTime.ResumeLayout(false);
            this.tabPageFixedDate.ResumeLayout(false);
            this.tabPageFixedDate.PerformLayout();
            this.tabPageDelay.ResumeLayout(false);
            this.tabPageDelay.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHours)).EndInit();
            this.tabPageImmediate.ResumeLayout(false);
            this.customGroupBoxStatusInfo.ResumeLayout(false);
            this.tableLayoutPanelStatusInfo.ResumeLayout(false);
            this.tableLayoutPanelStatusInfo.PerformLayout();
            this.groupBoxActionType.ResumeLayout(false);
            this.groupBoxActionType.PerformLayout();
            this.tableLayoutPanelActionType.ResumeLayout(false);
            this.tableLayoutPanelActionType.PerformLayout();
            this.groupBoxActionStart.ResumeLayout(false);
            this.tableLayoutPanelActionStart.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem programToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goToTrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownSeconds;
        private System.Windows.Forms.NumericUpDown numericUpDownMinutes;
        private System.Windows.Forms.NumericUpDown numericUpDownHours;
        private System.Windows.Forms.ComboBox comboBoxAction;
        private System.Windows.Forms.Button buttonStartTimer;
        private System.Windows.Forms.Button buttonStopTimer;
        private System.Windows.Forms.CheckBox checkBoxForceAction;
        private System.Windows.Forms.Label labelTimer;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.PictureBox pictureBoxHeader;
        private System.Windows.Forms.Label labelBlackLine1;
        private System.Windows.Forms.Label labelBlackLine2;
        private System.Windows.Forms.TabControl tabControlActionTime;
        private System.Windows.Forms.TabPage tabPageFixedDate;
        private System.Windows.Forms.TabPage tabPageDelay;
        private System.Windows.Forms.TabPage tabPageImmediate;
        private System.Windows.Forms.Label labelCurrentTime;
        private System.Windows.Forms.Label labelActionTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelImmediate;
        private System.Windows.Forms.ToolStripMenuItem licenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveCurrentSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadDefaultSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadInitialSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTrayIcon;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem actionNowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lockComputerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logOffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rebootToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shutDownToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem powerOffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sleepToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hibernateToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelStatusInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelActionStart;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelActionType;
        private CustomGroupBox groupBoxActionTime;
        private CustomGroupBox groupBoxActionType;
        private CustomGroupBox groupBoxActionStart;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Panel panel1;
        private CustomGroupBox customGroupBoxStatusInfo;
        private System.Windows.Forms.CheckBox checkBoxDisplayActionWarning;
        private FixedDateControl fixedDateControl1;
    }
}

