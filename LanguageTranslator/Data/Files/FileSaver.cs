using LanguageTranslator.Enums;
using LanguageTranslator.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

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

        public override bool HasTranslate(TranslateWord translate)
        {
            LinkedList<TranslateWord> translates = DeserializeData();
            bool hasWord, hasTranslate;

            hasWord = translates.Any(t =>
                t.Word.Text.Trim().ToLower() == translate.Word.Text.Trim().ToLower());

            hasTranslate = translates.Any(t =>
                t.Translate.Text.Trim().ToLower() == translate.Translate.Text.Trim().ToLower());

            if (hasWord || hasTranslate)
            {
                return true;
            }

            return false;
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

        private void SerializeData(TranslateWord translates)
        {
            using (FileStream fs = new FileStream("ru-en.dat", FileMode.Append))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, translates);
            }
        }
    }
}
