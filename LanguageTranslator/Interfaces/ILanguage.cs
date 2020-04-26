using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageTranslator.Interfaces
{
    public interface ILanguage
    {
        [JsonProperty("id")]
        int Id { get; }

        [JsonProperty("name")]
        string Name { get; }

        [JsonProperty("code")]
        string UniCode { get; }

        [JsonProperty("acronym")]
        string Acronym { get; }
    }
}
