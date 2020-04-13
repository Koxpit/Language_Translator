using LanguageTranslator.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using LanguageTranslator.Data;
using System.Data;

namespace LanguageTranslator.Controllers
{
    public class CreatorController : Controller
    {
        private readonly IDataSaver saver = new DatabaseSaver();

        [HttpPost]
        public IActionResult AddTranslate(TranslateWord trans)
        {
            if (ModelState.IsValid)
            {
                if (HasCurrentWord(trans.Word))
                {
                    return Content("Исходное слово уже существует.");
                }

                if (HasTranslateWord(trans.Translate))
                {
                    return Content("Перевод данного слова уже существует");
                }

                Words.translates.AddLast(new TranslateWord { Word = trans.Word, Translate = trans.Translate });
                Words.translates.OrderBy(w => w.Word);

                saver.Save();

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ViewResult AddTranslate()
        {
            return View();
        }

        public bool HasCurrentWord(string word)
        {
            return Words.translates.Any(t => t.Word.Trim() == word);
        }

        public bool HasTranslateWord(string translate)
        {
            return Words.translates.Any(t => t.Translate.Trim() == translate);
        }
    }
}
