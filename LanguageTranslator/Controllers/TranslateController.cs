using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageTranslator.Controllers
{
    public class TranslateController : Controller
    {
        public IActionResult FindTranslate(string word)
        {
            return View();
        }
    }
}
