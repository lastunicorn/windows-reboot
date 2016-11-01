// Windows Reboot
// Copyright (C) 2009-2015 Dust in the Wind
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

using DustInTheWind.WindowsReboot.UiCommon;

namespace DustInTheWind.WindowsReboot.MainWindow
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
            this.groupBoxActionTime = new DustInTheWind.WindowsReboot.UiCommon.CustomGroupBox(this.components);
            this.tabControlActionTime = new System.Windows.Forms.TabControl();
            this.tabPageFixedDate = new System.Windows.Forms.TabPage();
            this.fixedDateControl1 = new DustInTheWind.WindowsReboot.MainWindow.FixedDateControl();
            this.tabPageDaily = new System.Windows.Forms.TabPage();
            this.dailyControl1 = new DustInTheWind.WindowsReboot.MainWindow.DailyControl();
            this.tabPageDelay = new System.Windows.Forms.TabPage();
            this.delayTimeControl1 = new DustInTheWind.WindowsReboot.MainWindow.DelayTimeControl();
            this.tabPageImmediate = new System.Windows.Forms.TabPage();
            this.labelImmediate = new System.Windows.Forms.Label();
            this.customGroupBoxStatusInfo = new DustInTheWind.WindowsReboot.UiCommon.CustomGroupBox(this.components);
            this.statusControl1 = new DustInTheWind.WindowsReboot.MainWindow.StatusControl();
            this.groupBoxActionType = new DustInTheWind.WindowsReboot.UiCommon.CustomGroupBox(this.components);
            this.actionTypeControl1 = new DustInTheWind.WindowsReboot.MainWindow.ActionTypeControl();
            this.groupBoxActionStart = new DustInTheWind.WindowsReboot.UiCommon.CustomGroupBox(this.components);
            this.tableLayoutPanelActionStart = new System.Windows.Forms.TableLayoutPanel();
            this.buttonStopTimer = new System.Windows.Forms.Button();
            this.buttonStartTimer = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.actionTimeControl1 = new DustInTheWind.WindowsReboot.MainWindow.ActionTimeControl();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStripTrayIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHeader)).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
            this.groupBoxActionTime.SuspendLayout();
            this.tabControlActionTime.SuspendLayout();
            this.tabPageFixedDate.SuspendLayout();
            this.tabPageDaily.SuspendLayout();
            this.tabPageDelay.SuspendLayout();
            this.tabPageImmediate.SuspendLayout();
            this.customGroupBoxStatusInfo.SuspendLayout();
            this.groupBoxActionType.SuspendLayout();
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
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 3;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(389, 355);
            this.tableLayoutPanel6.TabIndex = 21;
            // 
            // groupBoxActionTime
            // 
            this.groupBoxActionTime.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.tableLayoutPanel6.SetColumnSpan(this.groupBoxActionTime, 2);
            this.groupBoxActionTime.Controls.Add(this.actionTimeControl1);
            this.groupBoxActionTime.Controls.Add(this.tabControlActionTime);
            this.groupBoxActionTime.CornerRadius = 0;
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
            this.groupBoxActionTime.TitleMargin = new System.Windows.Forms.Padding(0);
            this.groupBoxActionTime.TitlePadding = new System.Windows.Forms.Padding(3, 0, 5, 0);
            // 
            // tabControlActionTime
            // 
            this.tabControlActionTime.Controls.Add(this.tabPageFixedDate);
            this.tabControlActionTime.Controls.Add(this.tabPageDaily);
            this.tabControlActionTime.Controls.Add(this.tabPageDelay);
            this.tabControlActionTime.Controls.Add(this.tabPageImmediate);
            this.tabControlActionTime.Location = new System.Drawing.Point(8, 23);
            this.tabControlActionTime.Margin = new System.Windows.Forms.Padding(4);
            this.tabControlActionTime.Name = "tabControlActionTime";
            this.tabControlActionTime.SelectedIndex = 0;
            this.tabControlActionTime.Size = new System.Drawing.Size(190, 98);
            this.tabControlActionTime.TabIndex = 0;
            // 
            // tabPageFixedDate
            // 
            this.tabPageFixedDate.Controls.Add(this.fixedDateControl1);
            this.tabPageFixedDate.Location = new System.Drawing.Point(4, 22);
            this.tabPageFixedDate.Name = "tabPageFixedDate";
            this.tabPageFixedDate.Padding = new System.Windows.Forms.Padding(8);
            this.tabPageFixedDate.Size = new System.Drawing.Size(182, 72);
            this.tabPageFixedDate.TabIndex = 0;
            this.tabPageFixedDate.Text = "Fixed Date";
            this.tabPageFixedDate.UseVisualStyleBackColor = true;
            // 
            // fixedDateControl1
            // 
            this.fixedDateControl1.AutoSize = true;
            this.fixedDateControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fixedDateControl1.Date = new System.DateTime(2016, 11, 1, 16, 30, 31, 316);
            this.fixedDateControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.fixedDateControl1.FullTime = new System.DateTime(2016, 11, 2, 9, 1, 2, 621);
            this.fixedDateControl1.Location = new System.Drawing.Point(8, 8);
            this.fixedDateControl1.Name = "fixedDateControl1";
            this.fixedDateControl1.Size = new System.Drawing.Size(166, 52);
            this.fixedDateControl1.TabIndex = 5;
            this.fixedDateControl1.Time = new System.DateTime(2016, 11, 1, 16, 30, 31, 304);
            // 
            // tabPageDaily
            // 
            this.tabPageDaily.Controls.Add(this.dailyControl1);
            this.tabPageDaily.Location = new System.Drawing.Point(4, 22);
            this.tabPageDaily.Name = "tabPageDaily";
            this.tabPageDaily.Padding = new System.Windows.Forms.Padding(8);
            this.tabPageDaily.Size = new System.Drawing.Size(365, 76);
            this.tabPageDaily.TabIndex = 3;
            this.tabPageDaily.Text = "Daily";
            this.tabPageDaily.UseVisualStyleBackColor = true;
            // 
            // dailyControl1
            // 
            this.dailyControl1.AutoSize = true;
            this.dailyControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dailyControl1.Location = new System.Drawing.Point(8, 8);
            this.dailyControl1.Name = "dailyControl1";
            this.dailyControl1.Size = new System.Drawing.Size(349, 26);
            this.dailyControl1.TabIndex = 0;
            // 
            // tabPageDelay
            // 
            this.tabPageDelay.Controls.Add(this.delayTimeControl1);
            this.tabPageDelay.Location = new System.Drawing.Point(4, 22);
            this.tabPageDelay.Name = "tabPageDelay";
            this.tabPageDelay.Padding = new System.Windows.Forms.Padding(12);
            this.tabPageDelay.Size = new System.Drawing.Size(365, 76);
            this.tabPageDelay.TabIndex = 1;
            this.tabPageDelay.Text = "Delay";
            this.tabPageDelay.UseVisualStyleBackColor = true;
            // 
            // delayTimeControl1
            // 
            this.delayTimeControl1.AutoSize = true;
            this.delayTimeControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.delayTimeControl1.Location = new System.Drawing.Point(12, 12);
            this.delayTimeControl1.Name = "delayTimeControl1";
            this.delayTimeControl1.Size = new System.Drawing.Size(341, 39);
            this.delayTimeControl1.TabIndex = 0;
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
            this.customGroupBoxStatusInfo.Controls.Add(this.statusControl1);
            this.customGroupBoxStatusInfo.CornerRadius = 0;
            this.customGroupBoxStatusInfo.Location = new System.Drawing.Point(0, 251);
            this.customGroupBoxStatusInfo.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.customGroupBoxStatusInfo.Name = "customGroupBoxStatusInfo";
            this.customGroupBoxStatusInfo.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.customGroupBoxStatusInfo.Size = new System.Drawing.Size(389, 100);
            this.customGroupBoxStatusInfo.TabIndex = 23;
            this.customGroupBoxStatusInfo.TabStop = false;
            this.customGroupBoxStatusInfo.TitleColor = System.Drawing.SystemColors.ControlText;
            this.customGroupBoxStatusInfo.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customGroupBoxStatusInfo.TitleMargin = new System.Windows.Forms.Padding(0);
            this.customGroupBoxStatusInfo.TitlePadding = new System.Windows.Forms.Padding(0);
            // 
            // statusControl1
            // 
            this.statusControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusControl1.Location = new System.Drawing.Point(3, 4);
            this.statusControl1.Name = "statusControl1";
            this.statusControl1.Size = new System.Drawing.Size(383, 92);
            this.statusControl1.TabIndex = 16;
            this.statusControl1.TabStop = false;
            this.statusControl1.ViewModel = null;
            // 
            // groupBoxActionType
            // 
            this.groupBoxActionType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxActionType.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.groupBoxActionType.Controls.Add(this.actionTypeControl1);
            this.groupBoxActionType.CornerRadius = 0;
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
            this.groupBoxActionType.TitleMargin = new System.Windows.Forms.Padding(0);
            this.groupBoxActionType.TitlePadding = new System.Windows.Forms.Padding(3, 0, 5, 0);
            // 
            // actionTypeControl1
            // 
            this.actionTypeControl1.AutoSize = true;
            this.actionTypeControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.actionTypeControl1.Location = new System.Drawing.Point(3, 21);
            this.actionTypeControl1.Name = "actionTypeControl1";
            this.actionTypeControl1.Size = new System.Drawing.Size(230, 73);
            this.actionTypeControl1.TabIndex = 1;
            this.actionTypeControl1.ViewModel = null;
            // 
            // groupBoxActionStart
            // 
            this.groupBoxActionStart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxActionStart.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.groupBoxActionStart.Controls.Add(this.tableLayoutPanelActionStart);
            this.groupBoxActionStart.CornerRadius = 0;
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
            this.groupBoxActionStart.TitleMargin = new System.Windows.Forms.Padding(0);
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
            this.buttonStopTimer.TabIndex = 3;
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
            this.buttonStartTimer.TabIndex = 2;
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
            // actionTimeControl1
            // 
            this.actionTimeControl1.Location = new System.Drawing.Point(200, 23);
            this.actionTimeControl1.Name = "actionTimeControl1";
            this.actionTimeControl1.Size = new System.Drawing.Size(186, 99);
            this.actionTimeControl1.TabIndex = 1;
            this.actionTimeControl1.ViewModel = null;
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
            this.tabPageDaily.ResumeLayout(false);
            this.tabPageDaily.PerformLayout();
            this.tabPageDelay.ResumeLayout(false);
            this.tabPageDelay.PerformLayout();
            this.tabPageImmediate.ResumeLayout(false);
            this.customGroupBoxStatusInfo.ResumeLayout(false);
            this.groupBoxActionType.ResumeLayout(false);
            this.groupBoxActionType.PerformLayout();
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
        private System.Windows.Forms.Button buttonStartTimer;
        private System.Windows.Forms.Button buttonStopTimer;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.PictureBox pictureBoxHeader;
        private System.Windows.Forms.Label labelBlackLine1;
        private System.Windows.Forms.Label labelBlackLine2;
        private System.Windows.Forms.TabControl tabControlActionTime;
        private System.Windows.Forms.TabPage tabPageFixedDate;
        private System.Windows.Forms.TabPage tabPageDelay;
        private System.Windows.Forms.TabPage tabPageImmediate;
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelActionStart;
        private CustomGroupBox groupBoxActionTime;
        private CustomGroupBox groupBoxActionType;
        private CustomGroupBox groupBoxActionStart;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Panel panel1;
        private CustomGroupBox customGroupBoxStatusInfo;
        private FixedDateControl fixedDateControl1;
        private StatusControl statusControl1;
        private DelayTimeControl delayTimeControl1;
        private ActionTypeControl actionTypeControl1;
        private System.Windows.Forms.TabPage tabPageDaily;
        private DailyControl dailyControl1;
        private ActionTimeControl actionTimeControl1;
    }
}

