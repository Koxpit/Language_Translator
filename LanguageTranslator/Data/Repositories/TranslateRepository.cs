using LanguageTranslator.Enums;
using LanguageTranslator.Interfaces;
using LanguageTranslator.Models;

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

        public void Add(TranslateWordModel trans)
        {
            saver.SaveTranslate(trans);

            Status = saver.Status;

            if (Status == AddStatus.HAS_TRANSLATE)
            {
                return;
            }

            Words.translates.AddLast(new TranslateWordModel
            {
                WordModel = trans.WordModel,
                TranslateModel = trans.TranslateModel
            });
        }

        public bool IsCorrectLanguage(TranslateWordModel trans)
        {
            
            bool translateIsEn = true, wordIsRu = true;

            foreach (char ch in trans.WordModel.Word.Trim().ToLower())
            {
                if (ch >= 'а' && ch <= 'я')
                    wordIsRu = true;
                else
                    wordIsRu = false;
            }

            foreach (char ch in trans.TranslateModel.Translate.Trim().ToLower())
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
