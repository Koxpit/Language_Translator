using LanguageTranslator.Enums;
using LanguageTranslator.Models;

namespace LanguageTranslator.Data
{
    public abstract class IDataSaver
    {
        public AddStatus Status { get; set; }

        public abstract void SaveTranslate(TranslateWord translate);

        public abstract bool HasTranslate(TranslateWord translate);
    }
}
