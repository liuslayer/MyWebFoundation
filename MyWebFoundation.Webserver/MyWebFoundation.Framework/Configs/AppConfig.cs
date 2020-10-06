using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebFoundation.Framework.Config
{
    public class AppConfig
    {
        public static string SqlServerConnString = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
    }
}
