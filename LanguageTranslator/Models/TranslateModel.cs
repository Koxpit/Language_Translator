using LanguageTranslator.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace LanguageTranslator.Models
{
    [Serializable]
    public class TranslateModel
    {
        [MinLength(1)]
        [StringLength(20)]
        [Required(ErrorMessage = "Слово-перевод должно быть длиной от 1 до 20 символов.")]
        public string Translate { get; set; }

        public Languages Language { get; set; }
    }
}
