using LanguageTranslator.Models;
using System.Collections.Generic;
using System.Linq;

namespace LanguageTranslator.Data
{
    public class Words
    {
        public static LinkedList<TranslateWord> translates = new LinkedList<TranslateWord>();

        public static IOrderedEnumerable<TranslateWord> GetSortedTranslates()
        {
            return translates.OrderBy(w => w.Word.Text);
        }
    }
}
