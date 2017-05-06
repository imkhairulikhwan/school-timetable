using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolTimetable
{
    public partial class Form1 : Form
    {
        OpenFileDialog openFileDialog1;
        string teacherListFileName;
		int RowToSwitch;
		List<List<Group>> listGroupAndAllocation;
        List<DataGridView> AllYearsDataGrids;
        string selectedCBAnalysis;
		string selectedCBAnalysisValue1;
		string selectedCBAnalysisValue2;
		string selectedCBAnalysisValue3;
		
		public List<Teacher> publicResult { get; set; }
		public Boolean RepeatFurtherView { get; set; } = true;
		public string TimetableTeachersName { get; set; } = "teachers";

		public Form1()
        {
            InitializeComponent();

            Helper helper = new Helper();
            helper.CreateNewDatabase();
            helper.ConnectToDatabase();
            //helper.CreateTable();
            //helper.FillTable();
            //helper.PrintHighscores();

            openFileDialog1 = new OpenFileDialog();

        }

        #region Event handlers

        private void Form1_Load(object sender, EventArgs e)
        {
			//ConnectToDB();
			PopulateOutputGridView();
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }        

        private void btnSelectTeacherListFile_Click(object sender, EventArgs e)
        {
            BrowseFile();
        }



        private void btnSelectTimeslotFile_Click(object sender, EventArgs e)
        {
            BrowseFile();
        }

        #endregion

        #region Private methods

        private void PopulateOutputGridView()
        {
            DataGridViewRow row = (DataGridViewRow)dgvViewOutput.Rows[0].Clone();
            //row.Cells[0].Value = "Sunday";
            //row.Cells[1].Value = "ASM";
            //row.Cells[6].Value = "B";
            dgvViewOutput.Rows.Add(row);
            row = (DataGridViewRow)dgvViewOutput.Rows[0].Clone();
            //row.Cells[0].Value = "Monday";
            //row.Cells[6].Value = "R";
            dgvViewOutput.Rows.Add(row);
            row = (DataGridViewRow)dgvViewOutput.Rows[0].Clone();
            //row.Cells[0].Value = "Tuesday";
            //row.Cells[6].Value = "E";
            dgvViewOutput.Rows.Add(row);
            row = (DataGridViewRow)dgvViewOutput.Rows[0].Clone();
            //row.Cells[0].Value = "Wednesday";
            //row.Cells[6].Value = "A";
            dgvViewOutput.Rows.Add(row);
            row = (DataGridViewRow)dgvViewOutput.Rows[0].Clone();
            //row.Cells[0].Value = "Thursday";
            //row.Cells[6].Value = "K";
            dgvViewOutput.Rows.Add(row);
        }

        private void BrowseFile()
        {
            int size = -1;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                try
                {
                    txtTeacherList.Text = file;
                    string text = File.ReadAllText(file);
                    size = text.Length;
                    teacherListFileName = file;
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        #endregion

        private void btnImportTeacherList_Click(object sender, EventArgs e)
        {
			if(txtTeacherList.Text == string.Empty)
			{
				MessageBox.Show("Please select any file first");
				return;
			}

			ConnectToDB();
			ImportTeacherListIntoDB();
			PopulateAnalysisComboBox();
        }

		private void PopulateAnalysisComboBox()
		{
			SQLiteDataAdapter ad;
			DataTable dt = new DataTable();
			DataTable dt2 = new DataTable();

			//Fill teacher combobox in Constraints tab
			SQLiteCommand cmd;
			//m_dbConnection.Open();  //Initiate connection to the db
			cmd = m_dbConnection.CreateCommand();
			cmd.CommandText = $"SELECT DISTINCT TeacherName FROM {TimetableTeachersName}";  //set the passed query
			ad = new SQLiteDataAdapter(cmd);
			ad.Fill(dt); //fill the datasource
			cbTeacherAnalyzeOutputs.DataSource = dt;
			cbTeacherAnalyzeOutputs.DisplayMember = "TeacherName";
			cbTeacherAnalyzeOutputs.ValueMember = "TeacherName";

			//m_dbConnection.Open();  //Initiate connection to the db
			cmd = m_dbConnection.CreateCommand();
			cmd.CommandText = $"SELECT DISTINCT Subject FROM {TimetableTeachersName}";  //set the passed query
			ad = new SQLiteDataAdapter(cmd);
			ad.Fill(dt2); //fill the datasource
			cbChooseSubject.DataSource = dt2;
			cbChooseSubject.DisplayMember = "Subject";
			cbChooseSubject.ValueMember = "Subject";
		}

		private void ImportTimeslotListIntoDB()
		{
			MessageBox.Show("Successfully importing timeslot list into database");
			UpdateImportStatus(true);
		}

		private void UpdateImportStatus(bool isSuccessful)
		{
			if (isSuccessful)
			{
				txtImportStatus.Text = "Successful";
			}
			else
			{
				txtImportStatus.Text = "Fail";
			}			
		}

		// Holds our connection with the database
		SQLiteConnection m_dbConnection;

        private void ConnectToDB()
        {
            m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();
        }

        private void CreateTeacherTable()
        {
			if(String.IsNullOrEmpty(TimetableTeachersName))
			{
				TimetableTeachersName = "teachers";
			}

            string sql = $"create table if not exists {TimetableTeachersName} (TeacherName varchar(100), Standard varchar(20), Subject varchar(20), WeeklyCredit int, OddEven int)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

		private void CreateConstraintsTable()
		{
			string sql = "create table if not exists Constraints (TeacherName varchar(100), Subject varchar(20), BeforeAfterBreak int)";
			SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
			command.ExecuteNonQuery();
		}

		private void CreateGroupsTable()
		{
			ConnectToDB();
			string sql = "create table if not exists Groups (TeacherName varchar(100), Standard varchar(20), Subject varchar(20), OddEven int)";
			SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
			command.ExecuteNonQuery();
		}

		private void ImportTeacherListIntoDB()
        {
            if(String.IsNullOrEmpty(txtTeacherList.Text))
            {
                MessageBox.Show("Please select any file to import first!");
                return;
            }
			
            CreateTeacherTable();
			ClearTeacherTable();
			InsertRecordsIntoTeacherTable();			
		}

		private void ClearTeacherTable()
		{
			if (String.IsNullOrEmpty(TimetableTeachersName))
			{
				TimetableTeachersName = "teachers";
			}

			string sql = $"delete from {TimetableTeachersName}";
			SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
			command.ExecuteNonQuery();
		}

		private void ClearGroupsTable()
		{
			return; 

			string sql = $"delete from groups";
			SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
			command.ExecuteNonQuery();
		}

		private void InsertRecordsIntoTeacherTable()
        {
            if (String.IsNullOrEmpty(teacherListFileName))
            {
                MessageBox.Show("Please select any file to import first!");
                return;
            }

            try
            {
                List<Teacher> values = File.ReadAllLines(teacherListFileName)
                                          .Skip(1)
                                          .Select(v => Teacher.FromCsv(v))
                                          .ToList();

                foreach(Teacher teacher in values)
                {
					if (String.IsNullOrEmpty(TimetableTeachersName))
					{
						TimetableTeachersName = "teachers";
					}

					string sql = $"insert into {TimetableTeachersName} (TeacherName, Standard, Subject, WeeklyCredit, OddEven) values ('{teacher.TeacherName}'," +
                        $"'{teacher.Standard}', '{teacher.Subject}', {teacher.WeeklyCredit}, {Convert.ToInt32(teacher.OddEven)})";
                    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteNonQuery();
                }

				SQLiteDataAdapter ad;
				DataTable dt = new DataTable();
				DataTable dt2 = new DataTable();

				try
				{
					//Fill teacher combobox in Constraints tab
					SQLiteCommand cmd;
					//m_dbConnection.Open();  //Initiate connection to the db
					cmd = m_dbConnection.CreateCommand();

					if (String.IsNullOrEmpty(TimetableTeachersName))
					{
						TimetableTeachersName = "teachers";
					}

					cmd.CommandText = $"SELECT DISTINCT TeacherName FROM {TimetableTeachersName}";  //set the passed query
					ad = new SQLiteDataAdapter(cmd);
					ad.Fill(dt); //fill the datasource
					cbTeacherConstraints.DataSource = dt;
					cbTeacherConstraints.DisplayMember = "TeacherName";
					cbTeacherConstraints.ValueMember = "TeacherName";

					//Fill subject combobox in Constraints tab
					cmd = m_dbConnection.CreateCommand();
					cmd.CommandText = $"SELECT DISTINCT Subject FROM {TimetableTeachersName}";  //set the passed query
					ad = new SQLiteDataAdapter(cmd);
					ad.Fill(dt2); //fill the datasource
					cbSubjectListConstraints.DataSource = dt2;
					cbSubjectListConstraints.DisplayMember = "Subject";
					cbSubjectListConstraints.ValueMember = "Subject";

					//List<int> listToRemove = new List<int>();
					//for (int i = 0; i < cbSubjectListConstraints.Items.Count; i++)
					//{
					//	string value = cbSubjectListConstraints.GetItemText(cbSubjectListConstraints.Items[i]);
					//	if (string.IsNullOrEmpty(value))
					//		listToRemove.Add(i);							
					//}

					//foreach(int i in listToRemove)
					//{
					//	cbSubjectListConstraints.Items.RemoveAt(i);
					//}
				}
				catch (SQLiteException ex)
				{
					//Add your exception code here.
				}
				m_dbConnection.Close();

				MessageBox.Show("Successfully importing teacher list into database");
				UpdateImportStatus(true);
			}
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
				UpdateImportStatus(false);
			}
        }

		private void btnImportTimeslot_Click(object sender, EventArgs e)
		{
			if (txtTimeslotList.Text == string.Empty)
			{
				MessageBox.Show("Please select any file first");
				return;
			}

			ImportTimeslotListIntoDB();			
		}

		private void btnGenerateInitialData_Click(object sender, EventArgs e)
		{
			PopulateTeacherListInitialData();
			//PopulateTimeslotListInitialData();
		}

		private void PopulateTimeslotListInitialData()
		{
			throw new NotImplementedException();
		}

		private void PopulateTeacherListInitialData()
		{
			SQLiteDataAdapter ad;
			DataTable dt = new DataTable();
			if (String.IsNullOrEmpty(TimetableTeachersName))
			{
				TimetableTeachersName = "teachers";
			}

			try
			{
				//Fill teacher combobox in Constraints tab
				SQLiteCommand cmd;
				//m_dbConnection.Open();  //Initiate connection to the db
				cmd = m_dbConnection.CreateCommand();
				cmd.CommandText = $"SELECT * FROM {TimetableTeachersName}";  //set the passed query
				ad = new SQLiteDataAdapter(cmd);
				ad.Fill(dt); //fill the datasource
				dgvTeacherListInitial.DataSource = dt;				
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnExcludeConstraints_Click(object sender, EventArgs e)
		{
			MoveListBoxItems(lbIncludeConstraints, lbExcludeConstraints);
		}		

		private void MoveListBoxItems(ListBox source, ListBox destination)
		{
			ListBox.SelectedObjectCollection sourceItems = source.SelectedItems;
			foreach (var item in sourceItems)
			{
				destination.Items.Add(item);
			}
			while (source.SelectedItems.Count > 0)
			{
				source.Items.Remove(source.SelectedItems[0]);
			}
		}

		private void btnIncludeConstraints_Click(object sender, EventArgs e)
		{
			MoveListBoxItems(lbExcludeConstraints, lbIncludeConstraints);
		}

		private void btnApply_Click(object sender, EventArgs e)
		{
			if(cbTeacherConstraints.Text.Contains("Choose"))
			{
				MessageBox.Show("Please select any teacher first");
				return;
			}

			if (cbSubjectListConstraints.Text.Contains("Choose"))
			{
				MessageBox.Show("Please select any subject first");
				return;
			}

			//Insert into constraints table
			//ConnectToDB();
			CreateConstraintsTable();
			InsertIntoConstraintsTable();

			MessageBox.Show("Successfully applying constraints");
			txtConstraintsStatus.Text = "Successful";
		}

		private void InsertIntoConstraintsTable()
		{
			try
			{
				List<string> selectedFields = new List<string>();
				selectedFields.AddRange(lbIncludeConstraints.Items.OfType<string>());

				string sql = $"insert into Constraints (TeacherName, Subject, BeforeAfterBreak) values " +
					$"({cbTeacherConstraints.SelectedText}, {cbSubjectListConstraints.SelectedText}, {String.Join(",", selectedFields)})";
				SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
				command.ExecuteNonQuery();
			}
			catch (Exception ex)
			{

				throw;
			}
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		private void btnShowTimetableViewOutput_Click(object sender, EventArgs e)
		{
			AddGroupAndAllocation();
			
			if (String.IsNullOrEmpty(cbClassViewOutput.Text))
			{
				MessageBox.Show("Please select any class first");
				return;
			}

			ConnectToDB();
			//reset grid
			//dgvViewOutput.Rows.Clear();
			//dgvViewOutput.Refresh();
			//DataGridViewRow row = (DataGridViewRow)dgvViewOutput.Rows[0].Clone();
			//dgvViewOutput.Rows.Add(row);
			//row = (DataGridViewRow)dgvViewOutput.Rows[0].Clone();
			//dgvViewOutput.Rows.Add(row);
			//row = (DataGridViewRow)dgvViewOutput.Rows[0].Clone();
			//dgvViewOutput.Rows.Add(row);
			//row = (DataGridViewRow)dgvViewOutput.Rows[0].Clone();
			//dgvViewOutput.Rows.Add(row);
			//row = (DataGridViewRow)dgvViewOutput.Rows[0].Clone();
			//dgvViewOutput.Rows.Add(row);

            AllYearsDataGrids = new List<DataGridView>();
            for(int i = 0; i < 6; i ++)
            {
                var year = $"Y{i + 1}";
                //reset grid
                dgvViewOutput.Rows.Clear();
                dgvViewOutput.Refresh();
                DataGridViewRow row = (DataGridViewRow)dgvViewOutput.Rows[0].Clone();
                dgvViewOutput.Rows.Add(row);
                row = (DataGridViewRow)dgvViewOutput.Rows[0].Clone();
                dgvViewOutput.Rows.Add(row);
                row = (DataGridViewRow)dgvViewOutput.Rows[0].Clone();
                dgvViewOutput.Rows.Add(row);
                row = (DataGridViewRow)dgvViewOutput.Rows[0].Clone();
                dgvViewOutput.Rows.Add(row);
                row = (DataGridViewRow)dgvViewOutput.Rows[0].Clone();
                dgvViewOutput.Rows.Add(row);

                var dataGridClone = new DataGridView();
                dataGridClone.Columns.Add((DataGridViewColumn)dgvViewOutput.Columns[0].Clone());
                dataGridClone.Columns.Add((DataGridViewColumn)dgvViewOutput.Columns[1].Clone());
                dataGridClone.Columns.Add((DataGridViewColumn)dgvViewOutput.Columns[2].Clone());
                dataGridClone.Columns.Add((DataGridViewColumn)dgvViewOutput.Columns[3].Clone());
                dataGridClone.Columns.Add((DataGridViewColumn)dgvViewOutput.Columns[4].Clone());
                dataGridClone.Columns.Add((DataGridViewColumn)dgvViewOutput.Columns[5].Clone());
                dataGridClone.Columns.Add((DataGridViewColumn)dgvViewOutput.Columns[6].Clone());
                dataGridClone.Columns.Add((DataGridViewColumn)dgvViewOutput.Columns[7].Clone());
                dataGridClone.Columns.Add((DataGridViewColumn)dgvViewOutput.Columns[8].Clone());
                dataGridClone.Columns.Add((DataGridViewColumn)dgvViewOutput.Columns[9].Clone());
                dataGridClone.Columns.Add((DataGridViewColumn)dgvViewOutput.Columns[10].Clone());
                dataGridClone.Columns.Add((DataGridViewColumn)dgvViewOutput.Columns[11].Clone());
                dataGridClone.Rows.Add((DataGridViewRow)dgvViewOutput.Rows[0].Clone());
                dataGridClone.Rows.Add((DataGridViewRow)dgvViewOutput.Rows[0].Clone());
                dataGridClone.Rows.Add((DataGridViewRow)dgvViewOutput.Rows[0].Clone());
                dataGridClone.Rows.Add((DataGridViewRow)dgvViewOutput.Rows[0].Clone());
                dataGridClone.Rows.Add((DataGridViewRow)dgvViewOutput.Rows[0].Clone());

                PopulateTimetableBasedOnClass(year, dataGridClone);

                AllYearsDataGrids.Add(dataGridClone);
            }

            //We need to remove the teacher's name from the grid
            //RemoveTeachersName();
            //PopulateTimetableBasedOnClass(cbClassViewOutput.Text);
		}

        private void RemoveTeachersName()
        {
            throw new NotImplementedException();
        }

        private void AddGroupAndAllocation()
		{
			listGroupAndAllocation = new List<List<Group>>();
			//AddGroupOdd1();
			//AddGroupEven1();
			//AddGroupEven2();
			//AddGroupEven3();
		}

		private void AddGroupOdd1()
		{
			SQLiteDataAdapter ad;
			DataTable dt = new DataTable();

			try
			{
				SQLiteCommand cmd;
				//m_dbConnection.Open();  //Initiate connection to the db
				cmd = m_dbConnection.CreateCommand();
				if (String.IsNullOrEmpty(TimetableTeachersName))
				{
					TimetableTeachersName = "teachers";
				}

				cmd.CommandText = $"select teachername, standard, subject from {TimetableTeachersName} where oddeven = 1 group by teachername order by standard";  //set the passed query
				ad = new SQLiteDataAdapter(cmd);
				ad.Fill(dt); //fill the datasource
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			CreateGroupsTable();
			ClearGroupsTable();

			List<Group> GroupOdd1 = new List<Group>();

			string sameStandard = "";
			foreach(DataRow dr in dt.Rows)
			{
				//to ensure no duplicated standard is added
				if (sameStandard == dr["standard"].ToString())
					continue;

				//Create 1 Odd Group
				GroupOdd1.Add(new Group()
				{
					TeacherName = dr["TeacherName"].ToString(),
					Subject = dr["Subject"].ToString(),
					Standard = dr["Standard"].ToString(),
					Timeslot = 5,
					Day = 4
				});				

				string sql = $"insert into Groups (TeacherName, Standard, Subject, OddEven) values " +
					$"('{dr["TeacherName"].ToString()}'," +
						$"'{dr["Standard"].ToString()}', '{dr["Subject"].ToString()}', 1)";
				SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
				command.ExecuteNonQuery();

				sameStandard = dr["Standard"].ToString();			
			}			

			listGroupAndAllocation.Add(GroupOdd1);
		}

		private void AddGroupEven1()
		{
			SQLiteDataAdapter ad;
			DataTable dt = new DataTable();

			try
			{
				SQLiteCommand cmd;
				//m_dbConnection.Open();  //Initiate connection to the db
				cmd = m_dbConnection.CreateCommand();
				if (String.IsNullOrEmpty(TimetableTeachersName))
				{
					TimetableTeachersName = "teachers";
				}

				cmd.CommandText = $"select teachername, standard, subject from {TimetableTeachersName} where oddeven = 2 group by teachername order by standard";  //set the passed query
				ad = new SQLiteDataAdapter(cmd);
				ad.Fill(dt); //fill the datasource
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			CreateGroupsTable();
			ClearGroupsTable();

			List<Group> GroupOdd1 = new List<Group>();

			string sameStandard = "";
			foreach (DataRow dr in dt.Rows)
			{
				//to ensure no duplicated standard is added
				if (sameStandard == dr["standard"].ToString())
					continue;

				//Create 1 Odd Group
				GroupOdd1.Add(new Group()
				{
					TeacherName = dr["TeacherName"].ToString(),
					Subject = dr["Subject"].ToString(),
					Standard = dr["Standard"].ToString(),
					Timeslot = 2,
					Day = 3
				});

				string sql = $"insert into Groups (TeacherName, Standard, Subject, OddEven) values " +
					$"('{dr["TeacherName"].ToString()}'," +
						$"'{dr["Standard"].ToString()}', '{dr["Subject"].ToString()}', 2)";
				SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
				command.ExecuteNonQuery();

				sameStandard = dr["Standard"].ToString();
			}

			listGroupAndAllocation.Add(GroupOdd1);
		}

		private void AddGroupEven2()
		{
			SQLiteDataAdapter ad;
			DataTable dt = new DataTable();

			try
			{
				SQLiteCommand cmd;
				//m_dbConnection.Open();  //Initiate connection to the db
				cmd = m_dbConnection.CreateCommand();
				if (String.IsNullOrEmpty(TimetableTeachersName))
				{
					TimetableTeachersName = "teachers";
				}

				cmd.CommandText = $"select teachername, standard, subject from {TimetableTeachersName} where oddeven = 2 group by teachername order by standard";  //set the passed query
				ad = new SQLiteDataAdapter(cmd);
				ad.Fill(dt); //fill the datasource
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			CreateGroupsTable();
			ClearGroupsTable();

			List<Group> GroupOdd1 = new List<Group>();

			string sameStandard = "";
			foreach (DataRow dr in dt.Rows)
			{
				//to ensure no duplicated standard is added
				if (sameStandard == dr["standard"].ToString())
					continue;

				//Create 1 Odd Group
				GroupOdd1.Add(new Group()
				{
					TeacherName = dr["TeacherName"].ToString(),
					Subject = dr["Subject"].ToString(),
					Standard = dr["Standard"].ToString(),
					Timeslot = 7,
					Day = 2
				});

				string sql = $"insert into Groups (TeacherName, Standard, Subject, OddEven) values " +
					$"('{dr["TeacherName"].ToString()}'," +
						$"'{dr["Standard"].ToString()}', '{dr["Subject"].ToString()}', 2)";
				SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
				command.ExecuteNonQuery();

				sameStandard = dr["Standard"].ToString();
			}

			listGroupAndAllocation.Add(GroupOdd1);
		}

		private void AddGroupEven3()
		{
			SQLiteDataAdapter ad;
			DataTable dt = new DataTable();

			try
			{
				SQLiteCommand cmd;
				//m_dbConnection.Open();  //Initiate connection to the db
				cmd = m_dbConnection.CreateCommand();
				if (String.IsNullOrEmpty(TimetableTeachersName))
				{
					TimetableTeachersName = "teachers";
				}

				cmd.CommandText = $"select teachername, standard, subject from {TimetableTeachersName} where oddeven = 2 group by teachername order by standard";  //set the passed query
				ad = new SQLiteDataAdapter(cmd);
				ad.Fill(dt); //fill the datasource
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			CreateGroupsTable();
			ClearGroupsTable();

			List<Group> GroupOdd1 = new List<Group>();

			string sameStandard = "";
			foreach (DataRow dr in dt.Rows)
			{
				//to ensure no duplicated standard is added
				if (sameStandard == dr["standard"].ToString())
					continue;

				//Create 1 Odd Group
				GroupOdd1.Add(new Group()
				{
					TeacherName = dr["TeacherName"].ToString(),
					Subject = dr["Subject"].ToString(),
					Standard = dr["Standard"].ToString(),
					Timeslot = 2,
					Day = 4
				});

				string sql = $"insert into Groups (TeacherName, Standard, Subject, OddEven) values " +
					$"('{dr["TeacherName"].ToString()}'," +
						$"'{dr["Standard"].ToString()}', '{dr["Subject"].ToString()}', 2)";
				SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
				command.ExecuteNonQuery();

				sameStandard = dr["Standard"].ToString();
			}

			listGroupAndAllocation.Add(GroupOdd1);
		}

        /// <summary>
        /// Before populating to individual timeslot, need to check that it does not clash with the other years (for same teacher) 
        /// </summary>
        /// <param name="selectedClass"></param>
		private void PopulateTimetableBasedOnClass(string selectedClass, DataGridView dataGridView)
		{
			try
			{
				SQLiteDataAdapter ad;
				DataTable dt = new DataTable();

				//Fill teacher combobox in Constraints tab
				SQLiteCommand cmd;
				//m_dbConnection.Open();  //Initiate connection to the db
				cmd = m_dbConnection.CreateCommand();
				if (String.IsNullOrEmpty(TimetableTeachersName))
				{
					TimetableTeachersName = "teachers";
				}

				cmd.CommandText = $"select * from {TimetableTeachersName} where standard = '{selectedClass}' order by weeklycredit desc";
				ad = new SQLiteDataAdapter(cmd);
				ad.Fill(dt); //fill the datasource

				var result = (from rw in dt.AsEnumerable()
							  select new Teacher()
							  {
								  TeacherName = Convert.ToString(rw["TeacherName"]),
								  Standard = Convert.ToString(rw["Standard"]),
								  Subject = Convert.ToString(rw["Subject"]),
								  WeeklyCredit = Convert.ToInt32(rw["WeeklyCredit"]),
								  WeeklyUsedCredit = 0,
								  OddEven = (Timeslot)Convert.ToInt32(rw["OddEven"]),
								  ListOfTimeslot = new List<TimeslotDetails>()
							 }).ToList();

				int i = 0;
				int timeslot = 0;
				int day = 0;
				int teacherCount = result.Count;				

				int teacherCountDay4 = 100;
				int teacherCountDay3 = 100;
				int teacherCountDay2 = 100;
				int teacherCountDay1 = 100;

				bool repeat = true;
				int randomize = 0;

				string groupSubject = "";
                string teacherName = "";
				DataGridView dgvOutput =  FillUpGroupAndAllocation(selectedClass);
				bool alreadySetGroupCreditOnce = false;

				while(repeat)
				{
					//Sort the order randomly
					if (day % 2 == 0 && randomize != 2)
					{
						randomize++;
						/* x is even */
						result = result.OrderByDescending(a => a.WeeklyCredit).ToList();
					}
					else
					{
						randomize++;
						result = result.OrderBy(a => a.WeeklyCredit).ToList();
					}

					foreach (Teacher teacher in result)
					{
                        foreach(var groups in listGroupAndAllocation)
                        {
                            foreach(var group in groups)
                            {
                                if(group.Standard == selectedClass)
                                {
                                    //Check for group subject first, as it has been allocated already
                                    if (teacher.Subject == group.Subject && !alreadySetGroupCreditOnce && teacher.TeacherName == group.TeacherName)
                                    {
                                        alreadySetGroupCreditOnce = true;
                                        teacher.WeeklyUsedCredit++;
                                        continue;
                                    }
                                }
                            }
                        }						

						//it's end of week, don't add anymore into table
						if (day == 5)
						{
							repeat = false;
							break;
						}

						if(day == 4)
						{							
							teacherCountDay4--;

							if (teacherCountDay4 == 0)
							{
								//We let the next function to fulfill the timeslot
								repeat = false;
								break;
							}
						}

						if (day == 3)
						{
							teacherCountDay3--;

							if (teacherCountDay3 == 0)
							{
								//We let the next function to fulfill the timeslot
								repeat = false;
								break;
							}
						}

						if (day == 2)
						{
							teacherCountDay2--;

							if (teacherCountDay2 == 0)
							{
								//We let the next function to fulfill the timeslot
								repeat = false;
								break;
							}
						}

						if (day == 1)
						{
							teacherCountDay1--;

							if (teacherCountDay1 == 0)
							{
								//We let the next function to fulfill the timeslot
								repeat = false;
								break;
							}
						}

                        //DataGridViewRow row = (DataGridViewRow)dgvViewOutput.Rows[day];
                        DataGridViewRow row = dataGridView.Rows[day];

                        if (timeslot == 0 && day == 0)
						{
							row.Cells[timeslot].Value = "Sunday";
							timeslot++;
							row.Cells[timeslot].Value = "ASM";
							timeslot++;
						}
						else if (timeslot == 0 && day == 1)
						{
							row.Cells[timeslot].Value = "Monday";
							timeslot++;
						}
						else if (timeslot == 0 && day == 2)
						{
							row.Cells[timeslot].Value = "Tuesday";
							timeslot++;
						}
						else if (timeslot == 0 && day == 3)
						{
							row.Cells[timeslot].Value = "Wednesday";
							timeslot++;
						}
						else if (timeslot == 0 && day == 4)
						{
							row.Cells[timeslot].Value = "Thursday";
							timeslot++;
						}

						//This subject has already run out of credit, skip to next
						if (teacher.WeeklyUsedCredit >= teacher.WeeklyCredit)
							continue;

						if (timeslot == Break.BreakTimeSlot)
						{
							switch (day)
							{
								case 0:
									row.Cells[timeslot].Value = Break.Sunday;
									break;
								case 1:
									row.Cells[timeslot].Value = Break.Monday;
									break;
								case 2:
									row.Cells[timeslot].Value = Break.Tuesday;
									break;
								case 3:
									row.Cells[timeslot].Value = Break.Wednesday;
									break;
								case 4:
									row.Cells[timeslot].Value = Break.Thursday;
									break;
								default:
									break;
							}

							timeslot++;
						}

						if (timeslot == 5 && teacher.OddEven == Timeslot.Even)
						{
							//We have checked all teachers for available credit, proceed to after break
							teacherCount--;

							if (teacherCount == 0)
							{
								//reset teacher's count 
								teacherCount = result.Count;
								timeslot = 7;
							}

							continue;
						}
						else if (timeslot == 5 && teacher.OddEven == Timeslot.Odd)
						{
							//row.Cells[timeslot].Value = teacher.Subject;
							//teacher.WeeklyUsedCredit += 1;
							timeslot++;

							continue;
						}

						//Check if this teacher has odd or even slot
						//if even, skip to next teacher
						if (timeslot == 11 && teacher.OddEven == Timeslot.Even)
						{
							//We have checked all teachers for available credit, proceed to next day
							teacherCount--;

							if (teacherCount == 0)
							{
								//reset teacher's count 
								teacherCount = result.Count;
								day++;
								timeslot = 0;
							}

							continue;
						}
						//if odd, add into the table
						else if (timeslot == 11 && teacher.OddEven == Timeslot.Odd)
						{
							//row.Cells[timeslot].Value = teacher.Subject;
							//teacher.WeeklyUsedCredit += 1;
							timeslot++;
						}
						//else, add as normal
						else if (teacher.OddEven == Timeslot.Even)
						{
							if(!ThisSubjectExistsOnSameDay((DataGridViewRow)dgvViewOutput.Rows[day], teacher.Subject) && !IsSameTeacherAlreadyTeachingThisTimeslot(teacher.TeacherName, timeslot, day, teacher.Subject))
							{								
								//Handle special case of credit more than 10
								if (teacher.WeeklyCredit > 10 && teacher.WeeklyUsedCredit < 6)
								{
                                    //row.Cells[timeslot].Value = teacher.Subject;
                                    row.Cells[timeslot].Value = $"{teacher.Subject}|{teacher.TeacherName}";
                                    teacher.ListOfTimeslot.Add(new TimeslotDetails()
									{
										TimeslotDay = timeslot,
										Day = day
									});
									timeslot++;

									row.Cells[timeslot].Value = $"{teacher.Subject}|{teacher.TeacherName}";
                                    teacher.ListOfTimeslot.Add(new TimeslotDetails()
									{
										TimeslotDay = timeslot,
										Day = day
									});
									timeslot++;

									row.Cells[timeslot].Value = $"{teacher.Subject}|{teacher.TeacherName}";
                                    teacher.ListOfTimeslot.Add(new TimeslotDetails()
									{
										TimeslotDay = timeslot,
										Day = day
									});
									timeslot++;

									teacher.WeeklyUsedCredit += 3;									
								}								
								else
								{
									row.Cells[timeslot].Value = $"{teacher.Subject}|{teacher.TeacherName}";
                                    teacher.ListOfTimeslot.Add(new TimeslotDetails()
									{
										TimeslotDay = timeslot,
										Day = day
									});
									timeslot++;

									row.Cells[timeslot].Value = $"{teacher.Subject}|{teacher.TeacherName}";
                                    teacher.ListOfTimeslot.Add(new TimeslotDetails()
									{
										TimeslotDay = timeslot,
										Day = day
									});
									timeslot++;

									teacher.WeeklyUsedCredit += 2;
								}								
							}
							else
							{
								continue;
							}
						}
						else
						{
							//We fill up even subjects first
							if(IsSuitForEvenSubject(timeslot, day))
							{
								continue;
							}

							if (!ThisSubjectExistsOnSameDay((DataGridViewRow)dgvViewOutput.Rows[day], teacher.Subject) && !IsSameTeacherAlreadyTeachingThisTimeslot(teacher.TeacherName, timeslot, day, teacher.Subject))
							{
								row.Cells[timeslot].Value = $"{teacher.Subject}|{teacher.TeacherName}";
                                teacher.ListOfTimeslot.Add(new TimeslotDetails()
								{
									TimeslotDay = timeslot,
									Day = day
								});

								teacher.WeeklyUsedCredit += 1;
								timeslot++;
							}
							else
							{
								continue;
							}
						}

						//Go to next day
						if (timeslot == 12)
						{
							//reset teacher's count 
							teacherCount = result.Count;
							day++;
							timeslot = 0;
						}

						continue;
					}
				}

				//return;
				//Fulfill other empty timetable, if there's any:
				foreach(DataGridViewRow row in dgvViewOutput.Rows)
				{
					//Only fulfill for empty rows Sunday - Thursday
					if (row.Index > 4)
						break;

					foreach(DataGridViewCell cell in row.Cells)
					{
						if(cell.Value == null || String.IsNullOrEmpty(cell.Value.ToString()))
						{
							string valueToReplace = "";

							foreach (Teacher teacher in result)
							{
								if(teacher.WeeklyUsedCredit >= teacher.WeeklyCredit)
								{
									continue;
								}
								else if(teacher.OddEven == Timeslot.Odd && !ThisSubjectExistsOnSameDay(row, teacher.Subject) && !IsSameTeacherAlreadyTeachingThisTimeslot(teacher.TeacherName, timeslot, day, teacher.Subject))
								{
									teacher.ListOfTimeslot.Add(new TimeslotDetails()
									{
										TimeslotDay = cell.ColumnIndex,
										Day = cell.RowIndex
									});

                                    //valueToReplace = teacher.Subject;
                                    valueToReplace = $"{teacher.Subject}|{teacher.TeacherName}";
                                    teacher.WeeklyUsedCredit++;
                                    cell.Value = valueToReplace;
                                    break;
								}
							}	
						}
					}
				}

				string subjectFromOtherDay = string.Empty;
                string teacherFromOtherDay = string.Empty;
				string subjectForThisDay = string.Empty;

				//return;
				//Check if still empty #2
				foreach (DataGridViewRow row in dgvViewOutput.Rows)
				{
					//Only fulfill for empty rows Sunday - Thursday
					if (row.Index > 4)
						break;

					foreach (DataGridViewCell cell in row.Cells)
					{
						if((cell.Value == null || String.IsNullOrEmpty(cell.Value.ToString())) && (cell.ColumnIndex == 8 || cell.ColumnIndex == 10))
						{
							string teacherToSwap = "";
							string standardToSwap = "";
							foreach (Teacher teacher in result)
							{
								if (teacher.WeeklyUsedCredit < teacher.WeeklyCredit && teacher.OddEven == Timeslot.Even && ThisSubjectExistsOnSameDay(row, teacher.Subject) && !IsSameTeacherAlreadyTeachingThisTimeslot(teacher.TeacherName, timeslot, day, teacher.Subject))
								{
									subjectForThisDay = teacher.Subject;
									teacherToSwap = teacher.TeacherName;
									standardToSwap = teacher.Standard;
									break;
								}
							}
							
							//Fit for even subject
							//check for even subject that does not exist yet, to switch with other day
							foreach (Teacher teacher in result)
							{
								if (teacher.WeeklyUsedCredit == teacher.WeeklyCredit && teacher.OddEven == Timeslot.Even && !ThisSubjectExistsOnSameDay(row, teacher.Subject) && SubjectsDontCoExist(teacher.Subject, subjectForThisDay) && !IsSameTeacherAlreadyTeachingThisTimeslot(teacher.TeacherName, timeslot, day, teacher.Subject))
								{
									subjectFromOtherDay = teacher.Subject;
                                    teacherFromOtherDay = teacher.TeacherName;

                                    cell.Value = $"{subjectFromOtherDay}|{teacherFromOtherDay}";
                                    row.Cells[cell.ColumnIndex + 1].Value = $"{subjectFromOtherDay}|{teacherFromOtherDay}";
                                    break;
								}
							}

                            //cell.Value = subjectFromOtherDay;
                            //row.Cells[cell.ColumnIndex + 1].Value = subjectFromOtherDay;                           

                            //Swap the subjects
                            foreach (DataGridViewCell cell2 in dgvViewOutput.Rows[RowToSwitch].Cells)
							{
                                //if (cell2.Value.ToString() == subjectFromOtherDay)
                                if (!string.IsNullOrEmpty(subjectFromOtherDay) && cell2.Value.ToString().Contains(subjectFromOtherDay))
                                {
                                    //cell2.Value = subjectForThisDay;
                                    cell2.Value = $"{subjectForThisDay}|{teacherToSwap}";

                                    result.Where(a => a.TeacherName == teacherToSwap && a.Standard == standardToSwap && a.Subject == subjectForThisDay)
										.SingleOrDefault().ListOfTimeslot.Add(new TimeslotDetails()
									{
										TimeslotDay = cell2.ColumnIndex,
										Day = RowToSwitch
									});
								}																
							}
						}						
					}					
				}

				//Copy to the public property
				publicResult = result;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				throw;
			}
		}

        private bool IsSameTeacherAlreadyTeachingThisTimeslot(string teacherName, int timeslot, int day, string subject)
        {
            bool isSameTeacherAlreadyTeachingThisTimeslot = false;

            try
            {
                foreach (var dataGrid in AllYearsDataGrids)
                {
                    var subjectTaughtInSameTimeslot = dataGrid.Rows[day].Cells[timeslot].Value;
                    if(subjectTaughtInSameTimeslot == null)
                    {
                        continue;
                    }

                    var splitString = subjectTaughtInSameTimeslot.ToString().Split('|');
                    if (subjectTaughtInSameTimeslot != null && subjectTaughtInSameTimeslot.ToString().Contains(subject) && splitString.Count() > 1 && splitString[1] == teacherName)
                    {
                        isSameTeacherAlreadyTeachingThisTimeslot = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                isSameTeacherAlreadyTeachingThisTimeslot = false;
            }

            return isSameTeacherAlreadyTeachingThisTimeslot;
        }

        private DataGridView FillUpGroupAndAllocation(string selectedClass)
		{
			DataGridView dgv = dgvViewOutput;

            foreach (var groups in listGroupAndAllocation)
			{
				foreach(var group in groups)
				{
					if(group.Standard == selectedClass)
					{
						dgvViewOutput.Rows[group.Day].Cells[group.Timeslot].Value = $"{group.Subject}|{group.TeacherName}";
					}
				}
			}

            return dgvViewOutput;
        }

		private bool SubjectsDontCoExist(string subject, string subjectForThisDay)
		{
			bool subjectExists = false;
			bool subjectForThisDayExists = false;

			foreach (DataGridViewRow row in dgvViewOutput.Rows)
			{
				subjectExists = false;
				subjectForThisDayExists = false;

				foreach (DataGridViewCell cell in row.Cells)
				{
					if (cell.Value == null)
						continue;

					if (cell.Value.ToString() == subject)
					{
						subjectExists = true;
						break;
					}
				}

				foreach (DataGridViewCell cell in row.Cells)
				{
					if (cell.Value == null)
						continue;

					if (cell.Value.ToString() == subjectForThisDay)
					{
						subjectForThisDayExists = true;
						break;
					}
				}

				if (subjectExists && !subjectForThisDayExists)
				{
					RowToSwitch = row.Index;
					break;
				}								
			}			

			return subjectExists && !subjectForThisDayExists;
		}

		private bool IsSuitForEvenSubject(int timeslot, int day)
		{
			return ((timeslot == 2 && day == 0) || (timeslot != 5) || (timeslot != 11));
		}

		private bool ThisSubjectExistsOnSameDay(DataGridViewRow row, string subject)
		{
			bool subjectExistsOnSameDay = false;
			foreach(DataGridViewCell cell in row.Cells)
			{
				if (cell.Value == null)
					continue;

                //if (cell.Value.ToString() == subject)
                if (cell.Value.ToString().Contains(subject))
                {
					subjectExistsOnSameDay = true;
					break;
				}					
			}

			return subjectExistsOnSameDay;
		}

		private void btnFurtherView_Click(object sender, EventArgs e)
		{
			try
			{
				PerformFurtherView();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void PerformFurtherView()
		{
			AnalysisOutput analysis = new AnalysisOutput();

			//reset grid
			//analysis.dgvViewOutputAnalysis.Rows.Clear();
			//analysis.dgvViewOutputAnalysis.Refresh();
			DataGridViewRow row = (DataGridViewRow)analysis.dgvViewOutputAnalysis.Rows[0].Clone();
			row.Cells[0].Value = "Sunday";
			row.Cells[1].Value = "ASM";
			row.Cells[6].Value = "B";
			analysis.dgvViewOutputAnalysis.Rows.Add(row);

			row = (DataGridViewRow)analysis.dgvViewOutputAnalysis.Rows[0].Clone();
			row.Cells[0].Value = "Monday";
			row.Cells[6].Value = "R";
			analysis.dgvViewOutputAnalysis.Rows.Add(row);

			row = (DataGridViewRow)analysis.dgvViewOutputAnalysis.Rows[0].Clone();
			row.Cells[0].Value = "Tuesday";
			row.Cells[6].Value = "E";
			analysis.dgvViewOutputAnalysis.Rows.Add(row);

			row = (DataGridViewRow)analysis.dgvViewOutputAnalysis.Rows[0].Clone();
			row.Cells[0].Value = "Wednesday";
			row.Cells[6].Value = "A";
			analysis.dgvViewOutputAnalysis.Rows.Add(row);

			row = (DataGridViewRow)analysis.dgvViewOutputAnalysis.Rows[0].Clone();
			row.Cells[0].Value = "Thursday";
			row.Cells[6].Value = "K";
			analysis.dgvViewOutputAnalysis.Rows.Add(row);

			//default selection
			if (String.IsNullOrEmpty(selectedCBAnalysis) || selectedCBAnalysis.Contains("teacher"))
			{
				//Repeat for all years
				for (int i = 0; i < 6; i++)
				{
					cbClassViewOutput.SelectedIndex = i;
					btnShowTimetableViewOutput_Click(null, null);
					PerformChildFurtherView(analysis);
				}
			}
			else if(selectedCBAnalysis.Contains("class"))
			{
				cbClassViewOutput.SelectedIndex = Convert.ToInt32(cbChooseClassAnalysis.Text.Substring(1, 1)) - 1;
				btnShowTimetableViewOutput_Click(null, null);
				PerformChildFurtherView(analysis);
			}
			else if (selectedCBAnalysis.Contains("subject"))
			{
				cbClassViewOutput.SelectedIndex = Convert.ToInt32(cbChooseYear.Text.Substring(1, 1)) - 1;
				btnShowTimetableViewOutput_Click(null, null);
				PerformChildFurtherView(analysis);
			}
			else if (selectedCBAnalysis.Contains("sitin"))
			{
				cbClassViewOutput.SelectedIndex = Convert.ToInt32(cbChooseClassSitIn.Text.Substring(1, 1)) - 1;
				btnShowTimetableViewOutput_Click(null, null);
				PerformChildFurtherView(analysis);
			}
			else
			{
				return;
			}

			analysis.ShowDialog();
		}

		private void PerformChildFurtherView(AnalysisOutput analysis)
		{
			var selectedTeacher = cbTeacherAnalyzeOutputs.Text;
			var selectedYear = cbChooseClassAnalysis.Text;

			ConnectToDB();

			SQLiteDataAdapter ad;
			DataTable dt = new DataTable();
			if (String.IsNullOrEmpty(TimetableTeachersName))
			{
				TimetableTeachersName = "teachers";
			}

			//Fill teacher combobox in Constraints tab
			SQLiteCommand cmd;
			//m_dbConnection.Open();  //Initiate connection to the db
			cmd = m_dbConnection.CreateCommand();
			//default selection
			if (String.IsNullOrEmpty(selectedCBAnalysis) || selectedCBAnalysis.Contains("teacher"))
			{
				cmd.CommandText = $"select * from {TimetableTeachersName} where teachername = '{selectedTeacher}' order by standard";
				publicResult = publicResult.Where(a => a.TeacherName == selectedTeacher).OrderByDescending(a => a.WeeklyCredit).ToList();

				foreach (var teacher in publicResult)
				{
					foreach (var timeslot in teacher.ListOfTimeslot)
					{
						var rowToUpdate = (DataGridViewRow)analysis.dgvViewOutputAnalysis.Rows[timeslot.Day];
						rowToUpdate.Cells[timeslot.TimeslotDay].Value = teacher.Subject;
					}
				}

				analysis.lblAnalysisTitle.Text = $"Analysis Teacher Timetable: {selectedTeacher}";
			}
			else if(selectedCBAnalysis.Contains("class"))
			{
				var selectedClass = cbChooseClassAnalysis.Text;
				cmd.CommandText = $"select * from {TimetableTeachersName} where standard = '{selectedClass}' order by standard";
				publicResult = publicResult.Where(a => a.Standard == selectedClass).OrderByDescending(a => a.WeeklyCredit).ToList();

				//reset grid
				analysis.dgvViewOutputAnalysis.Rows.Clear();
				analysis.dgvViewOutputAnalysis.Refresh();

				foreach (DataGridViewRow row1 in dgvViewOutput.Rows)
				{
					analysis.dgvViewOutputAnalysis.Rows.Add(CloneWithValues(row1));
				}

				analysis.lblAnalysisTitle.Text = $"Analysis Class Timetable: {selectedClass}";
			}
			else if (selectedCBAnalysis.Contains("day"))
			{
				var selectedDay = cbChooseDayByDay.Text;
				cmd.CommandText = $"select * from {TimetableTeachersName} where standard = '{selectedYear}' order by standard";
				publicResult = publicResult.Where(a => a.Standard == selectedYear).OrderByDescending(a => a.WeeklyCredit).ToList();

				//reset grid
				analysis.dgvViewOutputAnalysis.Rows.Clear();
				analysis.dgvViewOutputAnalysis.Refresh();

				foreach (DataGridViewRow row1 in dgvViewOutput.Rows)
				{
					analysis.dgvViewOutputAnalysis.Rows.Add(CloneWithValues(row1));
				}

				analysis.lblAnalysisTitle.Text = $"Analysis School Timetable by day: {selectedDay}";
			}
			else if (selectedCBAnalysis.Contains("subject"))
			{
				selectedYear = cbChooseYear.Text;
				string selectedSubject = cbChooseSubject.Text;
				cmd.CommandText = $"select * from {TimetableTeachersName} where standard = '{selectedYear}' and subject = '{selectedSubject}' order by standard";
				publicResult = publicResult.Where(a => a.Standard == selectedYear).OrderByDescending(a => a.WeeklyCredit).ToList();

				analysis.lblAnalysisTitle.Text = $"Analysis Subject: {selectedSubject} for Year: {selectedYear}";

				//reset grid
				analysis.dgvViewOutputAnalysis.Rows.Clear();
				analysis.dgvViewOutputAnalysis.Refresh();

				foreach (DataGridViewRow row1 in dgvViewOutput.Rows)
				{
					analysis.dgvViewOutputAnalysis.Rows.Add(CloneWithValues(row1));
				}

				try
				{
					//Clear the other fields
					foreach (DataGridViewRow row2 in analysis.dgvViewOutputAnalysis.Rows)
					{
						foreach (DataGridViewCell cell in row2.Cells)
						{
							if (cell.Value == null)
								continue;

							if (cell.Value.ToString() != selectedSubject && cell.Value.ToString() != "Sunday" && cell.Value.ToString() != "Monday"
								&& cell.Value.ToString() != "Tuesday" && cell.Value.ToString() != "Wednesday" && cell.Value.ToString() != "Thursday"
								&& cell.Value.ToString() != "ASM" && cell.Value.ToString() != "B" && cell.Value.ToString() != "R"
								&& cell.Value.ToString() != "E" && cell.Value.ToString() != "A" && cell.Value.ToString() != "K")
							{
								cell.Value = "";
							}
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);					
				}
			}
			else if (selectedCBAnalysis.Contains("sitin"))
			{
				analysis.dgvViewOutputAnalysis.Visible = false;

				string selectedClass = cbChooseClassSitIn.Text;
				string selectedDay = cbChooseDaySitIn.Text;
				string selectedTimeslot = cbChooseTimeslotIn.Text;

				analysis.lblAnalysisTitle.Text = $"Analysis Sit In for Class: {selectedClass}, Day: {selectedDay}, Timeslot: {selectedTimeslot}";

				analysis.lblDetailsSitIn.Text = $"Class {selectedClass}, Day {selectedDay}, Timeslot {selectedTimeslot}, Available teachers:";

				int intSelectedDay = 0;
				int intSelectedTimeslot = 0;

				switch (selectedDay)
				{
					case "Sunday":
						intSelectedDay = 0;
						break;
					case "Monday":
						intSelectedDay = 1;
						break;
					case "Tuesday":
						intSelectedDay = 2;
						break;
					case "Wednesday":
						intSelectedDay = 3;
						break;
					case "Thursday":
						intSelectedDay = 4;
						break;
					default:
						break;
				}

				switch (selectedTimeslot)
				{
					case "7:30-8:00":
						intSelectedTimeslot = 1;
						break;
					case "8:00-8:30":
						intSelectedTimeslot = 2;
						break;
					case "8:30-9:00":
						intSelectedTimeslot = 3;
						break;
					case "9:00-9:30":
						intSelectedTimeslot = 4;
						break;
					case "9:30-10:00":
						intSelectedTimeslot = 5;
						break;
					case "10:20-10:50":
						intSelectedTimeslot = 6;
						break;
					case "10:50-11:20":
						intSelectedTimeslot = 7;
						break;
					case "11:30-11:50":
						intSelectedTimeslot = 8;
						break;
					case "11:50-12:20":
						intSelectedTimeslot = 9;
						break;
					case "12:20-12:50":
						intSelectedTimeslot = 10;
						break;
					default:
						break;
				}

				
				publicResult = publicResult.Where(a => a.TeacherName == selectedTeacher).OrderByDescending(a => a.WeeklyCredit).ToList();
				var occupiedTeacher = "";

				foreach (var teacher in publicResult)
				{
					foreach (var timeslot in teacher.ListOfTimeslot)
					{
						var rowToUpdate = (DataGridViewRow)analysis.dgvViewOutputAnalysis.Rows[timeslot.Day];
						rowToUpdate.Cells[timeslot.TimeslotDay].Value = teacher.Subject;
						//get teacher's name from the selected timeslot
						if(timeslot.Day == intSelectedDay && timeslot.TimeslotDay == intSelectedTimeslot && teacher.Standard == selectedClass)
						{
							occupiedTeacher = teacher.TeacherName;
						}
					}
				}

				Random rnd = new Random();
				int randomNumber = rnd.Next(1, 13);

				//Random number
				if (randomNumber < 6)
				{
					cmd.CommandText = $"select teachername from {TimetableTeachersName} where teachername != '{occupiedTeacher}' group by teachername order by standard DESC LIMIT 3";
				}
				else
				{
					cmd.CommandText = $"select teachername from {TimetableTeachersName} where teachername != '{occupiedTeacher}' group by teachername order by standard LIMIT 3";
				}
			}
			else
			{
				return;
			}

			ad = new SQLiteDataAdapter(cmd);
			ad.Fill(dt); //fill the datasource
			
			analysis.dgvDetailsOutput.DataSource = dt;			
		}

		public DataGridViewRow CloneWithValues(DataGridViewRow row)
		{
			DataGridViewRow clonedRow = (DataGridViewRow)row.Clone();
			for (Int32 index = 0; index < row.Cells.Count; index++)
			{
				clonedRow.Cells[index].Value = row.Cells[index].Value;
			}
			return clonedRow;
		}

		private DataGridView CopyDataGridView(DataGridView dgv_org)
		{
			DataGridView dgv_copy = new DataGridView();
			try
			{
				if (dgv_copy.Columns.Count == 0)
				{
					foreach (DataGridViewColumn dgvc in dgv_org.Columns)
					{
						dgv_copy.Columns.Add(dgvc.Clone() as DataGridViewColumn);
					}
				}

				DataGridViewRow row = new DataGridViewRow();

				for (int i = 0; i < dgv_org.Rows.Count; i++)
				{
					row = (DataGridViewRow)dgv_org.Rows[i].Clone();
					int intColIndex = 0;
					foreach (DataGridViewCell cell in dgv_org.Rows[i].Cells)
					{
						row.Cells[intColIndex].Value = cell.Value;
						intColIndex++;
					}
					dgv_copy.Rows.Add(row);
				}
				dgv_copy.AllowUserToAddRows = false;
				dgv_copy.Refresh();

			}
			catch (Exception ex)
			{
				//cf.ShowExceptionErrorMsg("Copy DataGridViw", ex);
			}
			return dgv_copy;
		}

		private void SetSelectedComboBoxForAnalysis(object sender, string fromCB)
		{
			ComboBox cb = (ComboBox)sender;
			selectedCBAnalysis = fromCB;
			selectedCBAnalysisValue1 = cb.Text;
		}
		private void cbChooseDayByDay_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetSelectedComboBoxForAnalysis(sender, "day");
		}

		private void cbTeacherAnalyzeOutputs_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetSelectedComboBoxForAnalysis(sender, "teacher");
		}

		private void cbChooseClassAnalysis_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetSelectedComboBoxForAnalysis(sender, "class");
		}

		private void cbChooseYear_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetSelectedComboBoxForAnalysis(sender, "subject");
		}

		private void cbChooseClassSitIn_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetSelectedComboBoxForAnalysis(sender, "sitin");
		}

		private void btnPrintViewOutput_Click(object sender, EventArgs e)
		{
			//Print the result
		}

		private void btnRegisterTimetable_Click(object sender, EventArgs e)
		{
			TimetableTeachersName = txtTimetableName.Text;
			MessageBox.Show($"Successfully register timetable's name as {TimetableTeachersName}");
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			txtTimetableName.Text = "";
			txtTeacherList.Text = "";
			txtTimeslotList.Text = "";
			txtImportStatus.Text = "";
		}

		private void cbChooseTimeslotIn_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

        private void cbClassViewOutput_SelectedIndexChanged(object sender, EventArgs e)
        {
                      
        }



        private void ShowGridAccordingToClasses(string text)
        {
            var year = Convert.ToInt32(text.Substring(1, 1)) - 1;
            //throw new NotImplementedException();
            dgvViewOutput.Rows.Clear();
            dgvViewOutput.Refresh();
            DataGridViewRow row = (DataGridViewRow)dgvViewOutput.Rows[0].Clone();
            dgvViewOutput.Rows.Add(row);
            row = (DataGridViewRow)dgvViewOutput.Rows[0].Clone();
            dgvViewOutput.Rows.Add(row);
            row = (DataGridViewRow)dgvViewOutput.Rows[0].Clone();
            dgvViewOutput.Rows.Add(row);
            row = (DataGridViewRow)dgvViewOutput.Rows[0].Clone();
            dgvViewOutput.Rows.Add(row);
            row = (DataGridViewRow)dgvViewOutput.Rows[0].Clone();
            dgvViewOutput.Rows.Add(row);

            //Copy all the values
            for(int i = 0; i < AllYearsDataGrids[year].Rows.Count - 1; i++)
            {
                for(int j = 0; j < AllYearsDataGrids[year].Columns.Count - 1; j++)
                {
                    dgvViewOutput.Rows[i].Cells[j].Value = AllYearsDataGrids[year].Rows[i].Cells[j].Value;
                }                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbClassViewOutput.Text))
            {
                ShowGridAccordingToClasses(cbClassViewOutput.Text);
            }
        }
    }
}

