using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
		FileDialog openFileDialog1;

		public Form1()
        {
            InitializeComponent();
        }

		#region Event handlers

		private void Form1_Load(object sender, EventArgs e)
		{

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

		private void BrowseFile()
		{
			int size = -1;
			DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
			if (result == DialogResult.OK) // Test result.
			{
				string file = openFileDialog1.FileName;
				try
				{
					string text = File.ReadAllText(file);
					size = text.Length;
				}
				catch (IOException)
				{
				}
			}
		}

		#endregion
	}
}
