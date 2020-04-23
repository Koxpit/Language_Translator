using LanguageTranslator.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace LanguageTranslator.Models
{
    [Serializable]
    public class WordModel
    {
        [MinLength(1)]
        [StringLength(20)]
        [Required(ErrorMessage = "Исходное слово должно быть длиной от 1 до 20 символов.")]
        public string Word { get; set; }

        public Languages Language { get; set; }
    }
}
