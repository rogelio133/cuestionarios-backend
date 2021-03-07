using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Data
{
    public class Helper
    {
        public static string GetConnection() => ConfigurationManager.ConnectionStrings["dbCS"].ConnectionString ?? string.Empty;
    }
}
