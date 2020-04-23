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
        public override void SaveTranslate(TranslateWordModel translate)
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

        private void SerializeData(TranslateWordModel translates)
        {
            using (FileStream fs = new FileStream("ru-en.dat", FileMode.Append))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, translates);
            }
        }

        private LinkedList<TranslateWordModel> DeserializeData()
        {
            LinkedList<TranslateWordModel> translates = new LinkedList<TranslateWordModel>();

            using (FileStream fs = new FileStream("ru-en.dat", FileMode.OpenOrCreate))
            {
                if (fs.Length != 0)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    while (fs.Position != fs.Length)
                    {
                        translates.AddLast((TranslateWordModel)formatter.Deserialize(fs));
                    }
                    return translates;
                }
            }

            return translates;
        }

        public override bool HasTranslate(TranslateWordModel translate)
        {
            LinkedList<TranslateWordModel> translates = DeserializeData();
            bool hasWord, hasTranslate;

            hasWord = translates.Any(t =>
                t.WordModel.Word.Trim().ToLower() == translate.WordModel.Word.Trim().ToLower());

            hasTranslate = translates.Any(t =>
                t.TranslateModel.Translate.Trim().ToLower() == translate.TranslateModel.Translate.Trim().ToLower());

            if (hasWord || hasTranslate)
            {
                return true;
            }

            return false;
        }
    }
}
