
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

        public abstract Task SaveTranslate(TranslateWord translate);
        public abstract bool HasCurrentWord(string word);
        public abstract bool HasCurrentTranslate(string word);
        public abstract Task SortTranslates();
    }
}
