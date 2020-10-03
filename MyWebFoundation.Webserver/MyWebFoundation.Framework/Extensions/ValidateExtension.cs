using MyWebFoundation.Framework.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebFoundation.Framework.Extensions
{
    public static class ValidateExtension
    {
        public static bool Validate<T>(this T oObject) where T : class
        {
            Type type = oObject.GetType();
            foreach (var prop in type.GetProperties())
            {
                if (prop.IsDefined(typeof(AbstractValidateAttribute), true))
                {
                    object[] attributeArray = prop.GetCustomAttributes(typeof(AbstractValidateAttribute), true);
                    foreach (AbstractValidateAttribute attribute in attributeArray)
                    {
                        if (!attribute.Validate(prop.GetValue(oObject)))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
