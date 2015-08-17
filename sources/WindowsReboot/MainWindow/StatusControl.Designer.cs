namespace DustInTheWind.WindowsReboot.MainWindow
{
    partial class StatusControl
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
            this.tableLayoutPanelStatusInfo = new System.Windows.Forms.TableLayoutPanel();
            this.labelActionTime = new System.Windows.Forms.Label();
            this.labelCurrentTime = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelTimer = new System.Windows.Forms.Label();
            this.tableLayoutPanelStatusInfo.SuspendLayout();
            this.SuspendLayout();
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
            this.tableLayoutPanelStatusInfo.Controls.Add(this.labelTimer, 0, 2);
            this.tableLayoutPanelStatusInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelStatusInfo.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelStatusInfo.Name = "tableLayoutPanelStatusInfo";
            this.tableLayoutPanelStatusInfo.RowCount = 3;
            this.tableLayoutPanelStatusInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelStatusInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelStatusInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelStatusInfo.Size = new System.Drawing.Size(491, 92);
            this.tableLayoutPanelStatusInfo.TabIndex = 16;
            // 
            // labelActionTime
            // 
            this.labelActionTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelActionTime.Location = new System.Drawing.Point(85, 19);
            this.labelActionTime.Margin = new System.Windows.Forms.Padding(8, 0, 3, 0);
            this.labelActionTime.Name = "labelActionTime";
            this.labelActionTime.Size = new System.Drawing.Size(403, 19);
            this.labelActionTime.TabIndex = 13;
            this.labelActionTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCurrentTime
            // 
            this.labelCurrentTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCurrentTime.Location = new System.Drawing.Point(85, 0);
            this.labelCurrentTime.Margin = new System.Windows.Forms.Padding(8, 0, 3, 0);
            this.labelCurrentTime.Name = "labelCurrentTime";
            this.labelCurrentTime.Size = new System.Drawing.Size(403, 19);
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
            // labelTimer
            // 
            this.labelTimer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.labelTimer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tableLayoutPanelStatusInfo.SetColumnSpan(this.labelTimer, 2);
            this.labelTimer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTimer.Font = new System.Drawing.Font("Trebuchet MS", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTimer.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelTimer.Location = new System.Drawing.Point(0, 42);
            this.labelTimer.Margin = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.labelTimer.Name = "labelTimer";
            this.labelTimer.Size = new System.Drawing.Size(491, 50);
            this.labelTimer.TabIndex = 7;
            this.labelTimer.Text = "--  :  --  :  --  .  -";
            this.labelTimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StatusControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanelStatusInfo);
            this.Name = "StatusControl";
            this.Size = new System.Drawing.Size(491, 92);
            this.tableLayoutPanelStatusInfo.ResumeLayout(false);
            this.tableLayoutPanelStatusInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelStatusInfo;
        private System.Windows.Forms.Label labelActionTime;
        private System.Windows.Forms.Label labelCurrentTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelTimer;
    }
}
