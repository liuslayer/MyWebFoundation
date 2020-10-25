using MyWebFoundation.DB.EF;
using MyWebFoundation.Test.Data;
using MyWebFoundation.Test.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebFoundation.Test.Bussiness
{
    internal class UserList: BaseEntities<tb_User>,IUserList
    {
        #region Identity
        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="context"></param>
        public UserList(DbContext context):base(context)
        {
        }
        #endregion Identity
    }
}
