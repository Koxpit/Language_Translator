using LanguageTranslator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageTranslator.Data
{
    public class FileSaver : IDataSaver
    {
        public override bool HasCurrentTranslate(string word)
        {
            throw new NotImplementedException();
        }

        public override bool HasCurrentWord(string word)
        {
            throw new NotImplementedException();
        }

        public override Task SaveTranslate(TranslateWord translate)
        {
            throw new NotImplementedException();
        }

        public override Task SortTranslates()
        {
            throw new NotImplementedException();
        }
    }
}
