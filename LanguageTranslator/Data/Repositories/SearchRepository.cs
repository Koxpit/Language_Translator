using LanguageTranslator.Interfaces;
using LanguageTranslator.Models;
using System.Linq;

namespace LanguageTranslator.Data.Repositories
{
    public class SearchRepository : ISearch
    {
        public string FindTranslate(string word)
        {
            TranslateWordModel result = Words.translates
                .FirstOrDefault(item => item.WordModel.Word.Trim().ToLower() == word.Trim().ToLower());

            if (result == null)
            {
                return "Перевод не найден";
            }

            return result.TranslateModel.Translate;
        }
    }
}
