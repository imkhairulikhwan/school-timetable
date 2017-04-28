using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTimetable
{
    public class Teacher
    {
        public string TeacherName { get; set; }
        public string Standard { get; set; }
        public string Subject { get; set; }
        public int WeeklyCredit { get; set; }
        public Timeslot OddEven { get; set; }

        public static Teacher FromCsv(string csvLine)
        {
            try
            {
                string[] values = csvLine.Split(',');
                Teacher teacher = new Teacher();
                teacher.TeacherName = values[0];
                teacher.Standard = values[1];
                teacher.Subject = values[2];
                teacher.WeeklyCredit = Convert.ToInt32(values[3]);
                teacher.OddEven = (Timeslot)Convert.ToInt32(values[4]);

                return teacher;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }    
}
