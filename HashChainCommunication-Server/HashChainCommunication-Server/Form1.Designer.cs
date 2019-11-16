namespace HashChainCommunication_Server
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
            this.txtProcessorInfo = new System.Windows.Forms.RichTextBox();
            this.txtIncoming = new System.Windows.Forms.RichTextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtOutgoing = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnReKey = new System.Windows.Forms.Button();
            this.btnInitiateServer = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxServerPort = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtSystemMessage = new System.Windows.Forms.RichTextBox();
            this.btnNewReKey = new System.Windows.Forms.Button();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtProcessorInfo
            // 
            this.txtProcessorInfo.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtProcessorInfo.Location = new System.Drawing.Point(9, 20);
            this.txtProcessorInfo.Name = "txtProcessorInfo";
            this.txtProcessorInfo.ReadOnly = true;
            this.txtProcessorInfo.Size = new System.Drawing.Size(929, 125);
            this.txtProcessorInfo.TabIndex = 0;
            this.txtProcessorInfo.Text = "";
            // 
            // txtIncoming
            // 
            this.txtIncoming.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtIncoming.Location = new System.Drawing.Point(9, 19);
            this.txtIncoming.Name = "txtIncoming";
            this.txtIncoming.ReadOnly = true;
            this.txtIncoming.Size = new System.Drawing.Size(929, 163);
            this.txtIncoming.TabIndex = 0;
            this.txtIncoming.Text = "";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtIncoming);
            this.groupBox4.Location = new System.Drawing.Point(12, 525);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(944, 188);
            this.groupBox4.TabIndex = 27;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Incoming Message";
            // 
            // txtOutgoing
            // 
            this.txtOutgoing.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtOutgoing.Location = new System.Drawing.Point(9, 20);
            this.txtOutgoing.Name = "txtOutgoing";
            this.txtOutgoing.ReadOnly = true;
            this.txtOutgoing.Size = new System.Drawing.Size(929, 174);
            this.txtOutgoing.TabIndex = 0;
            this.txtOutgoing.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtOutgoing);
            this.groupBox3.Location = new System.Drawing.Point(12, 324);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(944, 200);
            this.groupBox3.TabIndex = 26;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Outgoing Message";
            // 
            // btnReKey
            // 
            this.btnReKey.Location = new System.Drawing.Point(502, 16);
            this.btnReKey.Name = "btnReKey";
            this.btnReKey.Size = new System.Drawing.Size(138, 45);
            this.btnReKey.TabIndex = 11;
            this.btnReKey.Text = "Re-Generate Hashchain";
            this.btnReKey.UseVisualStyleBackColor = true;
            this.btnReKey.Click += new System.EventHandler(this.btnReKey_Click);
            // 
            // btnInitiateServer
            // 
            this.btnInitiateServer.Location = new System.Drawing.Point(214, 16);
            this.btnInitiateServer.Name = "btnInitiateServer";
            this.btnInitiateServer.Size = new System.Drawing.Size(138, 45);
            this.btnInitiateServer.TabIndex = 8;
            this.btnInitiateServer.Text = "Initiate Server";
            this.btnInitiateServer.UseVisualStyleBackColor = true;
            this.btnInitiateServer.Click += new System.EventHandler(this.btnInitiateServer_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtProcessorInfo);
            this.groupBox5.Location = new System.Drawing.Point(12, 718);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(944, 151);
            this.groupBox5.TabIndex = 28;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Processor Information";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnNewReKey);
            this.groupBox1.Controls.Add(this.btnReKey);
            this.groupBox1.Controls.Add(this.textBoxServerPort);
            this.groupBox1.Controls.Add(this.btnInitiateServer);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(21, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(935, 74);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connect with Enrollment, Login and Disconnect";
            // 
            // textBoxServerPort
            // 
            this.textBoxServerPort.Location = new System.Drawing.Point(132, 23);
            this.textBoxServerPort.Name = "textBoxServerPort";
            this.textBoxServerPort.Size = new System.Drawing.Size(45, 20);
            this.textBoxServerPort.TabIndex = 10;
            this.textBoxServerPort.Text = "4545";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Server Port to Connect";
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Location = new System.Drawing.Point(239, 23);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(114, 24);
            this.btnSendMessage.TabIndex = 2;
            this.btnSendMessage.Text = "Send Message";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Message";
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(73, 26);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(160, 20);
            this.txtMessage.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSendMessage);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtMessage);
            this.groupBox2.Location = new System.Drawing.Point(20, 91);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(936, 57);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Send Message";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtSystemMessage);
            this.groupBox6.Location = new System.Drawing.Point(12, 163);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(944, 158);
            this.groupBox6.TabIndex = 27;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "System Message";
            // 
            // txtSystemMessage
            // 
            this.txtSystemMessage.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtSystemMessage.Location = new System.Drawing.Point(9, 20);
            this.txtSystemMessage.Name = "txtSystemMessage";
            this.txtSystemMessage.ReadOnly = true;
            this.txtSystemMessage.Size = new System.Drawing.Size(929, 131);
            this.txtSystemMessage.TabIndex = 0;
            this.txtSystemMessage.Text = "";
            // 
            // btnNewReKey
            // 
            this.btnNewReKey.Location = new System.Drawing.Point(358, 16);
            this.btnNewReKey.Name = "btnNewReKey";
            this.btnNewReKey.Size = new System.Drawing.Size(138, 45);
            this.btnNewReKey.TabIndex = 12;
            this.btnNewReKey.Text = "ReKey";
            this.btnNewReKey.UseVisualStyleBackColor = true;
            this.btnNewReKey.Click += new System.EventHandler(this.btnNewReKey_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 881);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Location = new System.Drawing.Point(500, 200);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtProcessorInfo;
        private System.Windows.Forms.RichTextBox txtIncoming;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RichTextBox txtOutgoing;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnReKey;
        private System.Windows.Forms.Button btnInitiateServer;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxServerPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSendMessage;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RichTextBox txtSystemMessage;
        private System.Windows.Forms.Button btnNewReKey;
    }
}

