namespace DustInTheWind.WindowsReboot.MainWindow
{
    partial class TrayIcon
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrayIcon));
            this.contextMenuStripTrayIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new DustInTheWind.WindowsReboot.CustomControls.ToolStripMenuItemWithCommand();
            this.actionNowToolStripMenuItem = new DustInTheWind.WindowsReboot.CustomControls.ToolStripMenuItemWithCommand();
            this.lockComputerToolStripMenuItem = new DustInTheWind.WindowsReboot.CustomControls.ToolStripMenuItemWithCommand();
            this.logOffToolStripMenuItem = new DustInTheWind.WindowsReboot.CustomControls.ToolStripMenuItemWithCommand();
            this.sleepToolStripMenuItem = new DustInTheWind.WindowsReboot.CustomControls.ToolStripMenuItemWithCommand();
            this.hibernateToolStripMenuItem = new DustInTheWind.WindowsReboot.CustomControls.ToolStripMenuItemWithCommand();
            this.rebootToolStripMenuItem = new DustInTheWind.WindowsReboot.CustomControls.ToolStripMenuItemWithCommand();
            this.shutDownToolStripMenuItem = new DustInTheWind.WindowsReboot.CustomControls.ToolStripMenuItemWithCommand();
            this.powerOffToolStripMenuItem = new DustInTheWind.WindowsReboot.CustomControls.ToolStripMenuItemWithCommand();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem2 = new DustInTheWind.WindowsReboot.CustomControls.ToolStripMenuItemWithCommand();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripTrayIcon.SuspendLayout();
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
            this.toolStripMenuItem1.Command = null;
            this.toolStripMenuItem1.Image = global::DustInTheWind.WindowsReboot.Properties.Resources.win_reboot;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(137, 22);
            this.toolStripMenuItem1.Text = "Show";
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
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(134, 6);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Command = null;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(137, 22);
            this.toolStripMenuItem2.Text = "Exit";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStripTrayIcon;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            this.notifyIcon1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseMove);
            this.contextMenuStripTrayIcon.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStripTrayIcon;
        private CustomControls.ToolStripMenuItemWithCommand toolStripMenuItem1;
        private CustomControls.ToolStripMenuItemWithCommand actionNowToolStripMenuItem;
        private CustomControls.ToolStripMenuItemWithCommand lockComputerToolStripMenuItem;
        private CustomControls.ToolStripMenuItemWithCommand logOffToolStripMenuItem;
        private CustomControls.ToolStripMenuItemWithCommand sleepToolStripMenuItem;
        private CustomControls.ToolStripMenuItemWithCommand hibernateToolStripMenuItem;
        private CustomControls.ToolStripMenuItemWithCommand rebootToolStripMenuItem;
        private CustomControls.ToolStripMenuItemWithCommand shutDownToolStripMenuItem;
        private CustomControls.ToolStripMenuItemWithCommand powerOffToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private CustomControls.ToolStripMenuItemWithCommand toolStripMenuItem2;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}
