using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebFoundation.DB.Dapper
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ProviderNameAttribute : Attribute
    {
        public string ProviderName { get; set; }

        public ProviderNameAttribute(string providerName)
        {
            if (providerName == null)
                throw new ArgumentNullException("providerName");
            this.ProviderName = providerName;
        }
    }
}
