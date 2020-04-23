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
                return;
            }

            Status = AddStatus.HAS_NOT_TRANSLATE;

            LinkedList<TranslateWord> translates = DeserializeData();
            translates.AddLast(translate);

            SerializeData(translates);
        }

        private void SerializeData(LinkedList<TranslateWord> translates)
        {
            using (FileStream fs = new FileStream("ru-en.dat", FileMode.OpenOrCreate))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, translates);
            }
        }

        private LinkedList<TranslateWord> DeserializeData()
        {
            using (FileStream fs = new FileStream("ru-en.dat", FileMode.OpenOrCreate))
            {
                if (fs.Length != 0)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return (LinkedList<TranslateWord>)formatter.Deserialize(fs);
                }
            }
            return new LinkedList<TranslateWord>();
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
