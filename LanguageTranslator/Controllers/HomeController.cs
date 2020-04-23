using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LanguageTranslator.Models;
using LanguageTranslator.Interfaces;
using LanguageTranslator.ViewModels;
using System.Threading.Tasks;
using LanguageTranslator.Enums;

namespace LanguageTranslator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITranslates translates;
        private readonly ISearch search;

        public HomeController(ITranslates translates, ISearch search)
        {
            this.translates = translates;
            this.search = search;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Translate(TranslateWord trans)
        {
            if (ModelState.IsValid)
            {
                if (!IsCorrectLanguage(trans))
                {
                    return View();
                }

                translates.Add(trans);

                if (HasCurrentTranslate())
                {
                    return View();
                }

                return View();
            }

            return View();
        }

        private bool IsCorrectLanguage(TranslateWord trans)
        {
            if (!translates.IsCorrectLanguage(trans))
            {
                ViewData["StatusTranslate"] = "Перевод не добавлен. Исходное слово должно быть русское, а перевод английским!";
                return false;
            }

            return true;
        }

        private bool HasCurrentTranslate()
        {
            if (translates.Status == AddStatus.HAS_TRANSLATE)
            {
                ViewData["StatusTranslate"] = "Перевод уже существует.";
                return true;
            }

            ViewData["StatusTranslate"] = "Перевод успешно добавлен!";
            return false;
        }

        [HttpGet]
        public ViewResult Translate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(SearchViewModel trans)
        {
            if (ModelState.IsValid)
            {
                ViewData["Found"] = search.FindTranslate(trans.Word);
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ViewResult Search()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
