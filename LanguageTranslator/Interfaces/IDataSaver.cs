
using LanguageTranslator.Enums;
using LanguageTranslator.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LanguageTranslator.Data
{
    public abstract class IDataSaver
    {
        public AddStatus Status { get; set; }

        public abstract void SaveTranslate(TranslateWord translate);
        public abstract bool HasTranslate(TranslateWord translate);
        public abstract void SortTranslates();
    }
}
