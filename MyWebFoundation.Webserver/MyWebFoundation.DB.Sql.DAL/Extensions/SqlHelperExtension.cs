using MyWebFoundation.Framework.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyWebFoundation.DB.Sql.DAL.Extensions
{
    public static class SqlHelperExtension
    {
        public static string GetColumnName(this PropertyInfo prop)
        {
            if (prop.IsDefined(typeof(ColumnAttribute), true))
            {
                ColumnAttribute attribute = (ColumnAttribute)prop.GetCustomAttribute(typeof(ColumnAttribute), true);
                return attribute.GetColumnName();
            }
            else
            {
                return prop.Name;
            }
        }

        public static string GetTableName<T>(this T oObject) where T : class
        {
            Type type = oObject.GetType();
            if (type.IsDefined(typeof(TableAttribute), true))
            {
                TableAttribute attribute = (TableAttribute)type.GetCustomAttribute(typeof(TableAttribute), true);
                return attribute.GetTableName();
            }
            else
            {
                return type.Name;
            }
        }
    }
}
