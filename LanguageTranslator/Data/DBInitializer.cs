using LanguageTranslator.Interfaces;
using LanguageTranslator.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

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
                        Words.translates.AddLast(new TranslateWord
                        {
                            Id = reader.GetInt32(0),
                            Word = reader.GetString(1),
                            Translate = reader.GetString(2)
                        });
                    }
                    await connection.CloseAsync();

                    Words.translates.OrderBy(w => w.Word);
                }
            }
        }
    }
}
