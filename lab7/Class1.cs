using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace lab7
{
    public class AdoNetExecutor
    {
        static string connectionString = @"Data Source=(local);Initial Catalog=students;Integrated Security=True";
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        public AdoNetExecutor()
        {

        }
        public bool InsertStudentToDataBase(SqlConnection sqlConnection, string connectionString, string FullName, string Group)
        {
            string sqlExpression = String.Format("SELECT * FROM People Where FullName='{0}' and Grupa='{1}'", FullName, Group);
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    return false;
                }
            }
            sqlExpression = String.Format("INSERT INTO People (FullName, Grupa) VALUES ('{0}', '{1}')", FullName, Group);
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
                int number = command.ExecuteNonQuery();
            }
            return true;
        }
        public bool InsertSubjectToDataBase(SqlConnection sqlConnection, string connectionString, string Name)
        {
            string sqlExpression = String.Format("SELECT * FROM Subjects Where Name='{0}'", Name);
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    return false;
                }
            }
            sqlExpression = String.Format("INSERT INTO Subjects (Name) VALUES ('{0}')", Name);
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
                int number = command.ExecuteNonQuery();
                Console.WriteLine("Добавлено объектов: {0}", number);
            }
            return true;
        }
        public bool InsertMarkToDataBase(SqlConnection sqlConnection, string connectionString, int idStud, int idSub, int mark)
        {
            string sqlExpression = String.Format("SELECT * FROM People Where id='{0}'", idStud);
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    reader.Close();
                    return false;
                }
            }
            sqlExpression = String.Format("SELECT * FROM Subjects Where id='{0}'", idSub);
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    reader.Close();
                    return false;
                }
            }

            sqlExpression = String.Format("SELECT COUNT(*) FROM Marks Where studentId='{0}'", idStud);
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
                string res = $"{command.ExecuteScalar()}";
                if (Convert.ToInt32(res) == 35)
                {
                    return false;
                }
            }

            sqlExpression = String.Format("INSERT INTO Marks (studentId, sujectId, mark) VALUES ('{0}', '{1}', '{2}')", idStud, idSub, mark);
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
                int number = command.ExecuteNonQuery();
                Console.WriteLine("Добавлено объектов: {0}", number);
            }
            return true;
        }
        public void ReadStudentsFromDataBase(SqlConnection sqlConnection, string connectionString, List<People> Students)
        {
            string sqlExpression = "SELECT distinct * FROM People";
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        People student = new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                        Students.Add(student);
                    }
                }

                reader.Close();
            }
        }
        public void ReadSubjectsFromDataBase(SqlConnection sqlConnection, string connectionString, List<Subject> Subjects)
        {
            string sqlExpression = "SELECT * FROM Subjects";
            using (sqlConnection = new SqlConnection(connectionString))
            {
                Subjects.Clear();
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Subject sub = new Subject(reader.GetInt32(0),reader.GetString(1));
                        Subjects.Add(sub);
                    }
                }

                reader.Close();
            }
        }
        public void ReadMarksFromDataBase(SqlConnection sqlConnection, string connectionString, List<marks> Marks)

        {
            Marks.Clear();
            string sqlExpression = "SELECT * FROM Marks";
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        marks mark = new(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3));
                        Marks.Add(mark);
                    }
                }

                reader.Close();
            }
        }
        public bool DeleteMarkFromDataBase(SqlConnection sqlConnection, string connectionString, int id)
        {
            string sqlExpression = String.Format("DELETE FROM Marks WHERE idrec='{0}' ", id);
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
                int number = command.ExecuteNonQuery();
            }
            return true;
        }
        public bool DeletePeopleFromDataBase(SqlConnection sqlConnection, string connectionString, int id)
        {
            string sqlExpression = String.Format("DELETE FROM People WHERE id='{0}' ", id);
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
                int number = command.ExecuteNonQuery();
            }
            return true;
        }
        public bool DeleteSubjectFromDataBase(SqlConnection sqlConnection, string connectionString, int id)
        {
            string sqlExpression = String.Format("DELETE FROM Subjects WHERE id='{0}' ", id);
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
                int number = command.ExecuteNonQuery();
            }
            return true;
        }
        public bool UpdateSubjectFromDataBase(SqlConnection sqlConnection, string connectionString, int id, string Name)
        {
            string sqlExpression = String.Format("SELECT * FROM Subjects WHERE id='{0}' ", id);
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    reader.Close();
                    return false;
                }
            }
            sqlExpression = String.Format("UPDATE Subjects SET Name='{1}' WHERE id='{0}' ", id, Name);
            using (sqlConnection = new SqlConnection(connectionString))
            { 
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
                int number = command.ExecuteNonQuery();
            }
                return true;
        }
        public bool UpdateMarkFromDataBase(SqlConnection sqlConnection, string connectionString, int id, int studId, int subjId, int mark)
        {
            string sqlExpression = String.Format("SELECT * FROM Marks WHERE id='{0}' ", id);
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    reader.Close();
                    return false;
                }
            }
            sqlExpression = String.Format("SELECT * FROM People Where id='{0}'", studId);
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    reader.Close();
                    return false;
                }
            }
            sqlExpression = String.Format("SELECT * FROM Subjects Where id='{0}'", subjId);
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    reader.Close();
                    return false;
                }
            }

            sqlExpression = String.Format("UPDATE Marks SET studentId='{1}', sujectId='{2}', mark='{3}'  WHERE id='{0}' ", id, studId, subjId, mark);
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
                int number = command.ExecuteNonQuery();
            }
            return true;
        }
        public bool UpdatePersonFromDataBase(SqlConnection sqlConnection, string connectionString, int id, string Name, string group)
        {
            string sqlExpression = String.Format("SELECT * FROM People WHERE id='{0}' ", id);
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    reader.Close();
                    return false;
                }
            }
            sqlExpression = String.Format("UPDATE People SET FullName='{1}', Grupa='{2}' WHERE id='{0}' ", id, Name, group);
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
                int number = command.ExecuteNonQuery();
            }
            return true;
        }
    }
}
