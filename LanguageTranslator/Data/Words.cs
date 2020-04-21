using LanguageTranslator.Models;
using LanguageTranslator.Services;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageTranslator.Data
{
    public static class Words
    {
        public static LinkedList<TranslateWord> translates = new LinkedList<TranslateWord>();

        public static async Task Initialize()
        {
            if (!translates.Any())
            {
                using (SqlConnection connection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Languages;Integrated Security=True"))
                {
                    string sqlExpression = "SELECT * FROM RU_EN";
                    using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                    {
                        await connection.OpenAsync();

                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            translates.AddLast(new TranslateWord
                            {
                                Id = reader.GetInt32(0),
                                Word = reader.GetString(1),
                                Translate = reader.GetString(2)
                            });
                        }
                        await connection.CloseAsync();

                        translates.OrderBy(w => w.Word);
                    }
                }
            }
        }
    }
}
