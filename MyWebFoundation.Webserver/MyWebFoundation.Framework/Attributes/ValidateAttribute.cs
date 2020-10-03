using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyWebFoundation.Framework.Attributes
{
    public abstract class AbstractValidateAttribute : Attribute
    {
        public abstract bool Validate(object oValue);
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class LengAttribute : AbstractValidateAttribute
    {
        private int _Min = 0;
        private int _Max = 0;
        public LengAttribute(int min, int max)
        {
            this._Min = min;
            this._Max = max;
        }


        public override bool Validate(object value)//" "
        {
            if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
            {
                int length = value.ToString().Length;
                if (length > this._Min && length < this._Max)
                {
                    return true;
                }
            }
            return false;
        }
    }
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class LongAttribute : AbstractValidateAttribute
    {
        private long _Min = 0;
        private long _Max = 0;
        public LongAttribute(long min, long max)
        {
            this._Min = min;
            this._Max = max;
        }


        public override bool Validate(object value)//" "
        {
            //可以改成一句话
            if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
            {
                if (long.TryParse(value.ToString(), out long lResult))
                {
                    if (lResult > this._Min && lResult < this._Max)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class RequirdValidateAttribute : AbstractValidateAttribute
    {
        public override bool Validate(object oValue)
        {
            return oValue != null;
        }
    }
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class EmailAttribute : AbstractValidateAttribute
    {
        public override bool Validate(object oValue)
        {
            return oValue != null
                && Regex.IsMatch(oValue.ToString(), @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        }
    }
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class MobileAttribute : AbstractValidateAttribute
    {
        public override bool Validate(object oValue)
        {
            if (oValue == null)
            {
                return false;
            }
            else
            {
                return Regex.IsMatch(oValue.ToString(), @"^[1]+[3,5]+\d{9}");
            }
        }
    }
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class RegexAttribute : AbstractValidateAttribute
    {
        private string _RegexExpression = string.Empty;
        public RegexAttribute(string regex)
        {
            this._RegexExpression = regex;
        }

        public override bool Validate(object oValue)
        {
            if (oValue == null)
            {
                return false;
            }
            else
            {
                return Regex.IsMatch(oValue.ToString(), _RegexExpression);
            }
        }
    }
}
