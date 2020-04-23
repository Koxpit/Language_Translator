using LanguageTranslator.Enums;
using LanguageTranslator.Models;

namespace LanguageTranslator.Data
{
    public abstract class IDataSaver
    {
        public AddStatus Status { get; set; }

        public abstract void SaveTranslate(TranslateWordModel translate);

        public abstract bool HasTranslate(TranslateWordModel translate);
    }
}
