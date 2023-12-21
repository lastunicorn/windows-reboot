using DustInTheWind.WindowsReboot.Presentation.CustomControls;

namespace DustInTheWind.WindowsReboot.Presentation.MainWindow
{
    partial class ActionControl
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
            this.tableLayoutPanelActionStart = new System.Windows.Forms.TableLayoutPanel();
            this.buttonStopTimer = new ButtonWithCommand();
            this.buttonStartTimer = new ButtonWithCommand();
            this.tableLayoutPanelActionStart.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelActionStart
            // 
            this.tableLayoutPanelActionStart.ColumnCount = 1;
            this.tableLayoutPanelActionStart.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelActionStart.Controls.Add(this.buttonStopTimer, 0, 1);
            this.tableLayoutPanelActionStart.Controls.Add(this.buttonStartTimer, 0, 0);
            this.tableLayoutPanelActionStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelActionStart.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelActionStart.Name = "tableLayoutPanelActionStart";
            this.tableLayoutPanelActionStart.RowCount = 2;
            this.tableLayoutPanelActionStart.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelActionStart.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelActionStart.Size = new System.Drawing.Size(150, 150);
            this.tableLayoutPanelActionStart.TabIndex = 6;
            // 
            // buttonStopTimer
            // 
            this.buttonStopTimer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStopTimer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonStopTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStopTimer.Location = new System.Drawing.Point(4, 78);
            this.buttonStopTimer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 4);
            this.buttonStopTimer.Name = "buttonStopTimer";
            this.buttonStopTimer.Size = new System.Drawing.Size(142, 68);
            this.buttonStopTimer.TabIndex = 3;
            this.buttonStopTimer.Text = "Stop";
            this.buttonStopTimer.UseVisualStyleBackColor = true;
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
            this.buttonStartTimer.Size = new System.Drawing.Size(142, 68);
            this.buttonStartTimer.TabIndex = 2;
            this.buttonStartTimer.Text = "Start";
            this.buttonStartTimer.UseVisualStyleBackColor = true;
            // 
            // ActionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanelActionStart);
            this.Name = "ActionControl";
            this.tableLayoutPanelActionStart.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelActionStart;
        private ButtonWithCommand buttonStopTimer;
        private ButtonWithCommand buttonStartTimer;
    }
}
