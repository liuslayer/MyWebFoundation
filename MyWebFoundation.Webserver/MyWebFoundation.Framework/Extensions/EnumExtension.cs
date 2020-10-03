using MyWebFoundation.Framework.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MyWebFoundation.Framework.Logs;

namespace MyWebFoundation.Framework.Extensions
{
    public static class EnumExtension
    {
        /// <summary>
        /// 获取当前枚举值的Remark
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetRemark(this Enum value)
        {
            string remark = string.Empty;
            Type type = value.GetType();
            FieldInfo fieldInfo = type.GetField(value.ToString());
            object[] attrs = fieldInfo.GetCustomAttributes(typeof(RemarkAttribute), true);
            RemarkAttribute attr = (RemarkAttribute)attrs.FirstOrDefault(a => a is RemarkAttribute);
            if (attr == null)
            {
                remark = fieldInfo.Name;
            }
            else
            {
                remark = attr.Remark;
            }
            return remark;
        }

        /// <summary>
        /// 获取当前枚举的全部Remark
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<KeyValuePair<string, string>> GetAllRemarks(this Enum value)
        {
            Type type = value.GetType();
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
            //ShowAttribute.
            foreach (var field in type.GetFields())
            {
                if (field.FieldType.IsEnum)
                {
                    object tmp = field.GetValue(value);
                    Enum enumValue = (Enum)tmp;
                    int intValue = (int)tmp;
                    result.Add(new KeyValuePair<string, string>(intValue.ToString(), enumValue.GetRemark()));
                }
            }
            return result;
        }
    }
}
