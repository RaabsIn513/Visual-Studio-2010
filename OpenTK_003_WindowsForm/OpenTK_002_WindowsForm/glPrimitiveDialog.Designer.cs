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
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.button_VertexColor = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.UpDown_VertextSize = new System.Windows.Forms.DomainUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button_LineColor = new System.Windows.Forms.Button();
            this.UpDown_LineWidth = new System.Windows.Forms.DomainUpDown();
            this.button_ObjectColor = new System.Windows.Forms.Button();
            this.colorDialog2 = new System.Windows.Forms.ColorDialog();
            this.colorDialog3 = new System.Windows.Forms.ColorDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.checkBox_showVerts.Location = new System.Drawing.Point(6, 19);
            this.checkBox_showVerts.Name = "checkBox_showVerts";
            this.checkBox_showVerts.Size = new System.Drawing.Size(93, 17);
            this.checkBox_showVerts.TabIndex = 2;
            this.checkBox_showVerts.Text = "show verticies";
            this.checkBox_showVerts.UseVisualStyleBackColor = true;
            // 
            // checkBox_showLines
            // 
            this.checkBox_showLines.AutoSize = true;
            this.checkBox_showLines.Location = new System.Drawing.Point(6, 19);
            this.checkBox_showLines.Name = "checkBox_showLines";
            this.checkBox_showLines.Size = new System.Drawing.Size(75, 17);
            this.checkBox_showLines.TabIndex = 3;
            this.checkBox_showLines.Text = "show lines";
            this.checkBox_showLines.UseVisualStyleBackColor = true;
            // 
            // button_VertexColor
            // 
            this.button_VertexColor.Location = new System.Drawing.Point(6, 42);
            this.button_VertexColor.Name = "button_VertexColor";
            this.button_VertexColor.Size = new System.Drawing.Size(109, 23);
            this.button_VertexColor.TabIndex = 4;
            this.button_VertexColor.Text = "Color";
            this.button_VertexColor.UseVisualStyleBackColor = true;
            this.button_VertexColor.Click += new System.EventHandler(this.button_VertexColor_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.UpDown_VertextSize);
            this.groupBox1.Controls.Add(this.checkBox_showVerts);
            this.groupBox1.Controls.Add(this.button_VertexColor);
            this.groupBox1.Location = new System.Drawing.Point(12, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(141, 113);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Vertex Options";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Size";
            // 
            // UpDown_VertextSize
            // 
            this.UpDown_VertextSize.Items.Add("1.0");
            this.UpDown_VertextSize.Items.Add("1.5");
            this.UpDown_VertextSize.Items.Add("2.0");
            this.UpDown_VertextSize.Items.Add("2.5");
            this.UpDown_VertextSize.Items.Add("3.0");
            this.UpDown_VertextSize.Items.Add("3.5");
            this.UpDown_VertextSize.Items.Add("4.0");
            this.UpDown_VertextSize.Items.Add("4.5");
            this.UpDown_VertextSize.Items.Add("5.0");
            this.UpDown_VertextSize.Items.Add("5.5");
            this.UpDown_VertextSize.Items.Add("6.0");
            this.UpDown_VertextSize.Location = new System.Drawing.Point(46, 71);
            this.UpDown_VertextSize.Name = "UpDown_VertextSize";
            this.UpDown_VertextSize.Size = new System.Drawing.Size(53, 20);
            this.UpDown_VertextSize.TabIndex = 5;
            this.UpDown_VertextSize.Text = "---";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.button_LineColor);
            this.groupBox2.Controls.Add(this.UpDown_LineWidth);
            this.groupBox2.Controls.Add(this.checkBox_showLines);
            this.groupBox2.Location = new System.Drawing.Point(159, 44);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(134, 113);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Line Options";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Size";
            // 
            // button_LineColor
            // 
            this.button_LineColor.Location = new System.Drawing.Point(6, 42);
            this.button_LineColor.Name = "button_LineColor";
            this.button_LineColor.Size = new System.Drawing.Size(122, 23);
            this.button_LineColor.TabIndex = 5;
            this.button_LineColor.Text = "Color";
            this.button_LineColor.UseVisualStyleBackColor = true;
            this.button_LineColor.Click += new System.EventHandler(this.button_LineColor_Click);
            // 
            // UpDown_LineWidth
            // 
            this.UpDown_LineWidth.Items.Add("1.0");
            this.UpDown_LineWidth.Items.Add("1.5");
            this.UpDown_LineWidth.Items.Add("2.0");
            this.UpDown_LineWidth.Items.Add("2.5");
            this.UpDown_LineWidth.Items.Add("3.0");
            this.UpDown_LineWidth.Items.Add("3.5");
            this.UpDown_LineWidth.Items.Add("4.0");
            this.UpDown_LineWidth.Items.Add("4.5");
            this.UpDown_LineWidth.Items.Add("5.0");
            this.UpDown_LineWidth.Items.Add("5.5");
            this.UpDown_LineWidth.Items.Add("6.0");
            this.UpDown_LineWidth.Location = new System.Drawing.Point(39, 71);
            this.UpDown_LineWidth.Name = "UpDown_LineWidth";
            this.UpDown_LineWidth.Size = new System.Drawing.Size(53, 20);
            this.UpDown_LineWidth.TabIndex = 7;
            this.UpDown_LineWidth.Text = "---";
            // 
            // button_ObjectColor
            // 
            this.button_ObjectColor.Location = new System.Drawing.Point(18, 12);
            this.button_ObjectColor.Name = "button_ObjectColor";
            this.button_ObjectColor.Size = new System.Drawing.Size(109, 23);
            this.button_ObjectColor.TabIndex = 7;
            this.button_ObjectColor.Text = "Color";
            this.button_ObjectColor.UseVisualStyleBackColor = true;
            this.button_ObjectColor.Click += new System.EventHandler(this.button_ObjectColor_Click);
            // 
            // glPrimitiveDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 222);
            this.Controls.Add(this.button_ObjectColor);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.button_Okay);
            this.Name = "glPrimitiveDialog";
            this.Text = "glPrimitiveDialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.glPrimitiveDialog_FormClosing);
            this.Load += new System.EventHandler(this.glPrimitiveDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_Okay;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBox_showVerts;
        private System.Windows.Forms.CheckBox checkBox_showLines;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button button_VertexColor;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_LineColor;
        private System.Windows.Forms.DomainUpDown UpDown_VertextSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DomainUpDown UpDown_LineWidth;
        private System.Windows.Forms.Button button_ObjectColor;
        private System.Windows.Forms.ColorDialog colorDialog2;
        private System.Windows.Forms.ColorDialog colorDialog3;

    }
}