using MyWebFoundation.Test.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebFoundation.Test.Interface
{
    public interface IUserDepartModule
    {
        tb_Department GetDepartAssembleByDepartId(int id);
    }
}
