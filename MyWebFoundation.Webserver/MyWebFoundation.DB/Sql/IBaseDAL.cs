using MyWebFoundation.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebFoundation.DB.Sql
{
    public interface IBaseDAL : IDisposable
    {

        /// <summary>
        /// 约束是为了正确的调用，才能int id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        T Find<T, TKey>(int id) where T : BaseModel<TKey>;

        List<T> FindAll<T, TKey>() where T : BaseModel<TKey>;

        void Update<T, TKey>(T t) where T : BaseModel<TKey>;

        void Insert<T, TKey>(T t) where T : BaseModel<TKey>;

        void Delete<T, TKey>(int id) where T : BaseModel<TKey>;


    }
}
