using MyWebFoundation.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebFoundation.DB.Sql.IDAL
{
    public interface IBaseDAL
    {

        /// <summary>
        /// 约束是为了正确的调用，才能int id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        T Find<T>(int id) where T : BaseModel;

        List<T> FindAll<T>() where T : BaseModel;

        void Update<T>(T t) where T : BaseModel;

        void Insert<T>(T t) where T : BaseModel;

        void Delete<T>(int id) where T : BaseModel;


    }
}
