using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nikitina_kursach
{
    public partial class Form5 : Form
    {
        public string[] month = new string[] { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };
        public Form5(string the_year, int userId)
        {
            var Id = userId;
            var year = the_year;
            InitializeComponent();
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisY.Interval = 1;
            using (var connection = new SqliteConnection("Data Source=usersdata.db"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                for (int i = 0; i < 12; i++)
                {
                    if (i > 9) command.CommandText = $"SELECT COUNT(*) FROM Tasks WHERE OwnerId = {Id} AND Date LIKE '%.{i + 1}.{year}'";
                    else command.CommandText = $"SELECT COUNT(*) FROM Tasks WHERE OwnerId = {Id} AND Date LIKE '%.0{i + 1}.{year}'";
                    chart1.Series[0].Points.AddXY(month[i], int.Parse(command.ExecuteScalar().ToString()));
                }   
            }
        }
    }
}
