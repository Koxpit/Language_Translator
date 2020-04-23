using LanguageTranslator.Interfaces;
using LanguageTranslator.Models;
using LanguageTranslator.Services;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageTranslator.Data
{
    public class Words
    {
        public static LinkedList<TranslateWord> translates = new LinkedList<TranslateWord>();
    }
}
