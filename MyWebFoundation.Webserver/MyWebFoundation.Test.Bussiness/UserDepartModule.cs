using MyWebFoundation.Test.Data;
using MyWebFoundation.Test.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWebFoundation.Framework.Extensions;

namespace MyWebFoundation.Test.Bussiness
{
    internal class UserDepartModule : IUserDepartModule
    {

        public UserDepartModule(DbContext context)
        {
            Context = context;
            DepartmentList = new DepartmentList(context);
            UserList = new UserList(context);
        }
        protected DbContext Context { get; private set; }
        public IDepartmentList DepartmentList { get; private set; }
        public IUserList UserList { get; private set; }

        public tb_Department GetDepartAssembleByDepartId(int id)
        {
            try
            {
                tb_Department department = DepartmentList.Find(id);
                List<tb_User> userList = UserList.Query(_ => _.DepartmentId == id).ToList();
                department.tb_Users.First();
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
