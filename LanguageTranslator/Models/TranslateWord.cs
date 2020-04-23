using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace LanguageTranslator.Models
{
    [Serializable]
    public class TranslateWord
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [MinLength(1)]
        [StringLength(20)]
        [Required(ErrorMessage = "Исходное слово должно быть длиной от 1 до 20 символов.")]
        public string Word { get; set; }

        [MinLength(1)]
        [StringLength(20)]
        [Required(ErrorMessage = "Слово-перевод должно быть длиной от 1 до 20 символов.")]
        public string Translate { get; set; }
    }
}
