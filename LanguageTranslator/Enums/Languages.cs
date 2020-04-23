using System.ComponentModel.DataAnnotations;

namespace LanguageTranslator.Enums
{
    public enum Languages
    {
        [Required(ErrorMessage = "Выберите язык.")]
        [Display(Name = "Русский")]
        Russian,

        [Required(ErrorMessage = "Выберите язык.")]
        [Display(Name = "Английский")]
        English
    }
}
