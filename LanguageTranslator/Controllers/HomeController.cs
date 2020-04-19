using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LanguageTranslator.Models;
using LanguageTranslator.Data;
using System.Linq;
using LanguageTranslator.Interfaces;
using LanguageTranslator.ViewModels;

namespace LanguageTranslator.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataSaver saver;
        private readonly ITranslates translates;
        private readonly ISearch search;

        public HomeController(IDataSaver saver, ITranslates translates, ISearch search)
        {
            this.saver = saver;
            this.translates = translates;
            this.search = search;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Translate(TranslateWord trans)
        {
            if (ModelState.IsValid)
            {
                if (HasCurrentTranslate(trans))
                {
                    return View();
                }

                translates.Add(trans);

                saver.Save();

                return RedirectToAction("Home", "Index");
            }
            return View();
        }

        private bool HasCurrentTranslate(TranslateWord trans)
        {
            bool hasWord = translates.HasCurrentWord(trans.Word);
            bool hasTranslate = translates.HasTranslateWord(trans.Translate);

            if (hasWord)
            {
                ViewData["StatusTranslate"] = "Исходное слово уже существует.";
                return true;
            }

            if (hasTranslate)
            {
                ViewData["StatusTranslate"] = "Перевод данного слова уже существует";
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
