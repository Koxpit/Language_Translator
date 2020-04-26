using LanguageTranslator.Controllers;
using LanguageTranslator.Enums;
using LanguageTranslator.Interfaces;
using LanguageTranslator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LanguageTranslator.Data.Repositories
{
    public class TranslateRepository : ITranslates
    {
        private readonly IDataSaver saver;

        public AddStatus Status { get; set; }

        public TranslateRepository(IDataSaver saver)
        {
            this.saver = saver;
        }

        public void Add(TranslateWord trans)
        {
            saver.SaveTranslate(trans);

            Status = saver.Status;

            if (Status == AddStatus.HAS_TRANSLATE)
            {
                return;
            }

            Words.translates.AddLast(new TranslateWord
            {
                Word = trans.Word,
                Translate = trans.Translate
            });

        }

        public bool IsCorrectLanguage(TranslateWord trans)
        {
            bool translateIsEn = true, wordIsRu = true;
            var translateUniCode = Startup.Langs.Where(l => l.Id == trans.Translate.LanguageId).Select(u => u.UniCode);
            var wordUniCode = Startup.Langs.Where(l => l.Id == trans.Word.LanguageId).Select(u => u.UniCode);

            foreach (char ch in trans.Word.Text)
            {
                if (Regex.IsMatch(ch.ToString(), $@"{wordUniCode}"))
                {
                    translateIsEn = true;
                }
                else
                {
                    translateIsEn = false;
                    break;
                }
            }

            foreach (char ch in trans.Translate.Text)
            {
                if (Regex.IsMatch(ch.ToString(), $@"{translateUniCode}"))
                {
                    translateIsEn = true;
                }
                else
                {
                    translateIsEn = false;
                    break;
                }
            }

            if (wordIsRu && translateIsEn)
                return true;
            else
                return false;
        }
    }
}
