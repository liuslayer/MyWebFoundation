using MyWebFoundation.DB.Sql.DAL.Extensions;
using MyWebFoundation.Framework.Extensions;
using MyWebFoundation.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebFoundation.DB.Sql.DAL
{
    public class TSqlHelper<T,TKey> where T : BaseModel<TKey>
    {
        static TSqlHelper()
        {
            Type type = typeof(T);
            string columnString = string.Join(",", type.GetProperties().Select(p => $"[{p.GetColumnName()}]"));
            FindSql = $"SELECT {columnString} FROM [{type.Name}] WHERE Id=";
            FindAllSql = $"SELECT {columnString} FROM [{type.Name}];";
        }

        public static string FindSql = null;
        public static string FindAllSql = null;
        //delete  update  insert 
    }
}
