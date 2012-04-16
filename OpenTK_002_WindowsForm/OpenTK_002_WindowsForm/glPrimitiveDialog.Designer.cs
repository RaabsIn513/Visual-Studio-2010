namespace OpenTK_002_WindowsForm
{
    partial class glPrimitiveDialog
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
            this.button_Okay = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBox_showVerts = new System.Windows.Forms.CheckBox();
            this.checkBox_showLines = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button_Okay
            // 
            this.button_Okay.Location = new System.Drawing.Point(278, 187);
            this.button_Okay.Name = "button_Okay";
            this.button_Okay.Size = new System.Drawing.Size(75, 23);
            this.button_Okay.TabIndex = 0;
            this.button_Okay.Text = "Okay";
            this.button_Okay.UseVisualStyleBackColor = true;
            this.button_Okay.Click += new System.EventHandler(this.buttonOkay_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(12, 187);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // checkBox_showVerts
            // 
            this.checkBox_showVerts.AutoSize = true;
            this.checkBox_showVerts.Location = new System.Drawing.Point(12, 74);
            this.checkBox_showVerts.Name = "checkBox_showVerts";
            this.checkBox_showVerts.Size = new System.Drawing.Size(93, 17);
            this.checkBox_showVerts.TabIndex = 2;
            this.checkBox_showVerts.Text = "show verticies";
            this.checkBox_showVerts.UseVisualStyleBackColor = true;
            // 
            // checkBox_showLines
            // 
            this.checkBox_showLines.AutoSize = true;
            this.checkBox_showLines.Location = new System.Drawing.Point(12, 97);
            this.checkBox_showLines.Name = "checkBox_showLines";
            this.checkBox_showLines.Size = new System.Drawing.Size(75, 17);
            this.checkBox_showLines.TabIndex = 3;
            this.checkBox_showLines.Text = "show lines";
            this.checkBox_showLines.UseVisualStyleBackColor = true;
            // 
            // glPrimitiveDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 222);
            this.Controls.Add(this.checkBox_showLines);
            this.Controls.Add(this.checkBox_showVerts);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.button_Okay);
            this.Name = "glPrimitiveDialog";
            this.Text = "glPrimitiveDialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.glPrimitiveDialog_FormClosing);
            this.Load += new System.EventHandler(this.glPrimitiveDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Okay;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBox_showVerts;
        private System.Windows.Forms.CheckBox checkBox_showLines;

    }
}