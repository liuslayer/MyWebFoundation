using MyWebFoundation.Framework.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyWebFoundation.DB.EF
{
    public interface IBaseEntities<T> : IDisposable where T : class
    {
        /// <summary>
        /// 根据id查询实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Find(int id);

        /// <summary>
        /// 提供对单表的查询，
        /// </summary>
        /// <returns>IQueryable类型集合</returns>
        IQueryable<T> Set();

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="funcWhere"></param>
        /// <returns></returns>
        IQueryable<T> Query(Expression<Func<T, bool>> funcWhere);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="funcWhere"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="funcOrderby"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        PageResult<T> QueryPage<S>(Expression<Func<T, bool>> funcWhere, int pageSize, int pageIndex, Expression<Func<T, S>> funcOrderby, bool isAsc = true);

        /// <summary>
        /// 新增数据，即时Commit
        /// </summary>
        /// <param name="t"></param>
        /// <returns>返回带主键的实体</returns>
        T Insert(T t,bool isTransaction);

        /// <summary>
        /// 新增数据，即时Commit
        /// 多条sql 一个连接，事务插入
        /// </summary>
        /// <param name="tList"></param>
        /// <returns>返回带主键的集合</returns>
        IEnumerable<T> Insert(IEnumerable<T> tList, bool isTransaction);

        /// <summary>
        /// 更新数据，即时Commit
        /// </summary>
        /// <param name="t"></param>
        void Update(T t, bool isTransaction);

        /// <summary>
        /// 更新数据，即时Commit
        /// </summary>
        /// <param name="tList"></param>
        void Update(IEnumerable<T> tList, bool isTransaction);

        /// <summary>
        /// 根据主键删除数据，即时Commit
        /// </summary>
        /// <param name="t"></param>
        void Delete(int Id, bool isTransaction);

        /// <summary>
        /// 删除数据，即时Commit
        /// </summary>
        /// <param name="t"></param>
        void Delete(T t, bool isTransaction);

        /// <summary>
        /// 删除数据，即时Commit
        /// </summary>
        /// <param name="tList"></param>
        void Delete(IEnumerable<T> tList, bool isTransaction);

        /// <summary>
        /// 立即保存全部修改
        /// </summary>
        void Commit();

        /// <summary>
        /// 执行sql 返回集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IQueryable<T> ExcuteQuery(string sql, SqlParameter[] parameters);

        /// <summary>
        /// 执行sql，无返回
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        void Excute(string sql, SqlParameter[] parameters);
    }
}
