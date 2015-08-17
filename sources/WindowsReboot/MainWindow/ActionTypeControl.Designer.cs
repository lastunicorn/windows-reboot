namespace DustInTheWind.WindowsReboot.MainWindow
{
    partial class ActionTypeControl
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
            this.tableLayoutPanelActionType = new System.Windows.Forms.TableLayoutPanel();
            this.checkBoxForceAction = new System.Windows.Forms.CheckBox();
            this.comboBoxAction = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxDisplayActionWarning = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanelActionType.SuspendLayout();
            this.SuspendLayout();
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
            this.tableLayoutPanelActionType.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelActionType.Name = "tableLayoutPanelActionType";
            this.tableLayoutPanelActionType.RowCount = 3;
            this.tableLayoutPanelActionType.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelActionType.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelActionType.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelActionType.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelActionType.Size = new System.Drawing.Size(236, 73);
            this.tableLayoutPanelActionType.TabIndex = 19;
            // 
            // checkBoxForceAction
            // 
            this.checkBoxForceAction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxForceAction.AutoSize = true;
            this.checkBoxForceAction.Location = new System.Drawing.Point(72, 30);
            this.checkBoxForceAction.Name = "checkBoxForceAction";
            this.checkBoxForceAction.Size = new System.Drawing.Size(161, 17);
            this.checkBoxForceAction.TabIndex = 1;
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
            this.comboBoxAction.Size = new System.Drawing.Size(161, 21);
            this.comboBoxAction.TabIndex = 0;
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
            this.checkBoxDisplayActionWarning.Size = new System.Drawing.Size(161, 17);
            this.checkBoxDisplayActionWarning.TabIndex = 2;
            this.checkBoxDisplayActionWarning.Text = "Display warning";
            this.checkBoxDisplayActionWarning.UseVisualStyleBackColor = true;
            // 
            // ActionTypeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.tableLayoutPanelActionType);
            this.Name = "ActionTypeControl";
            this.Size = new System.Drawing.Size(236, 73);
            this.tableLayoutPanelActionType.ResumeLayout(false);
            this.tableLayoutPanelActionType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelActionType;
        private System.Windows.Forms.CheckBox checkBoxForceAction;
        private System.Windows.Forms.ComboBox comboBoxAction;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBoxDisplayActionWarning;
    }
}
