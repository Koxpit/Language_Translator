using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace LanguageTranslator.Models
{
    [Serializable]
    public class TranslateWordModel
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public WordModel WordModel { get; set; }

        public TranslateModel TranslateModel { get; set; }
    }
}
