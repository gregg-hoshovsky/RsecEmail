namespace EmailAddin
{
    partial class AppListFrm
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
            this.label12 = new System.Windows.Forms.Label();
            this.datelistBox1 = new System.Windows.Forms.ListBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.texFrom = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.import = new System.Windows.Forms.Button();
            this.exportResultsList = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.emailBody = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.emailSubjectLine = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.testDataGrid = new System.Windows.Forms.DataGridView();
            this.testDate = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.selectedApp = new System.Windows.Forms.ComboBox();
            this.done = new System.Windows.Forms.Button();
            this.get_test_data = new System.Windows.Forms.Button();
            this.add_to_list = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.booleanListBox = new System.Windows.Forms.ListBox();
            this.userListBox = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.choiceListBox = new System.Windows.Forms.ListBox();
            this.SharePointList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.retrieveListBtn = new System.Windows.Forms.Button();
            this.CAMLTxtBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.secondaryListBox = new System.Windows.Forms.ListBox();
            this.label16 = new System.Windows.Forms.Label();
            this.secondaryList = new System.Windows.Forms.TextBox();
            this.primaryComboBox = new System.Windows.Forms.ComboBox();
            this.sendCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.testDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(548, 64);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(120, 13);
            this.label12.TabIndex = 72;
            this.label12.Text = "Date Fields ( Choose 1) ";
            // 
            // datelistBox1
            // 
            this.datelistBox1.FormattingEnabled = true;
            this.datelistBox1.Location = new System.Drawing.Point(548, 83);
            this.datelistBox1.Name = "datelistBox1";
            this.datelistBox1.Size = new System.Drawing.Size(155, 173);
            this.datelistBox1.TabIndex = 71;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(723, 206);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 35;
            this.dataGridView2.Size = new System.Drawing.Size(208, 118);
            this.dataGridView2.TabIndex = 70;
            // 
            // texFrom
            // 
            this.texFrom.Location = new System.Drawing.Point(510, 263);
            this.texFrom.Name = "texFrom";
            this.texFrom.Size = new System.Drawing.Size(179, 20);
            this.texFrom.TabIndex = 69;
            this.texFrom.Text = "App <noreply@dignithyhealth.org>";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(474, 270);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 68;
            this.label2.Text = "From";
            // 
            // import
            // 
            this.import.Location = new System.Drawing.Point(1135, 407);
            this.import.Name = "import";
            this.import.Size = new System.Drawing.Size(88, 23);
            this.import.TabIndex = 67;
            this.import.Text = "import";
            this.import.UseVisualStyleBackColor = true;
            this.import.Click += new System.EventHandler(this.import_Click);
            // 
            // exportResultsList
            // 
            this.exportResultsList.FormattingEnabled = true;
            this.exportResultsList.Location = new System.Drawing.Point(956, 436);
            this.exportResultsList.Name = "exportResultsList";
            this.exportResultsList.Size = new System.Drawing.Size(267, 160);
            this.exportResultsList.TabIndex = 66;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(956, 407);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 23);
            this.button1.TabIndex = 65;
            this.button1.Text = "Export";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // emailBody
            // 
            this.emailBody.Location = new System.Drawing.Point(108, 300);
            this.emailBody.Multiline = true;
            this.emailBody.Name = "emailBody";
            this.emailBody.Size = new System.Drawing.Size(360, 171);
            this.emailBody.TabIndex = 64;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(34, 300);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 13);
            this.label10.TabIndex = 63;
            this.label10.Text = "Email Body";
            // 
            // emailSubjectLine
            // 
            this.emailSubjectLine.Location = new System.Drawing.Point(108, 263);
            this.emailSubjectLine.Name = "emailSubjectLine";
            this.emailSubjectLine.Size = new System.Drawing.Size(360, 20);
            this.emailSubjectLine.TabIndex = 62;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 263);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 61;
            this.label6.Text = "Email Subject";
            // 
            // testDataGrid
            // 
            this.testDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.testDataGrid.Location = new System.Drawing.Point(37, 493);
            this.testDataGrid.Name = "testDataGrid";
            this.testDataGrid.Size = new System.Drawing.Size(900, 189);
            this.testDataGrid.TabIndex = 60;
            // 
            // testDate
            // 
            this.testDate.Location = new System.Drawing.Point(742, 396);
            this.testDate.Name = "testDate";
            this.testDate.Size = new System.Drawing.Size(200, 20);
            this.testDate.TabIndex = 59;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(688, 376);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(178, 13);
            this.label11.TabIndex = 58;
            this.label11.Text = "Set to a date for testing/debugging. ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(688, 396);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 57;
            this.label3.Text = "TestDate";
            // 
            // selectedApp
            // 
            this.selectedApp.FormattingEnabled = true;
            this.selectedApp.Location = new System.Drawing.Point(221, 26);
            this.selectedApp.Name = "selectedApp";
            this.selectedApp.Size = new System.Drawing.Size(426, 21);
            this.selectedApp.TabIndex = 56;
            // 
            // done
            // 
            this.done.Location = new System.Drawing.Point(834, 21);
            this.done.Name = "done";
            this.done.Size = new System.Drawing.Size(108, 29);
            this.done.TabIndex = 55;
            this.done.Text = "Save/Done";
            this.done.UseVisualStyleBackColor = true;
            this.done.Click += new System.EventHandler(this.done_Click);
            // 
            // get_test_data
            // 
            this.get_test_data.Location = new System.Drawing.Point(691, 455);
            this.get_test_data.Name = "get_test_data";
            this.get_test_data.Size = new System.Drawing.Size(97, 29);
            this.get_test_data.TabIndex = 54;
            this.get_test_data.Text = "Get Test data";
            this.get_test_data.UseVisualStyleBackColor = true;
            this.get_test_data.Click += new System.EventHandler(this.get_test_data_Click);
            // 
            // add_to_list
            // 
            this.add_to_list.Location = new System.Drawing.Point(691, 21);
            this.add_to_list.Name = "add_to_list";
            this.add_to_list.Size = new System.Drawing.Size(129, 29);
            this.add_to_list.TabIndex = 53;
            this.add_to_list.Text = "Update/Add To List";
            this.add_to_list.UseVisualStyleBackColor = true;
            this.add_to_list.Click += new System.EventHandler(this.add_to_list_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(720, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(121, 13);
            this.label9.TabIndex = 52;
            this.label9.Text = "Choice Fields (Choose1)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(384, 67);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 13);
            this.label8.TabIndex = 51;
            this.label8.Text = "Active Fields (Max of 2)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(236, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 13);
            this.label7.TabIndex = 50;
            this.label7.Text = "User Fields ( Choose 1) ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(321, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 13);
            this.label5.TabIndex = 49;
            // 
            // booleanListBox
            // 
            this.booleanListBox.FormattingEnabled = true;
            this.booleanListBox.Location = new System.Drawing.Point(387, 83);
            this.booleanListBox.Name = "booleanListBox";
            this.booleanListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.booleanListBox.Size = new System.Drawing.Size(155, 173);
            this.booleanListBox.TabIndex = 48;
            this.booleanListBox.SelectedIndexChanged += new System.EventHandler(this.booleanListBox_SelectedIndexChanged);
            // 
            // userListBox
            // 
            this.userListBox.FormattingEnabled = true;
            this.userListBox.Location = new System.Drawing.Point(239, 83);
            this.userListBox.Name = "userListBox";
            this.userListBox.Size = new System.Drawing.Size(141, 173);
            this.userListBox.TabIndex = 47;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(720, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 46;
            this.label4.Text = "Choice list Values";
            // 
            // choiceListBox
            // 
            this.choiceListBox.FormattingEnabled = true;
            this.choiceListBox.Location = new System.Drawing.Point(723, 83);
            this.choiceListBox.Name = "choiceListBox";
            this.choiceListBox.Size = new System.Drawing.Size(208, 82);
            this.choiceListBox.TabIndex = 45;
            this.choiceListBox.SelectedIndexChanged += new System.EventHandler(this.choiceListBox_SelectedIndexChanged_1);
            // 
            // SharePointList
            // 
            this.SharePointList.FormattingEnabled = true;
            this.SharePointList.Location = new System.Drawing.Point(31, 83);
            this.SharePointList.Name = "SharePointList";
            this.SharePointList.Size = new System.Drawing.Size(202, 173);
            this.SharePointList.TabIndex = 44;
            this.SharePointList.SelectedIndexChanged += new System.EventHandler(this.SharePointList_SelectedIndexChanged);
            this.SharePointList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SharePointList_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 26);
            this.label1.TabIndex = 43;
            this.label1.Text = "SharepointList\n(right  click for  secondary fields)";
            // 
            // retrieveListBtn
            // 
            this.retrieveListBtn.Location = new System.Drawing.Point(31, 24);
            this.retrieveListBtn.Name = "retrieveListBtn";
            this.retrieveListBtn.Size = new System.Drawing.Size(116, 23);
            this.retrieveListBtn.TabIndex = 42;
            this.retrieveListBtn.Text = "Retrieve App  List";
            this.retrieveListBtn.UseVisualStyleBackColor = true;
            this.retrieveListBtn.Click += new System.EventHandler(this.retrieveListBtn_Click);
            // 
            // CAMLTxtBox
            // 
            this.CAMLTxtBox.Location = new System.Drawing.Point(937, 83);
            this.CAMLTxtBox.Multiline = true;
            this.CAMLTxtBox.Name = "CAMLTxtBox";
            this.CAMLTxtBox.Size = new System.Drawing.Size(298, 284);
            this.CAMLTxtBox.TabIndex = 73;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(934, 64);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(189, 13);
            this.label13.TabIndex = 74;
            this.label13.Text = "CAML Query <View><Query><Where>";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(934, 376);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(251, 13);
            this.label14.TabIndex = 75;
            this.label14.Text = "</View></Query></Where> *overrides Active fields";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(474, 300);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(77, 13);
            this.label15.TabIndex = 76;
            this.label15.Text = "Secondary List";
            // 
            // secondaryListBox
            // 
            this.secondaryListBox.FormattingEnabled = true;
            this.secondaryListBox.Location = new System.Drawing.Point(551, 363);
            this.secondaryListBox.Name = "secondaryListBox";
            this.secondaryListBox.Size = new System.Drawing.Size(120, 108);
            this.secondaryListBox.Sorted = true;
            this.secondaryListBox.TabIndex = 77;
            this.secondaryListBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.secondaryListBox_KeyDown);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(474, 327);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(78, 13);
            this.label16.TabIndex = 78;
            this.label16.Text = "Field to Join on";
            // 
            // secondaryList
            // 
            this.secondaryList.Location = new System.Drawing.Point(571, 297);
            this.secondaryList.Name = "secondaryList";
            this.secondaryList.ReadOnly = true;
            this.secondaryList.Size = new System.Drawing.Size(100, 20);
            this.secondaryList.TabIndex = 79;
            // 
            // primaryComboBox
            // 
            this.primaryComboBox.FormattingEnabled = true;
            this.primaryComboBox.Location = new System.Drawing.Point(571, 324);
            this.primaryComboBox.Name = "primaryComboBox";
            this.primaryComboBox.Size = new System.Drawing.Size(121, 21);
            this.primaryComboBox.TabIndex = 81;
            // 
            // sendCheckBox
            // 
            this.sendCheckBox.AutoSize = true;
            this.sendCheckBox.Location = new System.Drawing.Point(691, 432);
            this.sendCheckBox.Name = "sendCheckBox";
            this.sendCheckBox.Size = new System.Drawing.Size(157, 17);
            this.sendCheckBox.TabIndex = 82;
            this.sendCheckBox.Text = "Allow Sending of test emails";
            this.sendCheckBox.UseVisualStyleBackColor = true;
            // 
            // AppListFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1246, 711);
            this.Controls.Add(this.sendCheckBox);
            this.Controls.Add(this.primaryComboBox);
            this.Controls.Add(this.secondaryList);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.secondaryListBox);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.CAMLTxtBox);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.datelistBox1);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.texFrom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.import);
            this.Controls.Add(this.exportResultsList);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.emailBody);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.emailSubjectLine);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.testDataGrid);
            this.Controls.Add(this.testDate);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.selectedApp);
            this.Controls.Add(this.done);
            this.Controls.Add(this.get_test_data);
            this.Controls.Add(this.add_to_list);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.booleanListBox);
            this.Controls.Add(this.userListBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.choiceListBox);
            this.Controls.Add(this.SharePointList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.retrieveListBtn);
            this.Name = "AppListFrm";
            this.Text = "Email Addin Admin";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.testDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ListBox datelistBox1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TextBox texFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button import;
        private System.Windows.Forms.ListBox exportResultsList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox emailBody;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox emailSubjectLine;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView testDataGrid;
        private System.Windows.Forms.DateTimePicker testDate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox selectedApp;
        private System.Windows.Forms.Button done;
        private System.Windows.Forms.Button get_test_data;
        private System.Windows.Forms.Button add_to_list;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox booleanListBox;
        private System.Windows.Forms.ListBox userListBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox choiceListBox;
        private System.Windows.Forms.ListBox SharePointList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button retrieveListBtn;
        private System.Windows.Forms.TextBox CAMLTxtBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ListBox secondaryListBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox secondaryList;
        private System.Windows.Forms.ComboBox primaryComboBox;
        private System.Windows.Forms.CheckBox sendCheckBox;
    }
}