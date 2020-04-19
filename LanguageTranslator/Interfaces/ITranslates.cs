using LanguageTranslator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageTranslator.Interfaces
{
    public interface ITranslates
    {
        void Add(TranslateWord trans);
        bool HasCurrentWord(string word);
        bool HasTranslateWord(string translate);
    }
}
