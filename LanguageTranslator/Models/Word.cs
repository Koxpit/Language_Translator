using LanguageTranslator.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace LanguageTranslator.Models
{
    [Serializable]
    public class Word
    {
        [MinLength(1)]
        [StringLength(20)]
        [Required(ErrorMessage = "Исходное слово должно быть длиной от 1 до 20 символов.")]
        [Display(Name = "Исходное слово")]
        public string Text { get; set; }

        public int LanguageId { get; set; }
    }
}
