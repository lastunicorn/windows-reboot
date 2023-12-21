namespace DustInTheWind.WindowsReboot.Presentation.MainWindow
{
    partial class ActionTimeControl
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
            this.tabControlActionTime = new System.Windows.Forms.TabControl();
            this.tabPageFixedDate = new System.Windows.Forms.TabPage();
            this.tabPageDaily = new System.Windows.Forms.TabPage();
            this.tabPageDelay = new System.Windows.Forms.TabPage();
            this.tabPageImmediate = new System.Windows.Forms.TabPage();
            this.labelImmediate = new System.Windows.Forms.Label();
            this.fixedDateControl1 = new FixedDateTabContent();
            this.dailyControl1 = new DailyTabContent();
            this.delayTimeControl1 = new DelayTimeTabContent();
            this.tabControlActionTime.SuspendLayout();
            this.tabPageFixedDate.SuspendLayout();
            this.tabPageDaily.SuspendLayout();
            this.tabPageDelay.SuspendLayout();
            this.tabPageImmediate.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlActionTime
            // 
            this.tabControlActionTime.Controls.Add(this.tabPageFixedDate);
            this.tabControlActionTime.Controls.Add(this.tabPageDaily);
            this.tabControlActionTime.Controls.Add(this.tabPageDelay);
            this.tabControlActionTime.Controls.Add(this.tabPageImmediate);
            this.tabControlActionTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlActionTime.Location = new System.Drawing.Point(0, 0);
            this.tabControlActionTime.Margin = new System.Windows.Forms.Padding(4);
            this.tabControlActionTime.Name = "tabControlActionTime";
            this.tabControlActionTime.SelectedIndex = 0;
            this.tabControlActionTime.Size = new System.Drawing.Size(436, 123);
            this.tabControlActionTime.TabIndex = 1;
            this.tabControlActionTime.SelectedIndexChanged += new System.EventHandler(this.tabControlActionTime_SelectedIndexChanged);
            // 
            // tabPageFixedDate
            // 
            this.tabPageFixedDate.Controls.Add(this.fixedDateControl1);
            this.tabPageFixedDate.Location = new System.Drawing.Point(4, 22);
            this.tabPageFixedDate.Name = "tabPageFixedDate";
            this.tabPageFixedDate.Padding = new System.Windows.Forms.Padding(8);
            this.tabPageFixedDate.Size = new System.Drawing.Size(428, 97);
            this.tabPageFixedDate.TabIndex = 0;
            this.tabPageFixedDate.Text = "Fixed Date";
            this.tabPageFixedDate.UseVisualStyleBackColor = true;
            // 
            // tabPageDaily
            // 
            this.tabPageDaily.Controls.Add(this.dailyControl1);
            this.tabPageDaily.Location = new System.Drawing.Point(4, 22);
            this.tabPageDaily.Name = "tabPageDaily";
            this.tabPageDaily.Padding = new System.Windows.Forms.Padding(8);
            this.tabPageDaily.Size = new System.Drawing.Size(428, 97);
            this.tabPageDaily.TabIndex = 3;
            this.tabPageDaily.Text = "Daily";
            this.tabPageDaily.UseVisualStyleBackColor = true;
            // 
            // tabPageDelay
            // 
            this.tabPageDelay.Controls.Add(this.delayTimeControl1);
            this.tabPageDelay.Location = new System.Drawing.Point(4, 22);
            this.tabPageDelay.Name = "tabPageDelay";
            this.tabPageDelay.Padding = new System.Windows.Forms.Padding(8);
            this.tabPageDelay.Size = new System.Drawing.Size(428, 97);
            this.tabPageDelay.TabIndex = 1;
            this.tabPageDelay.Text = "Delay";
            this.tabPageDelay.UseVisualStyleBackColor = true;
            // 
            // tabPageImmediate
            // 
            this.tabPageImmediate.Controls.Add(this.labelImmediate);
            this.tabPageImmediate.Location = new System.Drawing.Point(4, 22);
            this.tabPageImmediate.Name = "tabPageImmediate";
            this.tabPageImmediate.Size = new System.Drawing.Size(428, 97);
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
            this.labelImmediate.Size = new System.Drawing.Size(428, 97);
            this.labelImmediate.TabIndex = 0;
            this.labelImmediate.Text = "The action will be executed as soon as you press the\r\n\"Start timer\" button.";
            this.labelImmediate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fixedDateControl1
            // 
            this.fixedDateControl1.AutoSize = true;
            this.fixedDateControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fixedDateControl1.Date = new System.DateTime(2016, 11, 2, 0, 0, 0, 0);
            this.fixedDateControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fixedDateControl1.Location = new System.Drawing.Point(8, 8);
            this.fixedDateControl1.Name = "fixedDateControl1";
            this.fixedDateControl1.Size = new System.Drawing.Size(412, 81);
            this.fixedDateControl1.TabIndex = 5;
            // 
            // dailyControl1
            // 
            this.dailyControl1.AutoSize = true;
            this.dailyControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dailyControl1.Location = new System.Drawing.Point(8, 8);
            this.dailyControl1.Name = "dailyControl1";
            this.dailyControl1.Size = new System.Drawing.Size(412, 81);
            this.dailyControl1.TabIndex = 0;
            this.dailyControl1.Time = System.TimeSpan.Parse("18:21:01.7806729");
            // 
            // delayTimeControl1
            // 
            this.delayTimeControl1.AutoSize = true;
            this.delayTimeControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.delayTimeControl1.Hours = 0;
            this.delayTimeControl1.Location = new System.Drawing.Point(8, 8);
            this.delayTimeControl1.Minutes = 0;
            this.delayTimeControl1.Name = "delayTimeControl1";
            this.delayTimeControl1.Seconds = 0;
            this.delayTimeControl1.Size = new System.Drawing.Size(412, 81);
            this.delayTimeControl1.TabIndex = 0;
            // 
            // ActionTimeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlActionTime);
            this.Name = "ActionTimeControl";
            this.Size = new System.Drawing.Size(436, 123);
            this.tabControlActionTime.ResumeLayout(false);
            this.tabPageFixedDate.ResumeLayout(false);
            this.tabPageFixedDate.PerformLayout();
            this.tabPageDaily.ResumeLayout(false);
            this.tabPageDaily.PerformLayout();
            this.tabPageDelay.ResumeLayout(false);
            this.tabPageDelay.PerformLayout();
            this.tabPageImmediate.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlActionTime;
        private System.Windows.Forms.TabPage tabPageFixedDate;
        private FixedDateTabContent fixedDateControl1;
        private System.Windows.Forms.TabPage tabPageDaily;
        private DailyTabContent dailyControl1;
        private System.Windows.Forms.TabPage tabPageDelay;
        private DelayTimeTabContent delayTimeControl1;
        private System.Windows.Forms.TabPage tabPageImmediate;
        private System.Windows.Forms.Label labelImmediate;
    }
}
