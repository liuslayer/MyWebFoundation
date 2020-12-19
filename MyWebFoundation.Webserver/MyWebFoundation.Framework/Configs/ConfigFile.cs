using MyWebFoundation.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyWebFoundation.Framework.Configs
{
    public static class ConfigFile
    {
        public static string SqlServerConnString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static string ReadAppSettingsValue(this Configuration config, string key, string defaultValue = "")
        {
            try
            {
                var AppSetting = config.AppSettings.Settings[key];
                if (AppSetting == null)
                {
                    return defaultValue;
                }
                else
                {
                    return AppSetting.Value;
                }
            }
            catch (Exception ex)
            {
                typeof(ConfigFile).Error(MethodBase.GetCurrentMethod().Name, ex);
                return defaultValue;
            }
        }
    }
}
