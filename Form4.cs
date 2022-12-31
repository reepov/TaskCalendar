using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nikitina_kursach
{
    public partial class Form4 : Form
    {
        public string sqlQuery;
        public SQLiteDataAdapter adapter;
        public Form4()
        {
            InitializeComponent();
            Fill();
        }
        public void Fill()
        {
            DataTable dTable = new DataTable();
            var connection = new SQLiteConnection("Data Source=usersdata.db");
            connection.Open();
            sqlQuery = "SELECT * FROM Tasks";
            adapter = new SQLiteDataAdapter(sqlQuery, connection);
            adapter.Fill(dTable);
            dataGridView1.Rows.Clear();
            for (int i = 0; i < dTable.Rows.Count; i++) dataGridView1.Rows.Add(dTable.Rows[i].ItemArray);
            sqlQuery = "SELECT * FROM Users";
            adapter = new SQLiteDataAdapter(sqlQuery, connection);
            dTable = new DataTable();
            adapter.Fill(dTable);
            dataGridView2.Rows.Clear();
            for (int i = 0; i < dTable.Rows.Count; i++) dataGridView2.Rows.Add(dTable.Rows[i].ItemArray);
        }
    }
}
