using LanguageTranslator.Enums;
using LanguageTranslator.Models;

namespace LanguageTranslator.Interfaces
{
    public interface ITranslates
    {
        void Add(TranslateWordModel trans);

        AddStatus Status { get; set; }

        bool IsCorrectLanguage(TranslateWordModel trans);
    }
}
