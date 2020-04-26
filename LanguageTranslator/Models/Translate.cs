﻿using LanguageTranslator.Enums;
using LanguageTranslator.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace LanguageTranslator.Models
{
    [Serializable]
    public class Translate
    {
        [MinLength(1)]
        [StringLength(20)]
        [Required(ErrorMessage = "Перевод должен быть длиной от 1 до 20 символов.")]
        [Display(Name = "Перевод")]
        public string Text { get; set; }

        public int LanguageId { get; set; }
    }
}
