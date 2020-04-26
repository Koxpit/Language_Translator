using LanguageTranslator.Enums;
using LanguageTranslator.Interfaces;
using LanguageTranslator.Models;
using System.Data.SqlClient;

namespace LanguageTranslator.Data
{
    public class DBInitializer : IInitializer
    {
        public async void Initialize()
        {
            using (SqlConnection connection = new SqlConnection(Startup.ConnectionString))
            {
                string sqlExpression = "SELECT * FROM TranslateWord";
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    await connection.OpenAsync();

                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        Words.translates.AddLast(new TranslateWord()
                        {
                            // TODO: Get objects.
                        });
                    }
                    await connection.CloseAsync();
                }
            }
        }
    }
}
