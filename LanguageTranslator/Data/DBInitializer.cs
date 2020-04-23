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
                string sqlExpression = "SELECT * FROM RU_EN";
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    await connection.OpenAsync();

                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        Words.translates.AddLast(new TranslateWordModel()
                        {
                            Id = reader.GetInt32(0),
                            WordModel = new WordModel()
                            {
                                Word = reader.GetString(1),
                                Language = (Languages)reader.GetInt32(3)
                            },
                            TranslateModel = new TranslateModel() {
                               Translate = reader.GetString(2),
                               Language = (Languages)reader.GetInt32(4)
                            }
                        });
                    }
                    await connection.CloseAsync();
                }
            }
        }
    }
}
