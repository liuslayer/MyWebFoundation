using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyWebFoundation.DB.Dapper
{
    internal static class DatabaseTypeExtensions
    {
        private class Setting
        {
            public DatabaseType DatabaseType { get; set; }

            public string ProviderName { get; set; }
        }

        private static readonly IDictionary<DatabaseType, Setting> cache = new Dictionary<DatabaseType, Setting>();

        static DatabaseTypeExtensions()
        {
            var enumType = typeof(DatabaseType);
            foreach (DatabaseType value in Enum.GetValues(enumType))
            {
                var name = Enum.GetName(enumType, value);
                var field = enumType.GetRuntimeField(name);
                var providerNameAttribute = field.GetCustomAttribute<ProviderNameAttribute>();

                cache.Add(value, new Setting()
                {
                    DatabaseType = value,
                    ProviderName = providerNameAttribute == null ? null : providerNameAttribute.ProviderName,
                });
            }
        }

        public static string GetProviderName(this DatabaseType kind)
        {
            return cache[kind].ProviderName;
        }
    }
}
