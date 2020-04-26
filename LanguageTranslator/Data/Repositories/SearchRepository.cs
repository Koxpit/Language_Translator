using LanguageTranslator.Interfaces;
using LanguageTranslator.Models;
using System.Linq;

namespace LanguageTranslator.Data.Repositories
{
    public class SearchRepository : ISearch
    {
        public string FindTranslate(string word)
        {
            TranslateWord result = Words.translates
                .FirstOrDefault(item => item.Word.Text.Trim().ToLower() == word.Trim().ToLower());

            if (result == null)
            {
                return "Перевод не найден";
            }

            return result.Translate.Text;
        }
    }
}
