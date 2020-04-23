using LanguageTranslator.Models;
using System.Collections.Generic;
using System.Linq;

namespace LanguageTranslator.Data
{
    public class Words
    {
        public static LinkedList<TranslateWordModel> translates = new LinkedList<TranslateWordModel>();

        public static IOrderedEnumerable<TranslateWordModel> SortTranslates()
        {
            IOrderedEnumerable<TranslateWordModel> result = translates.OrderBy(w => w.WordModel.Word);
            return result;
        }
    }
}
