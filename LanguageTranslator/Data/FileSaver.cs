using LanguageTranslator.Enums;
using LanguageTranslator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace LanguageTranslator.Data
{
    public class FileSaver : IDataSaver
    {
        public override void SaveTranslate(TranslateWord translate)
        {
            if (HasTranslate(translate))
            {
                Status = AddStatus.HAS_TRANSLATE;
            }
            else
            {
                Status = AddStatus.HAS_NOT_TRANSLATE;
                SerializeData(translate);
            }
        }

        private void SerializeData(TranslateWord translates)
        {
            using (FileStream fs = new FileStream("ru-en.dat", FileMode.Append))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, translates);
            }
        }

        private LinkedList<TranslateWord> DeserializeData()
        {
            LinkedList<TranslateWord> translates = new LinkedList<TranslateWord>();

            using (FileStream fs = new FileStream("ru-en.dat", FileMode.OpenOrCreate))
            {
                if (fs.Length != 0)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    while (fs.Position != fs.Length)
                    {
                        translates.AddLast((TranslateWord)formatter.Deserialize(fs));
                    }
                    return translates;
                }
            }

            return translates;
        }

        public override bool HasTranslate(TranslateWord translate)
        {
            LinkedList<TranslateWord> translates = DeserializeData();
            bool hasWord, hasTranslate;

            hasWord = translates.Any(t =>
                t.Word.Trim().ToLower() == translate.Word.Trim().ToLower());

            hasTranslate = translates.Any(t =>
                t.Translate.Trim().ToLower() == translate.Translate.Trim().ToLower());

            if (hasWord || hasTranslate)
            {
                return true;
            }

            return false;
        }

        public override void SortTranslates()
        {
            //TODO: реализовать сортировку записей файла.
        }
    }
}
