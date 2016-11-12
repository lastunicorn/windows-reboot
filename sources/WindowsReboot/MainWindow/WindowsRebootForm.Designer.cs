﻿// Windows Reboot
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

using DustInTheWind.WindowsReboot.CustomControls;
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
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripTrayIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.labelBlackLine1 = new System.Windows.Forms.Label();
            this.labelBlackLine2 = new System.Windows.Forms.Label();
            this.pictureBoxHeader = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBoxActionTime = new DustInTheWind.WindowsReboot.UiCommon.CustomGroupBox(this.components);
            this.actionTimeControl1 = new DustInTheWind.WindowsReboot.MainWindow.ActionTimeControl();
            this.customGroupBoxStatusInfo = new DustInTheWind.WindowsReboot.UiCommon.CustomGroupBox(this.components);
            this.statusControl1 = new DustInTheWind.WindowsReboot.MainWindow.StatusControl();
            this.groupBoxActionType = new DustInTheWind.WindowsReboot.UiCommon.CustomGroupBox(this.components);
            this.actionTypeControl1 = new DustInTheWind.WindowsReboot.MainWindow.ActionTypeControl();
            this.groupBoxActionStart = new DustInTheWind.WindowsReboot.UiCommon.CustomGroupBox(this.components);
            this.actionControl1 = new DustInTheWind.WindowsReboot.MainWindow.ActionControl();
            this.programToolStripMenuItem = new DustInTheWind.WindowsReboot.CustomControls.ToolStripMenuItemWithCommand();
            this.goToTrayToolStripMenuItem = new DustInTheWind.WindowsReboot.CustomControls.ToolStripMenuItemWithCommand();
            this.exitToolStripMenuItem = new DustInTheWind.WindowsReboot.CustomControls.ToolStripMenuItemWithCommand();
            this.configurationToolStripMenuItem = new DustInTheWind.WindowsReboot.CustomControls.ToolStripMenuItemWithCommand();
            this.loadInitialSettingsToolStripMenuItem = new DustInTheWind.WindowsReboot.CustomControls.ToolStripMenuItemWithCommand();
            this.saveCurrentSettingsToolStripMenuItem = new DustInTheWind.WindowsReboot.CustomControls.ToolStripMenuItemWithCommand();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.loadDefaultSettingsToolStripMenuItem = new DustInTheWind.WindowsReboot.CustomControls.ToolStripMenuItemWithCommand();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.optionsToolStripMenuItem = new DustInTheWind.WindowsReboot.CustomControls.ToolStripMenuItemWithCommand();
            this.helpToolStripMenuItem = new DustInTheWind.WindowsReboot.CustomControls.ToolStripMenuItemWithCommand();
            this.licenseToolStripMenuItem = new DustInTheWind.WindowsReboot.CustomControls.ToolStripMenuItemWithCommand();
            this.aboutToolStripMenuItem = new DustInTheWind.WindowsReboot.CustomControls.ToolStripMenuItemWithCommand();
            this.toolStripMenuItem1 = new DustInTheWind.WindowsReboot.CustomControls.ToolStripMenuItemWithCommand();
            this.actionNowToolStripMenuItem = new DustInTheWind.WindowsReboot.CustomControls.ToolStripMenuItemWithCommand();
            this.lockComputerToolStripMenuItem = new DustInTheWind.WindowsReboot.CustomControls.ToolStripMenuItemWithCommand();
            this.logOffToolStripMenuItem = new DustInTheWind.WindowsReboot.CustomControls.ToolStripMenuItemWithCommand();
            this.sleepToolStripMenuItem = new DustInTheWind.WindowsReboot.CustomControls.ToolStripMenuItemWithCommand();
            this.hibernateToolStripMenuItem = new DustInTheWind.WindowsReboot.CustomControls.ToolStripMenuItemWithCommand();
            this.rebootToolStripMenuItem = new DustInTheWind.WindowsReboot.CustomControls.ToolStripMenuItemWithCommand();
            this.shutDownToolStripMenuItem = new DustInTheWind.WindowsReboot.CustomControls.ToolStripMenuItemWithCommand();
            this.powerOffToolStripMenuItem = new DustInTheWind.WindowsReboot.CustomControls.ToolStripMenuItemWithCommand();
            this.toolStripMenuItem2 = new DustInTheWind.WindowsReboot.CustomControls.ToolStripMenuItemWithCommand();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStripTrayIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHeader)).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBoxActionTime.SuspendLayout();
            this.customGroupBoxStatusInfo.SuspendLayout();
            this.groupBoxActionType.SuspendLayout();
            this.groupBoxActionStart.SuspendLayout();
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
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(134, 6);
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
            // groupBoxActionTime
            // 
            this.groupBoxActionTime.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.tableLayoutPanel6.SetColumnSpan(this.groupBoxActionTime, 2);
            this.groupBoxActionTime.Controls.Add(this.actionTimeControl1);
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
            // actionTimeControl1
            // 
            this.actionTimeControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionTimeControl1.Location = new System.Drawing.Point(8, 23);
            this.actionTimeControl1.Name = "actionTimeControl1";
            this.actionTimeControl1.Size = new System.Drawing.Size(373, 102);
            this.actionTimeControl1.TabIndex = 1;
            this.actionTimeControl1.ViewModel = null;
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
            this.statusControl1.Margin = new System.Windows.Forms.Padding(0);
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
            this.groupBoxActionStart.Controls.Add(this.actionControl1);
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
            // actionControl1
            // 
            this.actionControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionControl1.Location = new System.Drawing.Point(3, 19);
            this.actionControl1.Name = "actionControl1";
            this.actionControl1.Size = new System.Drawing.Size(139, 80);
            this.actionControl1.TabIndex = 0;
            this.actionControl1.ViewModel = null;
            // 
            // programToolStripMenuItem
            // 
            this.programToolStripMenuItem.Command = null;
            this.programToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.goToTrayToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.programToolStripMenuItem.Name = "programToolStripMenuItem";
            this.programToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.programToolStripMenuItem.Text = "&Program";
            // 
            // goToTrayToolStripMenuItem
            // 
            this.goToTrayToolStripMenuItem.Command = null;
            this.goToTrayToolStripMenuItem.Image = global::DustInTheWind.WindowsReboot.Properties.Resources.tray;
            this.goToTrayToolStripMenuItem.Name = "goToTrayToolStripMenuItem";
            this.goToTrayToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.goToTrayToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.goToTrayToolStripMenuItem.Text = "Go To &Tray";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Command = null;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.Command = null;
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
            this.loadInitialSettingsToolStripMenuItem.Command = null;
            this.loadInitialSettingsToolStripMenuItem.Image = global::DustInTheWind.WindowsReboot.Properties.Resources.splat_blue;
            this.loadInitialSettingsToolStripMenuItem.Name = "loadInitialSettingsToolStripMenuItem";
            this.loadInitialSettingsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.loadInitialSettingsToolStripMenuItem.Text = "&Load Settings";
            // 
            // saveCurrentSettingsToolStripMenuItem
            // 
            this.saveCurrentSettingsToolStripMenuItem.Command = null;
            this.saveCurrentSettingsToolStripMenuItem.Image = global::DustInTheWind.WindowsReboot.Properties.Resources.splat_yellow;
            this.saveCurrentSettingsToolStripMenuItem.Name = "saveCurrentSettingsToolStripMenuItem";
            this.saveCurrentSettingsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.saveCurrentSettingsToolStripMenuItem.Text = "&Save Settings";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(183, 6);
            // 
            // loadDefaultSettingsToolStripMenuItem
            // 
            this.loadDefaultSettingsToolStripMenuItem.Command = null;
            this.loadDefaultSettingsToolStripMenuItem.Image = global::DustInTheWind.WindowsReboot.Properties.Resources.splat_black;
            this.loadDefaultSettingsToolStripMenuItem.Name = "loadDefaultSettingsToolStripMenuItem";
            this.loadDefaultSettingsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.loadDefaultSettingsToolStripMenuItem.Text = "Load &Default Settings";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(183, 6);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Command = null;
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.optionsToolStripMenuItem.Text = "&Options...";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Command = null;
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.licenseToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // licenseToolStripMenuItem
            // 
            this.licenseToolStripMenuItem.Command = null;
            this.licenseToolStripMenuItem.Name = "licenseToolStripMenuItem";
            this.licenseToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.licenseToolStripMenuItem.Text = "&License";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Command = null;
            this.aboutToolStripMenuItem.Image = global::DustInTheWind.WindowsReboot.Properties.Resources.help_about16;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Command = null;
            this.toolStripMenuItem1.Image = global::DustInTheWind.WindowsReboot.Properties.Resources.win_reboot;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(137, 22);
            this.toolStripMenuItem1.Text = "Show";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // actionNowToolStripMenuItem
            // 
            this.actionNowToolStripMenuItem.Command = null;
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
            this.lockComputerToolStripMenuItem.Command = null;
            this.lockComputerToolStripMenuItem.Image = global::DustInTheWind.WindowsReboot.Properties.Resources.lock_16x16;
            this.lockComputerToolStripMenuItem.Name = "lockComputerToolStripMenuItem";
            this.lockComputerToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.lockComputerToolStripMenuItem.Text = "Lock Computer";
            // 
            // logOffToolStripMenuItem
            // 
            this.logOffToolStripMenuItem.Command = null;
            this.logOffToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("logOffToolStripMenuItem.Image")));
            this.logOffToolStripMenuItem.Name = "logOffToolStripMenuItem";
            this.logOffToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.logOffToolStripMenuItem.Text = "Log Off";
            // 
            // sleepToolStripMenuItem
            // 
            this.sleepToolStripMenuItem.Command = null;
            this.sleepToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("sleepToolStripMenuItem.Image")));
            this.sleepToolStripMenuItem.Name = "sleepToolStripMenuItem";
            this.sleepToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.sleepToolStripMenuItem.Text = "Sleep";
            // 
            // hibernateToolStripMenuItem
            // 
            this.hibernateToolStripMenuItem.Command = null;
            this.hibernateToolStripMenuItem.Image = global::DustInTheWind.WindowsReboot.Properties.Resources.hibernate_16x16;
            this.hibernateToolStripMenuItem.Name = "hibernateToolStripMenuItem";
            this.hibernateToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.hibernateToolStripMenuItem.Text = "Hibernate";
            // 
            // rebootToolStripMenuItem
            // 
            this.rebootToolStripMenuItem.Command = null;
            this.rebootToolStripMenuItem.Image = global::DustInTheWind.WindowsReboot.Properties.Resources.reboot_16x16;
            this.rebootToolStripMenuItem.Name = "rebootToolStripMenuItem";
            this.rebootToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.rebootToolStripMenuItem.Text = "Reboot";
            // 
            // shutDownToolStripMenuItem
            // 
            this.shutDownToolStripMenuItem.Command = null;
            this.shutDownToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("shutDownToolStripMenuItem.Image")));
            this.shutDownToolStripMenuItem.Name = "shutDownToolStripMenuItem";
            this.shutDownToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.shutDownToolStripMenuItem.Text = "Shut Down";
            // 
            // powerOffToolStripMenuItem
            // 
            this.powerOffToolStripMenuItem.Command = null;
            this.powerOffToolStripMenuItem.Image = global::DustInTheWind.WindowsReboot.Properties.Resources.poweroff_16x16;
            this.powerOffToolStripMenuItem.Name = "powerOffToolStripMenuItem";
            this.powerOffToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.powerOffToolStripMenuItem.Text = "Power Off";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Command = null;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(137, 22);
            this.toolStripMenuItem2.Text = "Exit";
            // 
            // WindowsRebootForm
            // 
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
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.WindowsRebootForm_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStripTrayIcon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHeader)).EndInit();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBoxActionTime.ResumeLayout(false);
            this.customGroupBoxStatusInfo.ResumeLayout(false);
            this.groupBoxActionType.ResumeLayout(false);
            this.groupBoxActionType.PerformLayout();
            this.groupBoxActionStart.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private ToolStripMenuItemWithCommand programToolStripMenuItem;
        private ToolStripMenuItemWithCommand goToTrayToolStripMenuItem;
        private ToolStripMenuItemWithCommand exitToolStripMenuItem;
        private ToolStripMenuItemWithCommand helpToolStripMenuItem;
        private ToolStripMenuItemWithCommand aboutToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.PictureBox pictureBoxHeader;
        private System.Windows.Forms.Label labelBlackLine1;
        private System.Windows.Forms.Label labelBlackLine2;
        private ToolStripMenuItemWithCommand licenseToolStripMenuItem;
        private ToolStripMenuItemWithCommand configurationToolStripMenuItem;
        private ToolStripMenuItemWithCommand saveCurrentSettingsToolStripMenuItem;
        private ToolStripMenuItemWithCommand loadDefaultSettingsToolStripMenuItem;
        private ToolStripMenuItemWithCommand loadInitialSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItemWithCommand optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTrayIcon;
        private ToolStripMenuItemWithCommand toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItemWithCommand toolStripMenuItem2;
        private ToolStripMenuItemWithCommand actionNowToolStripMenuItem;
        private ToolStripMenuItemWithCommand lockComputerToolStripMenuItem;
        private ToolStripMenuItemWithCommand logOffToolStripMenuItem;
        private ToolStripMenuItemWithCommand rebootToolStripMenuItem;
        private ToolStripMenuItemWithCommand shutDownToolStripMenuItem;
        private ToolStripMenuItemWithCommand powerOffToolStripMenuItem;
        private ToolStripMenuItemWithCommand sleepToolStripMenuItem;
        private ToolStripMenuItemWithCommand hibernateToolStripMenuItem;
        private CustomGroupBox groupBoxActionTime;
        private CustomGroupBox groupBoxActionType;
        private CustomGroupBox groupBoxActionStart;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Panel panel1;
        private CustomGroupBox customGroupBoxStatusInfo;
        private StatusControl statusControl1;
        private ActionTypeControl actionTypeControl1;
        private ActionTimeControl actionTimeControl1;
        private ActionControl actionControl1;
    }
}

