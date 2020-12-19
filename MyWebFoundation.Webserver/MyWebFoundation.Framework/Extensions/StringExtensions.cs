using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyWebFoundation.Framework.Extensions
{
    public static class StringExtensions
    {
        public static string FormatStr(this string input, params object[] formatingValues)
        {
            if (input.IsEmptyOrWhiteSpace())
            {
                return string.Empty;
            }
            return string.Format(input, formatingValues);
        }

        /// <summary>
        /// format datetime to string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FormatDateTimeStr(this DateTime input)
        {
            return input.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static DateTime ToDate(this string input, bool throwExceptionIfFailed = false)
        {
            DateTime result;
            var valid = DateTime.TryParse(input, out result);
            if (!valid && throwExceptionIfFailed)
            {
                throw new FormatException(string.Format("'{0}' cannot be converted as DateTime", input));
            }
            return result;
        }

        public static int ToInt(this string input, bool throwExceptionIfFailed = false)
        {
            int result;
            var valid = int.TryParse(input, out result);
            if (!valid && throwExceptionIfFailed)
            {
                throw new FormatException(string.Format("'{0}' cannot be converted as int", input));
            }
            return result;
        }

        public static double ToDouble(this string input, bool throwExceptionIfFailed = false)
        {
            double result;
            var valid = double.TryParse(input, NumberStyles.AllowDecimalPoint, new NumberFormatInfo { NumberDecimalSeparator = "." }, out result);
            if (!valid && throwExceptionIfFailed)
            {
                throw new FormatException(string.Format("'{0}' cannot be converted as double", input));
            }
            return result;
        }

        public static string Reverse(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }
            char[] chars = input.ToCharArray();
            Array.Reverse(chars);
            return new String(chars);
        }

        /// <summary>
        /// Matching all capital letters in the input and seperate them with spaces to form a sentence.
        /// If the input is an abbreviation text, no space will be added and returns the same input.
        /// </summary>
        /// <example>
        /// input : HelloWorld
        /// output : Hello World
        /// </example>
        /// <example>
        /// input : BBC
        /// output : BBC
        /// </example>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToSentence(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;
            //return as is if the input is just an abbreviation
            if (Regex.Match(input, "[0-9A-Z]+$").Success)
            {
                return input;
            }
            //add a space before each capital letter, but not the first one.
            var result = Regex.Replace(input, "(\\B[A-Z])", " $1");
            return result;
        }

        public static string GetLast(this string input, int howMany)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            var value = input.Trim();
            return howMany >= value.Length ? value : value.Substring(value.Length - howMany);
        }

        public static string GetFirst(this string input, int howMany)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            var value = input.Trim();
            return howMany >= value.Length ? value : input.Substring(0, howMany);
        }

        public static bool IsEmpty(this string input)
        {
            return string.IsNullOrEmpty(input);
        }

        public static bool IsEmptyOrWhiteSpace(this string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        public static bool IsEmail(this string input)
        {
            var match = Regex.Match(input, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);
            return match.Success;
        }

        public static bool IsPhone(this string input)
        {
            var match = Regex.Match(input, @"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$", RegexOptions.IgnoreCase);
            return match.Success;
        }

        public static bool IsNumber(this string input)
        {
            var match = Regex.Match(input, @"^[0-9]+$", RegexOptions.IgnoreCase);
            return match.Success;
        }

        public static int ExtractNumber(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return 0;

            var match = Regex.Match(input, "[0-9]+", RegexOptions.IgnoreCase);
            return match.Success ? match.Value.ToInt() : 0;
        }

        public static string ExtractEmail(this string input)
        {
            if (input == null || string.IsNullOrWhiteSpace(input)) return string.Empty;

            var match = Regex.Match(input, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);
            return match.Success ? match.Value : string.Empty;
        }

        public static string ExtractQueryStringParamValue(this string queryString, string paramName)
        {
            if (string.IsNullOrWhiteSpace(queryString) || string.IsNullOrWhiteSpace(paramName)) return string.Empty;

            var query = queryString.Replace("?", string.Empty);
            if (!query.Contains("="))
            {
                return string.Empty;
            }
            var queryValues = query.Split('&').Select(piQ => piQ.Split('=')).ToDictionary(piKey => piKey[0].ToLower().Trim(), piValue => piValue[1]);
            string result;
            var found = queryValues.TryGetValue(paramName.ToLower().Trim(), out result);
            return found ? result : string.Empty;
        }

        public static bool ToBool(this string boolstring, bool throwErrorIfInvalid = false)
        {
            if (throwErrorIfInvalid)
            {
                return bool.Parse(boolstring);
            }
            else
            {
                bool result;
                if (bool.TryParse(boolstring, out result))
                {
                    return result;
                }
                else
                {
                    return false;
                }
            }
        }

        public static string JoinWith(this IEnumerable<string> stringList, string divider = "")
        {
            StringBuilder sb = new StringBuilder();

            var i = 0;
            var max = stringList.Count() - 1;
            foreach (var item in stringList)
            {
                if (string.IsNullOrEmpty(item))
                {
                    i++;
                    continue;
                }
                sb.Append(item);
                if (i < max)
                {
                    sb.Append(divider);
                }
                i++;
            }

            return sb.ToString();
        }

        public static bool Like(this string toSearch, string toFind)
        {
            return new Regex(@"\A" + new Regex(@"\.|\$|\^|\{|\[|\(|\||\)|\*|\+|\?|\\").Replace(toFind, ch => @"\" + ch).Replace('_', '.').Replace("%", ".*") + @"\z", RegexOptions.Singleline).IsMatch(toSearch);
        }

        public static int Hash(this string str)
        {
            if (str == null)
            {
                return 0;
            }
            int hash = str.Length;
            for (int i = 0; i != str.Length; ++i)
            {
                hash = (hash << 5) - hash + str[i];
            }
            return hash;
        }
    }
}
