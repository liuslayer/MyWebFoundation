using MyWebFoundation.DB.Sql.IDAL;
using MyWebFoundation.Framework.Config;
using MyWebFoundation.Framework.Extensions;
using MyWebFoundation.Framework.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebFoundation.DB.Sql.DAL
{
    public class BaseDAL : IBaseDAL
    {

        /// <summary>
        /// 约束是为了正确的调用，才能int id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        public T Find<T>(int id) where T : BaseModel
        {
            Type type = typeof(T);
            string sql = $"{TSqlHelper<T>.FindSql}{id};";
            T t = null;
            using (SqlConnection conn = new SqlConnection(AppConfig.SqlServerConnString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<T> list = this.ReaderToList<T>(reader);
                t = list.FirstOrDefault();

                //SqlDataAdapter adapter = new SqlDataAdapter(command);
                //DataSet ds = new DataSet();
                //adapter.Fill(ds);
                //DataTable dt = ds.Tables[0];
                //List<T> list = this.ReaderToList<T>(dt);
                //t = list.FirstOrDefault();
            }
            return t;
        }

        public List<T> FindAll<T>() where T : BaseModel
        {
            Type type = typeof(T);
            string sql = TSqlHelper<T>.FindAllSql;
            List<T> list = new List<T>();
            using (SqlConnection conn = new SqlConnection(AppConfig.SqlServerConnString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                list = this.ReaderToList<T>(reader);
            }
            return list;
        }

        public void Update<T>(T t) where T : BaseModel
        {
            if (!t.Validate<T>())
            {
                throw new Exception("数据不正确");
            }

            Type type = typeof(T);
            var propArray = type.GetProperties().Where(p => !p.Name.Equals("Id"));
            string columnString = string.Join(",", propArray.Select(p => $"[{p.GetColumnName()}]=@{p.GetColumnName()}"));
            var parameters = propArray.Select(p => new SqlParameter($"@{p.GetColumnName()}", p.GetValue(t) ?? DBNull.Value)).ToArray();
            //必须参数化  否则引号？  或者值里面还有引号
            string sql = $"UPDATE [{type.Name}] SET {columnString} WHERE Id={t.Id}";
            using (SqlConnection conn = new SqlConnection(AppConfig.SqlServerConnString))
            {
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddRange(parameters);
                    conn.Open();
                    int iResult = command.ExecuteNonQuery();
                    if (iResult == 0)
                        throw new Exception("Update数据不存在");
                }
            }
        }

        /// <summary>
        /// 自己完成
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public void Insert<T>(T t) where T : BaseModel
        {
        }
        /// <summary>
        /// 自己完成
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        public void Delete<T>(int id) where T : BaseModel
        {
        }
        #region PrivateMethod
        private List<T> ReaderToList<T>(SqlDataReader reader) where T : BaseModel
        {
            Type type = typeof(T);
            List<T> list = new List<T>();
            while (reader.Read())//表示有数据  开始读
            {
                T t = (T)Activator.CreateInstance(type);
                foreach (var prop in type.GetProperties())
                {
                    object oValue = reader[prop.GetColumnName()];
                    if (oValue is DBNull)
                        oValue = null;
                    prop.SetValue(t, oValue);//除了guid和枚举
                }
                list.Add(t);
            }
            return list;
        }

        private List<T> ReaderToList<T>(DataTable table) where T : BaseModel
        {
            Type type = typeof(T);
            List<T> list = new List<T>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow dr = table.Rows[i];
                T t = (T)Activator.CreateInstance(type);
                foreach (var prop in type.GetProperties())
                {
                    object oValue = dr[prop.GetColumnName()];
                    if (oValue is DBNull)
                        oValue = null;
                    prop.SetValue(t, oValue);//除了guid和枚举
                }
            }
            return list;
        }
        #endregion

    }
}
