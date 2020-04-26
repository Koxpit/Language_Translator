using LanguageTranslator.Interfaces;
using LanguageTranslator.Models;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace LanguageTranslator.Data
{
    public class FileInitializer : IInitializer
    {
        public void Initialize()
        {
            using (FileStream fs = new FileStream("ru-en.dat", FileMode.OpenOrCreate))
            {
                if (fs.Length == 0)
                {
                    return;
                }
                BinaryFormatter formatter = new BinaryFormatter();
                while (fs.Position != fs.Length)
                {
                    Words.translates.AddLast((TranslateWord)formatter.Deserialize(fs));
                }
            }
        }
    }
}
