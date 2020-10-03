using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebFoundation.Framework.Attributes
{
    public class RemarkAttribute : Attribute
    {
        private string _remark = string.Empty;
        public RemarkAttribute(string remark)
        {
            _remark = remark;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get
            {
                return _remark;
            }
            set
            {
                _remark = value;
            }
        }

        public string Description
        {
            get;
            set;
        }
    }
}
