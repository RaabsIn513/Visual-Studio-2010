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
            this.btnClear = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button2 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.ColumnHeader_ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeaderColor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.bgw_vertexSnap = new System.ComponentModel.BackgroundWorker();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_vertexSnap = new System.Windows.Forms.CheckBox();
            this.tbar_rotateX = new System.Windows.Forms.TrackBar();
            this.tbar_rotateY = new System.Windows.Forms.TrackBar();
            this.tbar_rotateZ = new System.Windows.Forms.TrackBar();
            this.lab_XY_ctrl_coordLabel = new System.Windows.Forms.Label();
            this.lab_Zrotate = new System.Windows.Forms.Label();
            this.lab_Yrotate = new System.Windows.Forms.Label();
            this.lab_Xrotate = new System.Windows.Forms.Label();
            this.btn_HOME_coord = new System.Windows.Forms.Button();
            this.btn_2x4 = new System.Windows.Forms.Button();
            this.btn_move_obj = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbar_rotateX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbar_rotateY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbar_rotateZ)).BeginInit();
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
            this.glControl1.Location = new System.Drawing.Point(164, 12);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(812, 367);
            this.glControl1.TabIndex = 0;
            this.glControl1.VSync = false;
            this.glControl1.Load += new System.EventHandler(this.glControl1_Load);
            this.glControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl1_Paint);
            this.glControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glControl1_KeyDown);
            this.glControl1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.glControl1_KeyUp);
            this.glControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseDown);
            this.glControl1.MouseHover += new System.EventHandler(this.glControl1_MouseHover);
            this.glControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseMove);
            this.glControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseUp);
            this.glControl1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseWheel);
            this.glControl1.Resize += new System.EventHandler(this.glControl1_Resize);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(115, 385);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(118, 12);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(40, 23);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.moveToolStripMenuItem,
            this.copyToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(107, 70);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.deleteToolStripMenuItem.Text = "delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // moveToolStripMenuItem
            // 
            this.moveToolStripMenuItem.Name = "moveToolStripMenuItem";
            this.moveToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.moveToolStripMenuItem.Text = "move";
            this.moveToolStripMenuItem.Click += new System.EventHandler(this.moveToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.copyToolStripMenuItem.Text = "copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(4, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(58, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "deselect";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // listView1
            // 
            this.listView1.AllowColumnReorder = true;
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader_ID,
            this.ColumnHeaderName,
            this.ColumnHeaderColor});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(4, 66);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(154, 177);
            this.listView1.TabIndex = 14;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // ColumnHeader_ID
            // 
            this.ColumnHeader_ID.Text = "ID";
            this.ColumnHeader_ID.Width = 36;
            // 
            // ColumnHeaderName
            // 
            this.ColumnHeaderName.Text = "Name";
            this.ColumnHeaderName.Width = 51;
            // 
            // ColumnHeaderColor
            // 
            this.ColumnHeaderColor.Text = "Color";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(115, 408);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "label3";
            // 
            // bgw_vertexSnap
            // 
            this.bgw_vertexSnap.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_vertexSnap_DoWork);
            this.bgw_vertexSnap.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_vertexSnap_RunWorkerCompleted);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 385);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "FPS:";
            // 
            // cb_vertexSnap
            // 
            this.cb_vertexSnap.AutoSize = true;
            this.cb_vertexSnap.Location = new System.Drawing.Point(12, 41);
            this.cb_vertexSnap.Name = "cb_vertexSnap";
            this.cb_vertexSnap.Size = new System.Drawing.Size(81, 17);
            this.cb_vertexSnap.TabIndex = 19;
            this.cb_vertexSnap.Text = "vertex snap";
            this.cb_vertexSnap.UseVisualStyleBackColor = true;
            // 
            // tbar_rotateX
            // 
            this.tbar_rotateX.AccessibleName = "";
            this.tbar_rotateX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbar_rotateX.Location = new System.Drawing.Point(164, 385);
            this.tbar_rotateX.Maximum = 180;
            this.tbar_rotateX.Minimum = -180;
            this.tbar_rotateX.Name = "tbar_rotateX";
            this.tbar_rotateX.Size = new System.Drawing.Size(812, 45);
            this.tbar_rotateX.TabIndex = 20;
            this.tbar_rotateX.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.tbar_rotateX.ValueChanged += new System.EventHandler(this.tbar_rotateX_ValueChanged);
            // 
            // tbar_rotateY
            // 
            this.tbar_rotateY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbar_rotateY.Location = new System.Drawing.Point(961, 12);
            this.tbar_rotateY.Maximum = 180;
            this.tbar_rotateY.Minimum = -180;
            this.tbar_rotateY.Name = "tbar_rotateY";
            this.tbar_rotateY.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbar_rotateY.Size = new System.Drawing.Size(45, 367);
            this.tbar_rotateY.TabIndex = 21;
            this.tbar_rotateY.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.tbar_rotateY.ValueChanged += new System.EventHandler(this.tbar_rotateY_ValueChanged);
            // 
            // tbar_rotateZ
            // 
            this.tbar_rotateZ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbar_rotateZ.Location = new System.Drawing.Point(1001, 12);
            this.tbar_rotateZ.Maximum = 180;
            this.tbar_rotateZ.Minimum = -180;
            this.tbar_rotateZ.Name = "tbar_rotateZ";
            this.tbar_rotateZ.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbar_rotateZ.Size = new System.Drawing.Size(45, 367);
            this.tbar_rotateZ.TabIndex = 22;
            this.tbar_rotateZ.ValueChanged += new System.EventHandler(this.tbar_rotateZ_ValueChanged);
            // 
            // lab_XY_ctrl_coordLabel
            // 
            this.lab_XY_ctrl_coordLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lab_XY_ctrl_coordLabel.AutoSize = true;
            this.lab_XY_ctrl_coordLabel.Location = new System.Drawing.Point(1, 408);
            this.lab_XY_ctrl_coordLabel.Name = "lab_XY_ctrl_coordLabel";
            this.lab_XY_ctrl_coordLabel.Size = new System.Drawing.Size(79, 13);
            this.lab_XY_ctrl_coordLabel.TabIndex = 23;
            this.lab_XY_ctrl_coordLabel.Text = "Control Coords:";
            // 
            // lab_Zrotate
            // 
            this.lab_Zrotate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lab_Zrotate.AutoSize = true;
            this.lab_Zrotate.Location = new System.Drawing.Point(1, 366);
            this.lab_Zrotate.Name = "lab_Zrotate";
            this.lab_Zrotate.Size = new System.Drawing.Size(55, 13);
            this.lab_Zrotate.TabIndex = 24;
            this.lab_Zrotate.Text = "Z rotation:";
            // 
            // lab_Yrotate
            // 
            this.lab_Yrotate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lab_Yrotate.AutoSize = true;
            this.lab_Yrotate.Location = new System.Drawing.Point(1, 353);
            this.lab_Yrotate.Name = "lab_Yrotate";
            this.lab_Yrotate.Size = new System.Drawing.Size(55, 13);
            this.lab_Yrotate.TabIndex = 25;
            this.lab_Yrotate.Text = "Y rotation:";
            // 
            // lab_Xrotate
            // 
            this.lab_Xrotate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lab_Xrotate.AutoSize = true;
            this.lab_Xrotate.Location = new System.Drawing.Point(1, 340);
            this.lab_Xrotate.Name = "lab_Xrotate";
            this.lab_Xrotate.Size = new System.Drawing.Size(55, 13);
            this.lab_Xrotate.TabIndex = 26;
            this.lab_Xrotate.Text = "Z rotation:";
            // 
            // btn_HOME_coord
            // 
            this.btn_HOME_coord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_HOME_coord.Location = new System.Drawing.Point(971, 385);
            this.btn_HOME_coord.Name = "btn_HOME_coord";
            this.btn_HOME_coord.Size = new System.Drawing.Size(75, 23);
            this.btn_HOME_coord.TabIndex = 27;
            this.btn_HOME_coord.Text = "HOME";
            this.btn_HOME_coord.UseVisualStyleBackColor = true;
            this.btn_HOME_coord.Click += new System.EventHandler(this.btn_HOME_coord_Click);
            // 
            // btn_2x4
            // 
            this.btn_2x4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_2x4.Location = new System.Drawing.Point(4, 247);
            this.btn_2x4.Name = "btn_2x4";
            this.btn_2x4.Size = new System.Drawing.Size(75, 23);
            this.btn_2x4.TabIndex = 28;
            this.btn_2x4.Text = "buildWall";
            this.btn_2x4.UseVisualStyleBackColor = true;
            this.btn_2x4.Click += new System.EventHandler(this.btn_buildWall);
            // 
            // btn_move_obj
            // 
            this.btn_move_obj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_move_obj.Location = new System.Drawing.Point(4, 276);
            this.btn_move_obj.Name = "btn_move_obj";
            this.btn_move_obj.Size = new System.Drawing.Size(75, 23);
            this.btn_move_obj.TabIndex = 29;
            this.btn_move_obj.Text = "move";
            this.btn_move_obj.UseVisualStyleBackColor = true;
            this.btn_move_obj.Click += new System.EventHandler(this.btn_move_obj_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 430);
            this.Controls.Add(this.btn_move_obj);
            this.Controls.Add(this.btn_2x4);
            this.Controls.Add(this.btn_HOME_coord);
            this.Controls.Add(this.lab_Xrotate);
            this.Controls.Add(this.lab_Yrotate);
            this.Controls.Add(this.lab_Zrotate);
            this.Controls.Add(this.lab_XY_ctrl_coordLabel);
            this.Controls.Add(this.tbar_rotateZ);
            this.Controls.Add(this.cb_vertexSnap);
            this.Controls.Add(this.tbar_rotateY);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbar_rotateX);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.glControl1);
            this.Name = "Form1";
            this.Text = "Rob\'s OpenTK Drawing Program";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbar_rotateX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbar_rotateY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbar_rotateZ)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenTK.GLControl glControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader ColumnHeader_ID;
        private System.Windows.Forms.ColumnHeader ColumnHeaderName;
        private System.Windows.Forms.ColumnHeader ColumnHeaderColor;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker bgw_vertexSnap;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cb_vertexSnap;
        private System.Windows.Forms.ToolStripMenuItem moveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.TrackBar tbar_rotateX;
        private System.Windows.Forms.TrackBar tbar_rotateY;
        private System.Windows.Forms.TrackBar tbar_rotateZ;
        private System.Windows.Forms.Label lab_XY_ctrl_coordLabel;
        private System.Windows.Forms.Label lab_Zrotate;
        private System.Windows.Forms.Label lab_Yrotate;
        private System.Windows.Forms.Label lab_Xrotate;
        private System.Windows.Forms.Button btn_HOME_coord;
        private System.Windows.Forms.Button btn_2x4;
        private System.Windows.Forms.Button btn_move_obj;
    }
}

