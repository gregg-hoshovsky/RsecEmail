namespace EmailAddin
{
    partial class SetupServer
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
            this.SaveBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SMTPServer = new System.Windows.Forms.TextBox();
            this.SMTPUserId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SMTPPwd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SMTPPort = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.SMTPSsl = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.ConfigLocation = new System.Windows.Forms.TextBox();
            this.test_email_Btn = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.select_config_location_btn = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.ToEmailTest = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.testEmailBody = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // SaveBtn
            // 
            this.SaveBtn.Location = new System.Drawing.Point(753, 250);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(75, 23);
            this.SaveBtn.TabIndex = 0;
            this.SaveBtn.Text = "Save";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "SMTP Server";
            // 
            // SMTPServer
            // 
            this.SMTPServer.Location = new System.Drawing.Point(112, 28);
            this.SMTPServer.Name = "SMTPServer";
            this.SMTPServer.Size = new System.Drawing.Size(136, 20);
            this.SMTPServer.TabIndex = 2;
            this.SMTPServer.Text = "smtp-relay.chw.edu";
            // 
            // SMTPUserId
            // 
            this.SMTPUserId.Location = new System.Drawing.Point(112, 54);
            this.SMTPUserId.Name = "SMTPUserId";
            this.SMTPUserId.Size = new System.Drawing.Size(136, 20);
            this.SMTPUserId.TabIndex = 4;
            this.SMTPUserId.Text = "SP_services";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "SMTP User Id";
            // 
            // SMTPPwd
            // 
            this.SMTPPwd.Location = new System.Drawing.Point(112, 80);
            this.SMTPPwd.Name = "SMTPPwd";
            this.SMTPPwd.Size = new System.Drawing.Size(136, 20);
            this.SMTPPwd.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "SMTP pwd";
            // 
            // SMTPPort
            // 
            this.SMTPPort.Location = new System.Drawing.Point(112, 106);
            this.SMTPPort.Name = "SMTPPort";
            this.SMTPPort.Size = new System.Drawing.Size(136, 20);
            this.SMTPPort.TabIndex = 8;
            this.SMTPPort.Text = "25";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "SMTP Port";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "SMTP SSL";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(198, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 13);
            this.label6.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(282, 106);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(126, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Current Application URLs";
            // 
            // SMTPSsl
            // 
            this.SMTPSsl.AutoSize = true;
            this.SMTPSsl.Location = new System.Drawing.Point(112, 134);
            this.SMTPSsl.Name = "SMTPSsl";
            this.SMTPSsl.Size = new System.Drawing.Size(137, 17);
            this.SMTPSsl.TabIndex = 20;
            this.SMTPSsl.Text = "Check if SSL is needed";
            this.SMTPSsl.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(282, 31);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(0, 13);
            this.label12.TabIndex = 24;
            // 
            // ConfigLocation
            // 
            this.ConfigLocation.Location = new System.Drawing.Point(425, 34);
            this.ConfigLocation.Name = "ConfigLocation";
            this.ConfigLocation.Size = new System.Drawing.Size(453, 20);
            this.ConfigLocation.TabIndex = 25;
            // 
            // test_email_Btn
            // 
            this.test_email_Btn.Location = new System.Drawing.Point(15, 167);
            this.test_email_Btn.Name = "test_email_Btn";
            this.test_email_Btn.Size = new System.Drawing.Size(81, 24);
            this.test_email_Btn.TabIndex = 26;
            this.test_email_Btn.Text = "Test Email";
            this.test_email_Btn.UseVisualStyleBackColor = true;
            this.test_email_Btn.Click += new System.EventHandler(this.button2_Click);
            // 
            // select_config_location_btn
            // 
            this.select_config_location_btn.Location = new System.Drawing.Point(281, 31);
            this.select_config_location_btn.Name = "select_config_location_btn";
            this.select_config_location_btn.Size = new System.Drawing.Size(127, 23);
            this.select_config_location_btn.TabIndex = 27;
            this.select_config_location_btn.Text = "Select Config location";
            this.select_config_location_btn.UseVisualStyleBackColor = true;
            this.select_config_location_btn.Click += new System.EventHandler(this.button3_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(109, 170);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(23, 13);
            this.label13.TabIndex = 28;
            this.label13.Text = "To:";
            // 
            // ToEmailTest
            // 
            this.ToEmailTest.Location = new System.Drawing.Point(152, 167);
            this.ToEmailTest.Name = "ToEmailTest";
            this.ToEmailTest.Size = new System.Drawing.Size(148, 20);
            this.ToEmailTest.TabIndex = 29;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(287, 75);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(104, 13);
            this.label14.TabIndex = 30;
            this.label14.Text = "Add Another App Url";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(425, 68);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(453, 20);
            this.textBox1.TabIndex = 31;
            this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(422, 106);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(456, 77);
            this.listView1.TabIndex = 32;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
            // 
            // testEmailBody
            // 
            this.testEmailBody.Location = new System.Drawing.Point(12, 197);
            this.testEmailBody.Multiline = true;
            this.testEmailBody.Name = "testEmailBody";
            this.testEmailBody.Size = new System.Drawing.Size(543, 76);
            this.testEmailBody.TabIndex = 33;
            // 
            // SetupServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 285);
            this.Controls.Add(this.testEmailBody);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.ToEmailTest);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.select_config_location_btn);
            this.Controls.Add(this.test_email_Btn);
            this.Controls.Add(this.ConfigLocation);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.SMTPSsl);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.SMTPPort);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.SMTPPwd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SMTPUserId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SMTPServer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SaveBtn);
            this.Name = "SetupServer";
            this.Text = "SetupServer";
            this.Load += new System.EventHandler(this.SetupServer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SMTPServer;
        private System.Windows.Forms.TextBox SMTPUserId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox SMTPPwd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox SMTPPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox SMTPSsl;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox ConfigLocation;
        private System.Windows.Forms.Button test_email_Btn;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button select_config_location_btn;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox ToEmailTest;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TextBox testEmailBody;
    }
}