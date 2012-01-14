// Windows Reboot
// Copyright (C) 2009 Iuga Alexandru
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

namespace DustInTheWind.WindowsReboot
{
    partial class OptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonOkay = new System.Windows.Forms.Button();
            this.checkBoxMinimizeToTray = new System.Windows.Forms.CheckBox();
            this.checkBoxCloseToTray = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxStartTimerAtApplicationStart = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonClose.Location = new System.Drawing.Point(268, 119);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // buttonOkay
            // 
            this.buttonOkay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOkay.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOkay.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonOkay.Location = new System.Drawing.Point(187, 119);
            this.buttonOkay.Name = "buttonOkay";
            this.buttonOkay.Size = new System.Drawing.Size(75, 23);
            this.buttonOkay.TabIndex = 0;
            this.buttonOkay.Text = "Okay";
            this.buttonOkay.UseVisualStyleBackColor = true;
            this.buttonOkay.Click += new System.EventHandler(this.buttonOkay_Click);
            // 
            // checkBoxMinimizeToTray
            // 
            this.checkBoxMinimizeToTray.AutoSize = true;
            this.checkBoxMinimizeToTray.Location = new System.Drawing.Point(12, 19);
            this.checkBoxMinimizeToTray.Name = "checkBoxMinimizeToTray";
            this.checkBoxMinimizeToTray.Size = new System.Drawing.Size(98, 17);
            this.checkBoxMinimizeToTray.TabIndex = 1;
            this.checkBoxMinimizeToTray.Text = "Minimize to tray";
            this.checkBoxMinimizeToTray.UseVisualStyleBackColor = true;
            // 
            // checkBoxCloseToTray
            // 
            this.checkBoxCloseToTray.AutoSize = true;
            this.checkBoxCloseToTray.Location = new System.Drawing.Point(12, 42);
            this.checkBoxCloseToTray.Name = "checkBoxCloseToTray";
            this.checkBoxCloseToTray.Size = new System.Drawing.Size(84, 17);
            this.checkBoxCloseToTray.TabIndex = 1;
            this.checkBoxCloseToTray.Text = "Close to tray";
            this.checkBoxCloseToTray.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkBoxStartTimerAtApplicationStart);
            this.groupBox1.Controls.Add(this.checkBoxMinimizeToTray);
            this.groupBox1.Controls.Add(this.checkBoxCloseToTray);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(331, 101);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // checkBoxStartTimerAtApplicationStart
            // 
            this.checkBoxStartTimerAtApplicationStart.AutoSize = true;
            this.checkBoxStartTimerAtApplicationStart.Location = new System.Drawing.Point(12, 65);
            this.checkBoxStartTimerAtApplicationStart.Name = "checkBoxStartTimerAtApplicationStart";
            this.checkBoxStartTimerAtApplicationStart.Size = new System.Drawing.Size(309, 17);
            this.checkBoxStartTimerAtApplicationStart.TabIndex = 2;
            this.checkBoxStartTimerAtApplicationStart.Text = "Automatically start the timer when \"Windows Reboot\" starts.";
            this.checkBoxStartTimerAtApplicationStart.UseVisualStyleBackColor = true;
            // 
            // OptionsForm
            // 
            this.AcceptButton = this.buttonOkay;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(355, 154);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonOkay);
            this.Controls.Add(this.buttonClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.Shown += new System.EventHandler(this.OptionsForm_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonOkay;
        private System.Windows.Forms.CheckBox checkBoxMinimizeToTray;
        private System.Windows.Forms.CheckBox checkBoxCloseToTray;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxStartTimerAtApplicationStart;
    }
}