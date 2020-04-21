using LanguageTranslator.Enums;
using LanguageTranslator.Interfaces;
using LanguageTranslator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task Add(TranslateWord trans)
        {
            await saver.SaveTranslate(trans);

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

            Words.translates.OrderBy(w => w.Word);
        }

        public bool IsCorrectLanguage(TranslateWord trans)
        {
            bool translateIsEn = true, wordIsRu = true;

            foreach (char ch in trans.Word.Trim().ToLower())
            {
                if (ch >= 'а' && ch <= 'я')
                    wordIsRu = true;
                else
                    wordIsRu = false;
            }

            foreach (char ch in trans.Translate.Trim().ToLower())
            {
                if (ch >= 'a' && ch <= 'z')
                    translateIsEn = true;
                else
                    translateIsEn = false;
            }

            if (wordIsRu && translateIsEn)
                return true;
            else
                return false;
        }
    }
}
