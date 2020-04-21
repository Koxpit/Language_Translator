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
        protected string Connection { get; } = ConnectionStringUtility.GetConnectionString();

        public async override Task SaveTranslate(TranslateWord translate)
        {
            if (HasCurrentTranslate(translate.Translate) || HasCurrentWord(translate.Word))
            {
                Status = AddStatus.HAS_TRANSLATE;
                return;
            }

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

                await SortTranslates(connection);

                if (connection.State == ConnectionState.Open)
                {
                    await connection.CloseAsync();
                }
            }
        }

        public override bool HasCurrentTranslate(string translate)
        {
            // TODO: Реализация проверки в БД
            return Words.translates.Any(t =>
                t.Translate.Trim().ToLower() == translate.Trim().ToLower());
        }

        public override bool HasCurrentWord(string word)
        {
            // TODO: Реализация проверки в БД
            return Words.translates.Any(t =>
                t.Word.Trim().ToLower() == word.Trim().ToLower());
        }

        public async override Task SortTranslates()
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

        public async Task SortTranslates(SqlConnection connection)
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
