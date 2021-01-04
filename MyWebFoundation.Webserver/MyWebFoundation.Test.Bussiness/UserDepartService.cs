using Dapper;
using MyWebFoundation.DB;
using MyWebFoundation.DB.Dapper;
using MyWebFoundation.Framework.Extensions;
using MyWebFoundation.Test.Data;
using MyWebFoundation.Test.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebFoundation.Test.Bussiness
{
    public class UserDepartService : BaseService, IUserDepartService
    {
        public UserDepartService()
            : base(DbManager.FromConfig("PGConn"))
        {

        }

        public bool InsertUserDepart(tb_User user,tb_Department department)
        {
            using (var tran = CreateScopeTransaction())
            {
                try
                {
                    var newId = base.Insert(department);
                    var temp= base.Get<tb_Department>(newId.Value);
                    temp= base.DBManager.Connection.Get<tb_Department>(newId.Value);
                    user.DepartmentId = newId.Value;
                    newId = base.Insert(user);
                    if (tran != null)
                    {
                        tran.Complete();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    this.GetType().Error(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                    return false;
                }
            }
        }
    }
}
