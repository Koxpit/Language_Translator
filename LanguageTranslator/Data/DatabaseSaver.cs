using LanguageTranslator.Enums;
using LanguageTranslator.Models;
using LanguageTranslator.Services;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageTranslator.Data
{
    public class DatabaseSaver : IDataSaver
    {
        private string Connection { get; } = ConnectionStringUtility.GetConnectionString();

        public async override void SaveTranslate(TranslateWord translate)
        {
            if (HasTranslate(translate))
            {
                Status = AddStatus.HAS_TRANSLATE;
                return;
            }

            Status = AddStatus.HAS_NOT_TRANSLATE;

            using (SqlConnection connection = new SqlConnection(Connection))
            {
                string sqlExpression = "INSERT INTO RU_EN (Word, Translate) VALUES (@Word, @Translate)";
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    await connection.OpenAsync();
                    command.Parameters.AddWithValue("@Word", translate.Word);
                    command.Parameters.AddWithValue("@Translate", translate.Translate);
                    await command.ExecuteNonQueryAsync();
                }

                SortTranslates(connection);

                if (connection.State == ConnectionState.Open)
                {
                    await connection.CloseAsync();
                }
            }
        }

        public override bool HasTranslate(TranslateWord translate)
        {
            // TODO: Реализация проверки в БД
            return false;
        }

        public async override void SortTranslates()
        {
            using (SqlConnection connection = new SqlConnection(Connection))
            {
                string sqlExpression = "SortWords";
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    await connection.OpenAsync();
                    command.CommandType = CommandType.StoredProcedure;
                    await command.ExecuteNonQueryAsync();
                }
                connection.Close();
            }
        }

        public async void SortTranslates(SqlConnection connection)
        {
            string sqlExpression = "SortWords";
            using (SqlCommand command = new SqlCommand(sqlExpression, connection))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    await connection.OpenAsync();
                }
                command.CommandType = CommandType.StoredProcedure;
                await command.ExecuteNonQueryAsync();
            }
            await connection.CloseAsync();
        }
    }
}
