using System.Data;
using System.Data.SqlClient;

namespace LanguageTranslator.Data
{
    public class DatabaseSaver : IDataSaver
    {
        public void Save()
        {
            using (SqlConnection connection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Languages;Integrated Security=True"))
            {
                string sqlExpression = $"INSERT INTO RU_EN (Id, Word, Translate) VALUES (@Id, @Word, @Translate)";
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    connection.Open();
                    foreach (var translate in Words.translates)
                    {
                        command.Parameters.AddWithValue("@Id", translate.Id);
                        command.Parameters.AddWithValue("@Word", translate.Word);
                        command.Parameters.AddWithValue("@Translate", translate.Translate);
                        command.ExecuteNonQuery();
                    }
                }
                SortTranslates(connection);

                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public static void SortTranslates()
        {
            using (SqlConnection connection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Languages;Integrated Security=True"))
            {
                string sqlExpression = "SortWords";
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    connection.Open();
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public static void SortTranslates(SqlConnection connection)
        {
            string sqlExpression = "SortWords";
            using (SqlCommand command = new SqlCommand(sqlExpression, connection))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                command.CommandType = CommandType.StoredProcedure;
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}
