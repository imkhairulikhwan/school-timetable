namespace SchoolTimetable
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.label1 = new System.Windows.Forms.Label();
			this.txtTimetableName = new System.Windows.Forms.TextBox();
			this.btnRegisterTimetable = new System.Windows.Forms.Button();
			this.btnSelectTeacherListFile = new System.Windows.Forms.Button();
			this.txtTeacherList = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnImportTeacherList = new System.Windows.Forms.Button();
			this.btnSelectTimeslotFile = new System.Windows.Forms.Button();
			this.txtTimeslotList = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.btnGenerate = new System.Windows.Forms.Button();
			this.txtImportStatus = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.btnClear = new System.Windows.Forms.Button();
			this.btnImportTimeslot = new System.Windows.Forms.Button();
			this.btnRemoveTimeslot = new System.Windows.Forms.Button();
			this.btnRemoveTeacherList = new System.Windows.Forms.Button();
			this.btnFurtherView = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.cbChooseTeacher = new System.Windows.Forms.ComboBox();
			this.btnAnalyzeYear1 = new System.Windows.Forms.Button();
			this.btnAnalyzeYear2 = new System.Windows.Forms.Button();
			this.btnAnalyzeYear3 = new System.Windows.Forms.Button();
			this.btnAnalyzeYear6 = new System.Windows.Forms.Button();
			this.btnAnalyzeYear5 = new System.Windows.Forms.Button();
			this.btnAnalyzeYear4 = new System.Windows.Forms.Button();
			this.cbChooseYear = new System.Windows.Forms.ComboBox();
			this.cbChooseSubject = new System.Windows.Forms.ComboBox();
			this.cbChooseCurrentYear = new System.Windows.Forms.ComboBox();
			this.cbChoosePreviousYear = new System.Windows.Forms.ComboBox();
			this.cbChooseTimeslot = new System.Windows.Forms.ComboBox();
			this.cbChooseDay = new System.Windows.Forms.ComboBox();
			this.cbChooseClass = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(12, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(999, 500);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.btnRemoveTeacherList);
			this.tabPage1.Controls.Add(this.btnRemoveTimeslot);
			this.tabPage1.Controls.Add(this.btnImportTimeslot);
			this.tabPage1.Controls.Add(this.btnClear);
			this.tabPage1.Controls.Add(this.label6);
			this.tabPage1.Controls.Add(this.btnGenerate);
			this.tabPage1.Controls.Add(this.txtImportStatus);
			this.tabPage1.Controls.Add(this.label5);
			this.tabPage1.Controls.Add(this.btnSelectTimeslotFile);
			this.tabPage1.Controls.Add(this.txtTimeslotList);
			this.tabPage1.Controls.Add(this.label4);
			this.tabPage1.Controls.Add(this.btnImportTeacherList);
			this.tabPage1.Controls.Add(this.btnSelectTeacherListFile);
			this.tabPage1.Controls.Add(this.txtTeacherList);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Controls.Add(this.btnRegisterTimetable);
			this.tabPage1.Controls.Add(this.txtTimetableName);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Location = new System.Drawing.Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(991, 471);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "School Timetable";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.groupBox5);
			this.tabPage2.Controls.Add(this.groupBox3);
			this.tabPage2.Controls.Add(this.groupBox4);
			this.tabPage2.Controls.Add(this.groupBox2);
			this.tabPage2.Controls.Add(this.groupBox1);
			this.tabPage2.Controls.Add(this.btnFurtherView);
			this.tabPage2.Location = new System.Drawing.Point(4, 25);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(991, 471);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Analyze Output";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(138, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(111, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "Timetable Name";
			// 
			// txtTimetableName
			// 
			this.txtTimetableName.Location = new System.Drawing.Point(271, 30);
			this.txtTimetableName.Name = "txtTimetableName";
			this.txtTimetableName.Size = new System.Drawing.Size(187, 22);
			this.txtTimetableName.TabIndex = 1;
			// 
			// btnRegisterTimetable
			// 
			this.btnRegisterTimetable.Location = new System.Drawing.Point(512, 29);
			this.btnRegisterTimetable.Name = "btnRegisterTimetable";
			this.btnRegisterTimetable.Size = new System.Drawing.Size(121, 33);
			this.btnRegisterTimetable.TabIndex = 2;
			this.btnRegisterTimetable.Text = "Register";
			this.btnRegisterTimetable.UseVisualStyleBackColor = true;
			// 
			// btnSelectTeacherListFile
			// 
			this.btnSelectTeacherListFile.Location = new System.Drawing.Point(512, 141);
			this.btnSelectTeacherListFile.Name = "btnSelectTeacherListFile";
			this.btnSelectTeacherListFile.Size = new System.Drawing.Size(121, 28);
			this.btnSelectTeacherListFile.TabIndex = 5;
			this.btnSelectTeacherListFile.Text = "Select File";
			this.btnSelectTeacherListFile.UseVisualStyleBackColor = true;
			this.btnSelectTeacherListFile.Click += new System.EventHandler(this.btnSelectTeacherListFile_Click);
			// 
			// txtTeacherList
			// 
			this.txtTeacherList.Location = new System.Drawing.Point(271, 144);
			this.txtTeacherList.Name = "txtTeacherList";
			this.txtTeacherList.Size = new System.Drawing.Size(187, 22);
			this.txtTeacherList.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(162, 147);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(87, 17);
			this.label2.TabIndex = 3;
			this.label2.Text = "Teacher List";
			// 
			// btnImportTeacherList
			// 
			this.btnImportTeacherList.Location = new System.Drawing.Point(512, 175);
			this.btnImportTeacherList.Name = "btnImportTeacherList";
			this.btnImportTeacherList.Size = new System.Drawing.Size(121, 30);
			this.btnImportTeacherList.TabIndex = 8;
			this.btnImportTeacherList.Text = "Import Teacher";
			this.btnImportTeacherList.UseVisualStyleBackColor = true;
			// 
			// btnSelectTimeslotFile
			// 
			this.btnSelectTimeslotFile.Location = new System.Drawing.Point(512, 256);
			this.btnSelectTimeslotFile.Name = "btnSelectTimeslotFile";
			this.btnSelectTimeslotFile.Size = new System.Drawing.Size(121, 28);
			this.btnSelectTimeslotFile.TabIndex = 11;
			this.btnSelectTimeslotFile.Text = "Select File";
			this.btnSelectTimeslotFile.UseVisualStyleBackColor = true;
			this.btnSelectTimeslotFile.Click += new System.EventHandler(this.btnSelectTimeslotFile_Click);
			// 
			// txtTimeslotList
			// 
			this.txtTimeslotList.Location = new System.Drawing.Point(271, 260);
			this.txtTimeslotList.Name = "txtTimeslotList";
			this.txtTimeslotList.Size = new System.Drawing.Size(187, 22);
			this.txtTimeslotList.TabIndex = 10;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(162, 262);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(87, 17);
			this.label4.TabIndex = 9;
			this.label4.Text = "Timeslot List";
			// 
			// btnGenerate
			// 
			this.btnGenerate.Location = new System.Drawing.Point(265, 415);
			this.btnGenerate.Name = "btnGenerate";
			this.btnGenerate.Size = new System.Drawing.Size(121, 29);
			this.btnGenerate.TabIndex = 14;
			this.btnGenerate.Text = "Generate";
			this.btnGenerate.UseVisualStyleBackColor = true;
			// 
			// txtImportStatus
			// 
			this.txtImportStatus.Location = new System.Drawing.Point(225, 362);
			this.txtImportStatus.Name = "txtImportStatus";
			this.txtImportStatus.Size = new System.Drawing.Size(187, 22);
			this.txtImportStatus.TabIndex = 13;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(128, 365);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(91, 17);
			this.label5.TabIndex = 12;
			this.label5.Text = "Import Status";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(137, 110);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(71, 17);
			this.label6.TabIndex = 15;
			this.label6.Text = "Input data";
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(440, 415);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(92, 29);
			this.btnClear.TabIndex = 16;
			this.btnClear.Text = "Clear";
			this.btnClear.UseVisualStyleBackColor = true;
			// 
			// btnImportTimeslot
			// 
			this.btnImportTimeslot.Location = new System.Drawing.Point(512, 290);
			this.btnImportTimeslot.Name = "btnImportTimeslot";
			this.btnImportTimeslot.Size = new System.Drawing.Size(121, 36);
			this.btnImportTimeslot.TabIndex = 17;
			this.btnImportTimeslot.Text = "Import Timeslot";
			this.btnImportTimeslot.UseVisualStyleBackColor = true;
			// 
			// btnRemoveTimeslot
			// 
			this.btnRemoveTimeslot.Location = new System.Drawing.Point(650, 290);
			this.btnRemoveTimeslot.Name = "btnRemoveTimeslot";
			this.btnRemoveTimeslot.Size = new System.Drawing.Size(121, 36);
			this.btnRemoveTimeslot.TabIndex = 18;
			this.btnRemoveTimeslot.Text = "Remove";
			this.btnRemoveTimeslot.UseVisualStyleBackColor = true;
			// 
			// btnRemoveTeacherList
			// 
			this.btnRemoveTeacherList.Location = new System.Drawing.Point(650, 175);
			this.btnRemoveTeacherList.Name = "btnRemoveTeacherList";
			this.btnRemoveTeacherList.Size = new System.Drawing.Size(121, 30);
			this.btnRemoveTeacherList.TabIndex = 20;
			this.btnRemoveTeacherList.Text = "Remove";
			this.btnRemoveTeacherList.UseVisualStyleBackColor = true;
			this.btnRemoveTeacherList.Click += new System.EventHandler(this.button10_Click);
			// 
			// btnFurtherView
			// 
			this.btnFurtherView.Location = new System.Drawing.Point(30, 26);
			this.btnFurtherView.Name = "btnFurtherView";
			this.btnFurtherView.Size = new System.Drawing.Size(110, 28);
			this.btnFurtherView.TabIndex = 0;
			this.btnFurtherView.Text = "Further View";
			this.btnFurtherView.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.cbChooseTeacher);
			this.groupBox1.Location = new System.Drawing.Point(30, 93);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(332, 155);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Analysis Teacher Timetable";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.cbChooseDay);
			this.groupBox2.Controls.Add(this.cbChooseClass);
			this.groupBox2.Controls.Add(this.cbChooseTimeslot);
			this.groupBox2.Location = new System.Drawing.Point(30, 254);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(332, 166);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Analysis Sit In";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.cbChooseYear);
			this.groupBox3.Controls.Add(this.cbChooseSubject);
			this.groupBox3.Location = new System.Drawing.Point(388, 199);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(541, 100);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Analysis Subject";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.btnAnalyzeYear6);
			this.groupBox4.Controls.Add(this.btnAnalyzeYear5);
			this.groupBox4.Controls.Add(this.btnAnalyzeYear4);
			this.groupBox4.Controls.Add(this.btnAnalyzeYear3);
			this.groupBox4.Controls.Add(this.btnAnalyzeYear2);
			this.groupBox4.Controls.Add(this.btnAnalyzeYear1);
			this.groupBox4.Location = new System.Drawing.Point(388, 26);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(541, 167);
			this.groupBox4.TabIndex = 3;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Analysis Class Timetable";
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.cbChooseCurrentYear);
			this.groupBox5.Controls.Add(this.cbChoosePreviousYear);
			this.groupBox5.Location = new System.Drawing.Point(388, 320);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(541, 100);
			this.groupBox5.TabIndex = 4;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Analysis Comparison with Previous Year Timetable";
			// 
			// cbChooseTeacher
			// 
			this.cbChooseTeacher.FormattingEnabled = true;
			this.cbChooseTeacher.Items.AddRange(new object[] {
            "-Choose teacher-"});
			this.cbChooseTeacher.Location = new System.Drawing.Point(21, 46);
			this.cbChooseTeacher.Name = "cbChooseTeacher";
			this.cbChooseTeacher.Size = new System.Drawing.Size(182, 24);
			this.cbChooseTeacher.TabIndex = 1;
			this.cbChooseTeacher.Text = "-Choose Teacher-";
			// 
			// btnAnalyzeYear1
			// 
			this.btnAnalyzeYear1.Location = new System.Drawing.Point(70, 56);
			this.btnAnalyzeYear1.Name = "btnAnalyzeYear1";
			this.btnAnalyzeYear1.Size = new System.Drawing.Size(104, 30);
			this.btnAnalyzeYear1.TabIndex = 0;
			this.btnAnalyzeYear1.Text = "Year 1";
			this.btnAnalyzeYear1.UseVisualStyleBackColor = true;
			// 
			// btnAnalyzeYear2
			// 
			this.btnAnalyzeYear2.Location = new System.Drawing.Point(199, 56);
			this.btnAnalyzeYear2.Name = "btnAnalyzeYear2";
			this.btnAnalyzeYear2.Size = new System.Drawing.Size(104, 30);
			this.btnAnalyzeYear2.TabIndex = 1;
			this.btnAnalyzeYear2.Text = "Year 2";
			this.btnAnalyzeYear2.UseVisualStyleBackColor = true;
			// 
			// btnAnalyzeYear3
			// 
			this.btnAnalyzeYear3.Location = new System.Drawing.Point(322, 56);
			this.btnAnalyzeYear3.Name = "btnAnalyzeYear3";
			this.btnAnalyzeYear3.Size = new System.Drawing.Size(104, 30);
			this.btnAnalyzeYear3.TabIndex = 2;
			this.btnAnalyzeYear3.Text = "Year 3";
			this.btnAnalyzeYear3.UseVisualStyleBackColor = true;
			// 
			// btnAnalyzeYear6
			// 
			this.btnAnalyzeYear6.Location = new System.Drawing.Point(322, 107);
			this.btnAnalyzeYear6.Name = "btnAnalyzeYear6";
			this.btnAnalyzeYear6.Size = new System.Drawing.Size(104, 30);
			this.btnAnalyzeYear6.TabIndex = 5;
			this.btnAnalyzeYear6.Text = "Year 6";
			this.btnAnalyzeYear6.UseVisualStyleBackColor = true;
			// 
			// btnAnalyzeYear5
			// 
			this.btnAnalyzeYear5.Location = new System.Drawing.Point(199, 107);
			this.btnAnalyzeYear5.Name = "btnAnalyzeYear5";
			this.btnAnalyzeYear5.Size = new System.Drawing.Size(104, 30);
			this.btnAnalyzeYear5.TabIndex = 4;
			this.btnAnalyzeYear5.Text = "Year 5";
			this.btnAnalyzeYear5.UseVisualStyleBackColor = true;
			// 
			// btnAnalyzeYear4
			// 
			this.btnAnalyzeYear4.Location = new System.Drawing.Point(70, 107);
			this.btnAnalyzeYear4.Name = "btnAnalyzeYear4";
			this.btnAnalyzeYear4.Size = new System.Drawing.Size(104, 30);
			this.btnAnalyzeYear4.TabIndex = 3;
			this.btnAnalyzeYear4.Text = "Year 4";
			this.btnAnalyzeYear4.UseVisualStyleBackColor = true;
			// 
			// cbChooseYear
			// 
			this.cbChooseYear.FormattingEnabled = true;
			this.cbChooseYear.Items.AddRange(new object[] {
            "-Choose teacher-"});
			this.cbChooseYear.Location = new System.Drawing.Point(70, 46);
			this.cbChooseYear.Name = "cbChooseYear";
			this.cbChooseYear.Size = new System.Drawing.Size(182, 24);
			this.cbChooseYear.TabIndex = 2;
			this.cbChooseYear.Text = "-Choose Year-";
			// 
			// cbChooseSubject
			// 
			this.cbChooseSubject.FormattingEnabled = true;
			this.cbChooseSubject.Items.AddRange(new object[] {
            "-Choose teacher-"});
			this.cbChooseSubject.Location = new System.Drawing.Point(290, 46);
			this.cbChooseSubject.Name = "cbChooseSubject";
			this.cbChooseSubject.Size = new System.Drawing.Size(182, 24);
			this.cbChooseSubject.TabIndex = 3;
			this.cbChooseSubject.Text = "-Choose Subject-";
			// 
			// cbChooseCurrentYear
			// 
			this.cbChooseCurrentYear.FormattingEnabled = true;
			this.cbChooseCurrentYear.Items.AddRange(new object[] {
            "-Choose teacher-"});
			this.cbChooseCurrentYear.Location = new System.Drawing.Point(70, 42);
			this.cbChooseCurrentYear.Name = "cbChooseCurrentYear";
			this.cbChooseCurrentYear.Size = new System.Drawing.Size(182, 24);
			this.cbChooseCurrentYear.TabIndex = 4;
			this.cbChooseCurrentYear.Text = "-Choose Current Year-";
			this.cbChooseCurrentYear.SelectedIndexChanged += new System.EventHandler(this.comboBox4_SelectedIndexChanged);
			// 
			// cbChoosePreviousYear
			// 
			this.cbChoosePreviousYear.FormattingEnabled = true;
			this.cbChoosePreviousYear.Items.AddRange(new object[] {
            "-Choose teacher-"});
			this.cbChoosePreviousYear.Location = new System.Drawing.Point(290, 42);
			this.cbChoosePreviousYear.Name = "cbChoosePreviousYear";
			this.cbChoosePreviousYear.Size = new System.Drawing.Size(182, 24);
			this.cbChoosePreviousYear.TabIndex = 5;
			this.cbChoosePreviousYear.Text = "-Choose Previous Year-";
			// 
			// cbChooseTimeslot
			// 
			this.cbChooseTimeslot.FormattingEnabled = true;
			this.cbChooseTimeslot.Items.AddRange(new object[] {
            "-Choose teacher-"});
			this.cbChooseTimeslot.Location = new System.Drawing.Point(129, 120);
			this.cbChooseTimeslot.Name = "cbChooseTimeslot";
			this.cbChooseTimeslot.Size = new System.Drawing.Size(182, 24);
			this.cbChooseTimeslot.TabIndex = 8;
			this.cbChooseTimeslot.Text = "-Choose Timeslot-";
			// 
			// cbChooseDay
			// 
			this.cbChooseDay.FormattingEnabled = true;
			this.cbChooseDay.Items.AddRange(new object[] {
            "-Choose teacher-"});
			this.cbChooseDay.Location = new System.Drawing.Point(129, 85);
			this.cbChooseDay.Name = "cbChooseDay";
			this.cbChooseDay.Size = new System.Drawing.Size(182, 24);
			this.cbChooseDay.TabIndex = 6;
			this.cbChooseDay.Text = "-Choose Day-";
			// 
			// cbChooseClass
			// 
			this.cbChooseClass.FormattingEnabled = true;
			this.cbChooseClass.Items.AddRange(new object[] {
            "-Choose teacher-"});
			this.cbChooseClass.Location = new System.Drawing.Point(129, 55);
			this.cbChooseClass.Name = "cbChooseClass";
			this.cbChooseClass.Size = new System.Drawing.Size(182, 24);
			this.cbChooseClass.TabIndex = 7;
			this.cbChooseClass.Text = "-Choose Class-";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(18, 58);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(42, 17);
			this.label3.TabIndex = 9;
			this.label3.Text = "Class";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(18, 88);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(33, 17);
			this.label7.TabIndex = 10;
			this.label7.Text = "Day";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(18, 123);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(61, 17);
			this.label8.TabIndex = 11;
			this.label8.Text = "Timeslot";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1023, 524);
			this.Controls.Add(this.tabControl1);
			this.Name = "Form1";
			this.Text = "School Timetable Simulator";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.ResumeLayout(false);

        }

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Button btnRemoveTeacherList;
		private System.Windows.Forms.Button btnRemoveTimeslot;
		private System.Windows.Forms.Button btnImportTimeslot;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button btnGenerate;
		private System.Windows.Forms.TextBox txtImportStatus;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnSelectTimeslotFile;
		private System.Windows.Forms.TextBox txtTimeslotList;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnImportTeacherList;
		private System.Windows.Forms.Button btnSelectTeacherListFile;
		private System.Windows.Forms.TextBox txtTeacherList;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnRegisterTimetable;
		private System.Windows.Forms.TextBox txtTimetableName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.ComboBox cbChooseCurrentYear;
		private System.Windows.Forms.ComboBox cbChoosePreviousYear;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.ComboBox cbChooseYear;
		private System.Windows.Forms.ComboBox cbChooseSubject;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Button btnAnalyzeYear6;
		private System.Windows.Forms.Button btnAnalyzeYear5;
		private System.Windows.Forms.Button btnAnalyzeYear4;
		private System.Windows.Forms.Button btnAnalyzeYear3;
		private System.Windows.Forms.Button btnAnalyzeYear2;
		private System.Windows.Forms.Button btnAnalyzeYear1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cbChooseDay;
		private System.Windows.Forms.ComboBox cbChooseClass;
		private System.Windows.Forms.ComboBox cbChooseTimeslot;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox cbChooseTeacher;
		private System.Windows.Forms.Button btnFurtherView;
	}
}

