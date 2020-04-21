using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageTranslator.Services
{
    public class ConnectionStringUtility
    {
        public static string GetConnectionString()
        {
            return Startup.ConnectionString;
        }
    }
}
