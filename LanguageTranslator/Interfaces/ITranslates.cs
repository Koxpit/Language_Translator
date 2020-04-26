using LanguageTranslator.Enums;
using LanguageTranslator.Models;

namespace LanguageTranslator.Interfaces
{
    public interface ITranslates
    {
        void Add(TranslateWord trans);

        AddStatus Status { get; set; }

        bool IsCorrectLanguage(TranslateWord trans);
    }
}
