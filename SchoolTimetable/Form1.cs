using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolTimetable
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Helper helper = new Helper();
            helper.CreateNewDatabase();
            helper.ConnectToDatabase();
            helper.CreateTable();
            helper.FillTable();
            helper.PrintHighscores();
        }

		private void Form1_Load(object sender, EventArgs e)
		{

		}
	}
}
