using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab7
{
    public partial class Form2 : Form
    {
        static string connectionString = @"Data Source=(local);Initial Catalog=students;Integrated Security=True";
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        DataGridView dgv1;
        DataGridView dgv2;
        DataGridView dgv3;
        Form1 forma;
        AdoNetExecutor executor;
        int Item;
        int Mode;
        public Form2(int item, int mode, DataGridView dgv1, DataGridView dgv2, DataGridView dgv3, AdoNetExecutor DataBase, Form1 forma)
        {
            InitializeComponent();
            this.forma = forma;
            this.dgv1 = dgv1;
            this.dgv2 = dgv2;
            this.dgv3 = dgv3;
            executor = DataBase;
            Item = item;
            Mode = mode;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label5.Visible = false;
            textBox4.Visible = false;
            if (Mode == 0)
            {
                button1.Text = "Додати";
                if (Item == 0) // People
                {
                    textBox3.Visible = false;
                    label3.Visible = false;
                    label1.Text = "ПІБ";
                    label2.Text = "Група";
                }
                if (Item == 1) //Subject
                {
                    textBox3.Visible = false;
                    label3.Visible = false;
                    label2.Visible = false;
                    label1.Text = "Назва";
                    textBox2.Visible = false;
                }
                if (Item == 2)
                {
                    label1.Text = "ID студента";
                    label2.Text = "ID предмета";
                    label3.Text = "Оцінка";
                }
            }
            if (Mode == 1)
            {

                button1.Text = "Видалили";
                if (Item == 0) // People
                {
                    textBox3.Visible = false;
                    label3.Visible = false;
                    label2.Visible = false;
                    label1.Text = "Id";
                    textBox2.Visible = false;
                }
                if (Item == 1) //Subject
                {
                    textBox3.Visible = false;
                    label3.Visible = false;
                    label2.Visible = false;
                    label1.Text = "Id";
                    textBox2.Visible = false;
                }
                if (Item == 2)
                {
                    textBox3.Visible = false;
                    label3.Visible = false;
                    label2.Visible = false;
                    label1.Text = "Id";
                    textBox2.Visible = false;
                }
            }
            if (Mode == 2)
            {

                button1.Text = "Оновити";
                if (Item == 0) // People
                {
                    label3.Text = "Група";
                    label2.Text = "ПІБ";
                    label1.Text = "Id(Ключ)";
                }
                if (Item == 1) //Subject
                {
                    textBox3.Visible = false;
                    label3.Visible = false;
                    label1.Text = "ID(Ключ)";
                    label2.Text = "Нова назва";
                }
                if (Item == 2)
                {
                    label1.Text = "ID(Ключ)";
                    label2.Text = "ID Студента";
                    label3.Text = "ID предмета";
                    label5.Text = "Оцінка";
                    label5.Visible = true;
                    textBox4.Visible = true;
                }

            }
        }
        static void RefreshGrids(DataGridView dgv1, DataGridView dgv2, DataGridView dgv3)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM People", sqlConnection);
                DataTable dataTable = new DataTable();
                sqlDa.Fill(dataTable);
                dgv1.DataSource = dataTable;
                SqlDataAdapter sqlDat = new SqlDataAdapter("SELECT * FROM Subjects", sqlConnection);
                DataTable dataTable1 = new DataTable();
                sqlDat.Fill(dataTable1);
                dgv2.DataSource = dataTable1;
                SqlDataAdapter sqlData = new SqlDataAdapter("SELECT * FROM Marks", sqlConnection);
                DataTable dataTable2 = new DataTable();
                sqlData.Fill(dataTable2);
                dgv3.DataSource = dataTable2;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Mode == 0)
            {
                if (Item == 0) // People
                {
                    executor.InsertStudentToDataBase(sqlConnection, connectionString, textBox1.Text, textBox2.Text);
                }
                if (Item == 1) //Subject
                {
                    executor.InsertSubjectToDataBase(sqlConnection, connectionString, textBox1.Text);
                }
                if (Item == 2)
                {

                    executor.InsertMarkToDataBase(sqlConnection, connectionString, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text));
                }
            }
            if (Mode == 1)
            {
                if (Item == 0) // People
                {
                    executor.DeletePeopleFromDataBase(sqlConnection, connectionString, Convert.ToInt32(textBox1.Text));
                }
                if (Item == 1) //Subject
                {
                    executor.DeleteSubjectFromDataBase(sqlConnection, connectionString, Convert.ToInt32(textBox1.Text));
                }
                if (Item == 2)
                {

                    executor.DeleteMarkFromDataBase(sqlConnection, connectionString, Convert.ToInt32(textBox1.Text));
                }
            }
            if (Mode == 2)
            {
                if (Item == 0) // People
                {
                    executor.UpdatePersonFromDataBase(sqlConnection, connectionString, Convert.ToInt32(textBox1.Text), textBox2.Text, textBox3.Text);
                }
                if (Item == 1) //Subject
                {
                    executor.UpdateSubjectFromDataBase(sqlConnection, connectionString, Convert.ToInt32(textBox1.Text), textBox2.Text);
                }
                if (Item == 2)
                {

                    executor.UpdateMarkFromDataBase(sqlConnection, connectionString, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text), Convert.ToInt32(textBox4.Text));
                }
                
            }
            RefreshGrids(dgv1, dgv2, dgv3);
            forma.Show();
            Close();
        }
    }
}
