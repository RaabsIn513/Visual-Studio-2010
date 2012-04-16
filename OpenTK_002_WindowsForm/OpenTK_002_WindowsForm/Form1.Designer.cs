namespace OpenTK_002_WindowsForm
{
    partial class Form1
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
            this.glControl1 = new OpenTK.GLControl();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLine = new System.Windows.Forms.Button();
            this.btnPolyLine = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnPoint = new System.Windows.Forms.Button();
            this.btnQuad = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showPointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLinesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLoopLine = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.ColumnHeader_ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeaderColor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // glControl1
            // 
            this.glControl1.AllowDrop = true;
            this.glControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.glControl1.Location = new System.Drawing.Point(183, 12);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(757, 375);
            this.glControl1.TabIndex = 0;
            this.glControl1.VSync = false;
            this.glControl1.Load += new System.EventHandler(this.glControl1_Load);
            this.glControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl1_Paint);
            this.glControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glControl1_KeyDown);
            this.glControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseDown);
            this.glControl1.MouseHover += new System.EventHandler(this.glControl1_MouseHover);
            this.glControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseMove);
            this.glControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseUp);
            this.glControl1.Resize += new System.EventHandler(this.glControl1_Resize);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(425, 390);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // btnLine
            // 
            this.btnLine.Location = new System.Drawing.Point(101, 36);
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(76, 23);
            this.btnLine.TabIndex = 6;
            this.btnLine.Text = "Line";
            this.btnLine.UseVisualStyleBackColor = true;
            this.btnLine.Click += new System.EventHandler(this.buttonLine_Click);
            // 
            // btnPolyLine
            // 
            this.btnPolyLine.Location = new System.Drawing.Point(102, 94);
            this.btnPolyLine.Name = "btnPolyLine";
            this.btnPolyLine.Size = new System.Drawing.Size(75, 23);
            this.btnPolyLine.TabIndex = 7;
            this.btnPolyLine.Text = "PolyLine";
            this.btnPolyLine.UseVisualStyleBackColor = true;
            this.btnPolyLine.Click += new System.EventHandler(this.buttonPolyLine_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(133, 181);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(44, 23);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // btnPoint
            // 
            this.btnPoint.Location = new System.Drawing.Point(102, 123);
            this.btnPoint.Name = "btnPoint";
            this.btnPoint.Size = new System.Drawing.Size(75, 23);
            this.btnPoint.TabIndex = 9;
            this.btnPoint.Text = "Point";
            this.btnPoint.UseVisualStyleBackColor = true;
            this.btnPoint.Click += new System.EventHandler(this.buttonPoint_Click);
            // 
            // btnQuad
            // 
            this.btnQuad.Location = new System.Drawing.Point(102, 152);
            this.btnQuad.Name = "btnQuad";
            this.btnQuad.Size = new System.Drawing.Size(75, 23);
            this.btnQuad.TabIndex = 10;
            this.btnQuad.Text = "Quad";
            this.btnQuad.UseVisualStyleBackColor = true;
            this.btnQuad.Click += new System.EventHandler(this.buttonQuad_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.showPointsToolStripMenuItem,
            this.showLinesToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(152, 70);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.deleteToolStripMenuItem.Text = "delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // showPointsToolStripMenuItem
            // 
            this.showPointsToolStripMenuItem.Name = "showPointsToolStripMenuItem";
            this.showPointsToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.showPointsToolStripMenuItem.Text = "show vertecies";
            this.showPointsToolStripMenuItem.Click += new System.EventHandler(this.showVerticiesToolStripMenuItem_Click);
            // 
            // showLinesToolStripMenuItem
            // 
            this.showLinesToolStripMenuItem.Name = "showLinesToolStripMenuItem";
            this.showLinesToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.showLinesToolStripMenuItem.Text = "show lines";
            // 
            // btnLoopLine
            // 
            this.btnLoopLine.Location = new System.Drawing.Point(102, 65);
            this.btnLoopLine.Name = "btnLoopLine";
            this.btnLoopLine.Size = new System.Drawing.Size(75, 23);
            this.btnLoopLine.TabIndex = 11;
            this.btnLoopLine.Text = "LoopLine";
            this.btnLoopLine.UseVisualStyleBackColor = true;
            this.btnLoopLine.Click += new System.EventHandler(this.buttonLoopLine_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(98, 181);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(29, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "deselect";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // listView1
            // 
            this.listView1.AllowColumnReorder = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader_ID,
            this.ColumnHeaderType,
            this.ColumnHeaderColor});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(4, 210);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(173, 177);
            this.listView1.TabIndex = 14;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // ColumnHeader_ID
            // 
            this.ColumnHeader_ID.Text = "ID";
            this.ColumnHeader_ID.Width = 36;
            // 
            // ColumnHeaderType
            // 
            this.ColumnHeaderType.Text = "Type";
            this.ColumnHeaderType.Width = 72;
            // 
            // ColumnHeaderColor
            // 
            this.ColumnHeaderColor.Text = "Color";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 412);
            this.Controls.Add(this.btnLine);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnLoopLine);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.btnPoint);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnQuad);
            this.Controls.Add(this.glControl1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnPolyLine);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenTK.GLControl glControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLine;
        private System.Windows.Forms.Button btnPolyLine;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnPoint;
        private System.Windows.Forms.Button btnQuad;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.Button btnLoopLine;
        private System.Windows.Forms.ToolStripMenuItem showPointsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showLinesToolStripMenuItem;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader ColumnHeader_ID;
        private System.Windows.Forms.ColumnHeader ColumnHeaderType;
        private System.Windows.Forms.ColumnHeader ColumnHeaderColor;
    }
}

