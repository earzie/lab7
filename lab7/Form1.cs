using System.Data;
using System.Data.SqlClient;

namespace lab7
{
    public partial class Form1 : Form
    {
        static string connectionString = @"Data Source=(local);Initial Catalog=students;Integrated Security=True";
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        static AdoNetExecutor DataBase = new AdoNetExecutor();
        static List<marks> Marks = new List<marks>();
        static List<People> students = new List<People>();
        static List<Subject> subjects = new List<Subject>();
        public Form1()
        {
            InitializeComponent();
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

        private void Form1_Load(object sender, EventArgs e)
        {
            DataBase.ReadMarksFromDataBase(sqlConnection, connectionString, Marks);
            DataBase.ReadSubjectsFromDataBase(sqlConnection, connectionString, subjects);
            DataBase.ReadStudentsFromDataBase(sqlConnection, connectionString, students);
            RefreshGrids(dataGridView1, dataGridView2, dataGridView3);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            Form2 forma = new Form2(0, 0, dataGridView1, dataGridView2, dataGridView3, DataBase, this);
            forma.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            Form2 forma = new Form2(1, 0, dataGridView1, dataGridView2, dataGridView3, DataBase, this);
            forma.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            Form2 forma = new Form2(2, 0, dataGridView1, dataGridView2, dataGridView3, DataBase, this);
            forma.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Hide();
            Form2 forma = new Form2(2, 1, dataGridView1, dataGridView2, dataGridView3, DataBase, this);
            forma.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Hide();
            Form2 forma = new Form2(1, 1, dataGridView1, dataGridView2, dataGridView3, DataBase, this);
            forma.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hide();
            Form2 forma = new Form2(0, 1, dataGridView1, dataGridView2, dataGridView3, DataBase, this);
            forma.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Hide();
            Form2 forma = new Form2(1, 2, dataGridView1, dataGridView2, dataGridView3, DataBase, this);
            forma.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Hide();
            Form2 forma = new Form2(2, 2, dataGridView1, dataGridView2, dataGridView3, DataBase, this);
            forma.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Hide();
            Form2 forma = new Form2(0, 2, dataGridView1, dataGridView2, dataGridView3, DataBase, this);
            forma.Show();
        }
    }
    public class People
    {
        int id;
        string FullName;
        string group;
        public People(int _id, string _FullName, string _group)
        {
            id = _id;
            FullName = _FullName;
            group = _group;
        }
    }
    public class Subject
    {
        int id;
        string Name;
        public Subject(int id, string name)
        {
            this.id = id;
            Name = name;
        }
    }
    public class marks
    {
        int Id;
        int StudId;
        int SubId;
        int Mark;
        public marks(int id, int studId, int subId, int mark)
        {
            Id = id;
            StudId = studId;
            SubId = subId;
            Mark = mark;
        }
    }

}