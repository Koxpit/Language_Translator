using LanguageTranslator.Enums;
using LanguageTranslator.Models;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace LanguageTranslator.Data
{
    public class DatabaseSaver : IDataSaver
    {
        private string Connection { get; } = Startup.ConnectionString;

        public async override void SaveTranslate(TranslateWordModel translate)
        {
            if (HasTranslate(translate))
            {
                Status = AddStatus.HAS_TRANSLATE;
                return;
            }

            Status = AddStatus.HAS_NOT_TRANSLATE;

            using (SqlConnection connection = new SqlConnection(Connection))
            {
                string sqlExpression = "INSERT INTO RU_EN (Word, Translate, WordLanguage, TranslateLanguage) VALUES (@Word, @Translate, @WordLanguage, @TranslateLanguage)";
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    await connection.OpenAsync();
                    command.Parameters.AddWithValue("@Word", translate.WordModel.Word);
                    command.Parameters.AddWithValue("@Translate", translate.TranslateModel.Translate);
                    command.Parameters.AddWithValue("@WordLanguage", (int)translate.WordModel.Language);
                    command.Parameters.AddWithValue("@TranslateLanguage", (int)translate.TranslateModel.Language);
                    await command.ExecuteNonQueryAsync();
                }

                if (connection.State == ConnectionState.Open)
                {
                    await connection.CloseAsync();
                }
            }
        }

        public override bool HasTranslate(TranslateWordModel translate)
        {
            // TODO: Реализация проверки в БД

            bool hasWord, hasTranslate;

            hasWord = Words.translates.Any(t =>
                t.WordModel.Word.Trim().ToLower() == translate.WordModel.Word.Trim().ToLower());

            hasTranslate = Words.translates.Any(t =>
                t.TranslateModel.Translate.Trim().ToLower() == translate.TranslateModel.Translate.Trim().ToLower());

            if (hasWord || hasTranslate)
            {
                return true;
            }

            return false;
        }
    }
}
