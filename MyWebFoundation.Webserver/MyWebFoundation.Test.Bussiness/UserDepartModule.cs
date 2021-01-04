using MyWebFoundation.Test.Data;
using MyWebFoundation.Test.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWebFoundation.Framework.Extensions;
using MyWebFoundation.DB.EF;

namespace MyWebFoundation.Test.Bussiness
{
    internal class UserDepartModule : BaseEntities, IUserDepartModule
    {

        public UserDepartModule()
            : base(new TestDBContext("TestDBContext"))
        {
        }

        public tb_Department GetDepartAssembleByDepartId(int id)
        {
            try
            {
                tb_Department department = base.Find<tb_Department>(id);
                List<tb_User> userList = base.Query<tb_User>(_ => _.DepartmentId == id).ToList();
                department.tb_Users = userList;
                return department;
            }
            catch(Exception ex)
            {
                this.GetType().Fatal(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                return null;
            }

        }
    }
}
