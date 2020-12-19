using MyWebFoundation.DB.Dapper;
using MyWebFoundation.Test.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebFoundation.Test.Interface
{
    public interface IUserDepartService : IBaseService
    {
        bool InsertUserDepart(tb_User user, tb_Department department);
    }
}
