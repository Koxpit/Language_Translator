using LanguageTranslator.Interfaces;
using LanguageTranslator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageTranslator.Data.Repositories
{
    public class TranslateRepository : ITranslates
    {
        public void Add(TranslateWord trans)
        {
            Words.translates.AddLast(new TranslateWord
            {
                Word = trans.Word.ToLower(),
                Translate = trans.Translate.ToLower()
            });

            Words.translates.OrderBy(w => w.Word);
        }

        public bool HasCurrentWord(string word)
        {
            return Words.translates.Any(t => t.Word.Trim() == word);
        }

        public bool HasTranslateWord(string translate)
        {
            return Words.translates.Any(t => t.Translate.Trim() == translate);
        }
    }
}
