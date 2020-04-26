using LanguageTranslator.Interfaces;
using LanguageTranslator.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageTranslator.Data
{
    public class MockLanguages
    {
        private static readonly MockLanguages instance = new MockLanguages(); // JsonConvert.DeserializeObject<LanJSON>(File.ReadAllText("Langs.json"));

        [JsonProperty("Languages")]
        public IEnumerable<ILanguage> LanguagesList { get; }

        private MockLanguages()
        {
            LanguagesList = new List<Language>
            {
                new Language
                {
                    Id = 1,
                    Name = "Russian",
                    UniCode = "[\u0400-\u04FF]+",
                    Acronym = "RU"
                },
                new Language
                {
                    Id = 2,
                    Name = "English",
                    UniCode = "[\u0000-\u007F]+",
                    Acronym = "EN"
                }
            };
        }

        public static MockLanguages GetInstance()
        {
            return instance;
        }
    }
}
