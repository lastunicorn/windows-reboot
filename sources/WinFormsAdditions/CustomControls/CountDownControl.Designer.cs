namespace DustInTheWind.WinFormsAdditions.CustomControls
{
    partial class CountDownControl
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
            this.labelTimer = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // labelTimer
            // 
            this.labelTimer.BackColor = System.Drawing.Color.Transparent;
            this.labelTimer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelTimer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTimer.Font = new System.Drawing.Font("Trebuchet MS", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTimer.Location = new System.Drawing.Point(0, 0);
            this.labelTimer.Margin = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.labelTimer.Name = "labelTimer";
            this.labelTimer.Size = new System.Drawing.Size(454, 132);
            this.labelTimer.TabIndex = 8;
            this.labelTimer.Text = TimerText.Empty;
            this.labelTimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // CountDownControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelTimer);
            this.Name = "CountDownControl";
            this.Size = new System.Drawing.Size(454, 132);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelTimer;
        private System.Windows.Forms.Timer timer1;
    }
}
