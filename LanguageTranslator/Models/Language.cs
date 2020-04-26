using LanguageTranslator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageTranslator.Models
{
    public class Language : ILanguage
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string UniCode { get; set; }

        public string Acronym { get; set; }
    }
}
