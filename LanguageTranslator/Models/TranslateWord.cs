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

        public Word Word { get; set; }

        public Translate Translate { get; set; }
    }
}
