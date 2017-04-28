using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        public Form1()
        {
            InitializeComponent();

            Helper helper = new Helper();
            helper.CreateNewDatabase();
            helper.ConnectToDatabase();
            helper.CreateTable();
            helper.FillTable();
            helper.PrintHighscores();

            openFileDialog1 = new OpenFileDialog();

        }

        #region Event handlers

        private void Form1_Load(object sender, EventArgs e)
        {
            PopulateOutputGridView();
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
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
            row.Cells[0].Value = "Monday";
            row.Cells[1].Value = "ASM";
            row.Cells[6].Value = "B";
            dgvViewOutput.Rows.Add(row);
            row = (DataGridViewRow)dgvViewOutput.Rows[0].Clone();
            row.Cells[0].Value = "Tuesday";
            row.Cells[6].Value = "R";
            dgvViewOutput.Rows.Add(row);
            row = (DataGridViewRow)dgvViewOutput.Rows[0].Clone();
            row.Cells[0].Value = "Wednesday";
            row.Cells[6].Value = "E";
            dgvViewOutput.Rows.Add(row);
            row = (DataGridViewRow)dgvViewOutput.Rows[0].Clone();
            row.Cells[0].Value = "Thursday";
            row.Cells[6].Value = "A";
            dgvViewOutput.Rows.Add(row);
            row = (DataGridViewRow)dgvViewOutput.Rows[0].Clone();
            row.Cells[0].Value = "Friday";
            row.Cells[6].Value = "K";
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
            ImportTeacherListIntoDB();
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
            string sql = "create table teachers (TeacherName varchar(100), Standard varchar(20), Subject varchar(20), WeeklyCredit int, OddEven bit)";
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

            ConnectToDB();
            CreateTeacherTable();
            InsertRecordsIntoTeacherTable();
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
                    string sql = $"insert into teachers (TeacherName, Standard, Subject, WeeklyCredit, OddEven) values ('{teacher.TeacherName}'," +
                        $"'{teacher.Standard}', '{teacher.Subject}', {teacher.WeeklyCredit}, {Convert.ToInt32(teacher.OddEven)})";
                    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
