using LanguageTranslator.Enums;
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
        AddStatus Status { get; set; }
        bool IsCorrectLanguage(TranslateWord trans);
    }
}
