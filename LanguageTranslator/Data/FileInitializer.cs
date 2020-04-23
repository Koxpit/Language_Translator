using LanguageTranslator.Interfaces;
using LanguageTranslator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace LanguageTranslator.Data
{
    public class FileInitializer : IInitializer
    {
        public void Initialize()
        {
            if (!File.Exists("ru-en.dat"))
            {
                FileStream create = File.Create("ru-en.dat");
                create.Close();
            }
            else
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
}
