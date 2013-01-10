namespace NetduinoHostProject
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
            this.gBox_ClientMess = new System.Windows.Forms.GroupBox();
            this.btn_SendMessage = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dgv_clientMess = new System.Windows.Forms.DataGridView();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lab_status = new System.Windows.Forms.Label();
            this.btn_StartServer = new System.Windows.Forms.Button();
            this.lab_statMessage = new System.Windows.Forms.Label();
            this.gBox_ClientMess.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_clientMess)).BeginInit();
            this.SuspendLayout();
            // 
            // gBox_ClientMess
            // 
            this.gBox_ClientMess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gBox_ClientMess.Controls.Add(this.btn_SendMessage);
            this.gBox_ClientMess.Controls.Add(this.textBox1);
            this.gBox_ClientMess.Controls.Add(this.dgv_clientMess);
            this.gBox_ClientMess.Location = new System.Drawing.Point(12, 28);
            this.gBox_ClientMess.Name = "gBox_ClientMess";
            this.gBox_ClientMess.Size = new System.Drawing.Size(648, 245);
            this.gBox_ClientMess.TabIndex = 0;
            this.gBox_ClientMess.TabStop = false;
            this.gBox_ClientMess.Text = "Client Messages";
            // 
            // btn_SendMessage
            // 
            this.btn_SendMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_SendMessage.Location = new System.Drawing.Point(559, 216);
            this.btn_SendMessage.Name = "btn_SendMessage";
            this.btn_SendMessage.Size = new System.Drawing.Size(83, 23);
            this.btn_SendMessage.TabIndex = 2;
            this.btn_SendMessage.Text = "Send";
            this.btn_SendMessage.UseVisualStyleBackColor = true;
            this.btn_SendMessage.Click += new System.EventHandler(this.btn_SendMessage_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(6, 218);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(547, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // dgv_clientMess
            // 
            this.dgv_clientMess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_clientMess.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_clientMess.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Time,
            this.Message});
            this.dgv_clientMess.Location = new System.Drawing.Point(6, 19);
            this.dgv_clientMess.Name = "dgv_clientMess";
            this.dgv_clientMess.Size = new System.Drawing.Size(636, 191);
            this.dgv_clientMess.TabIndex = 0;
            // 
            // Time
            // 
            this.Time.HeaderText = "Time";
            this.Time.Name = "Time";
            // 
            // Message
            // 
            this.Message.HeaderText = "Message";
            this.Message.Name = "Message";
            // 
            // lab_status
            // 
            this.lab_status.AutoSize = true;
            this.lab_status.Location = new System.Drawing.Point(15, 13);
            this.lab_status.Name = "lab_status";
            this.lab_status.Size = new System.Drawing.Size(40, 13);
            this.lab_status.TabIndex = 1;
            this.lab_status.Text = "Status:";
            // 
            // btn_StartServer
            // 
            this.btn_StartServer.Location = new System.Drawing.Point(427, 8);
            this.btn_StartServer.Name = "btn_StartServer";
            this.btn_StartServer.Size = new System.Drawing.Size(75, 23);
            this.btn_StartServer.TabIndex = 2;
            this.btn_StartServer.Text = "StartServer";
            this.btn_StartServer.UseVisualStyleBackColor = true;
            this.btn_StartServer.Click += new System.EventHandler(this.button1_Click);
            // 
            // lab_statMessage
            // 
            this.lab_statMessage.AutoSize = true;
            this.lab_statMessage.Location = new System.Drawing.Point(61, 13);
            this.lab_statMessage.Name = "lab_statMessage";
            this.lab_statMessage.Size = new System.Drawing.Size(87, 13);
            this.lab_statMessage.TabIndex = 3;
            this.lab_statMessage.Text = "lab_statMessage";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 285);
            this.Controls.Add(this.lab_statMessage);
            this.Controls.Add(this.btn_StartServer);
            this.Controls.Add(this.lab_status);
            this.Controls.Add(this.gBox_ClientMess);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gBox_ClientMess.ResumeLayout(false);
            this.gBox_ClientMess.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_clientMess)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gBox_ClientMess;
        private System.Windows.Forms.DataGridView dgv_clientMess;
        private System.Windows.Forms.Label lab_status;
        private System.Windows.Forms.Button btn_StartServer;
        private System.Windows.Forms.Label lab_statMessage;
        private System.Windows.Forms.Button btn_SendMessage;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Message;
    }
}

