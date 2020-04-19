using LanguageTranslator.Interfaces;
using LanguageTranslator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageTranslator.Data.Repositories
{
    public class SearchRepository : ISearch
    {
        public string FindTranslate(string word)
        {
            TranslateWord result = Words.translates
                .FirstOrDefault(item => item.Word.Trim().ToLower() == word.Trim().ToLower());

            if (result == null)
            {
                return "Перевод не найден";
            }

            return result.Translate;
        }
    }
}
