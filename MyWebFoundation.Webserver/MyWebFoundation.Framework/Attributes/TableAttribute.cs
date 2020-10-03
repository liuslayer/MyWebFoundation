using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebFoundation.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class TableAttribute : Attribute
    {
        public TableAttribute(string name)
        {
            this._Name = name;
        }

        private string _Name = null;
        public string GetTableName()
        {
            return this._Name;
        }
    }
}
