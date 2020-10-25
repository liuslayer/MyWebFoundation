using MyWebFoundation.DB.Sql.DAL.Extensions;
using MyWebFoundation.DB.Sql.IDAL;
using MyWebFoundation.Framework.Config;
using MyWebFoundation.Framework.Extensions;
using MyWebFoundation.Framework.Models;
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
        public T Find<T,TKey>(int id) where T : BaseModel<TKey>
        {
            Type type = typeof(T);
            string sql = $"{TSqlHelper<T,TKey>.FindSql}{id};";
            T t = null;
            using (SqlConnection conn = new SqlConnection(AppConfig.SqlServerConnString))
            {
                using (SqlCommand command = new SqlCommand(sql, conn))//释放SqlCommand不会释放SqlConnection
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    List<T> list = this.ReaderToList<T, TKey>(reader);
                    t = list.FirstOrDefault();
                }
                //SqlDataAdapter adapter = new SqlDataAdapter(command);
                //DataSet ds = new DataSet();
                //adapter.Fill(ds);
                //DataTable dt = ds.Tables[0];
                //List<T> list = this.ReaderToList<T>(dt);
                //t = list.FirstOrDefault();
            }
            return t;
        }

        public List<T> FindAll<T, TKey>() where T : BaseModel<TKey>
        {
            Type type = typeof(T);
            string sql = TSqlHelper<T, TKey>.FindAllSql;
            List<T> list = new List<T>();
            using (SqlConnection conn = new SqlConnection(AppConfig.SqlServerConnString))
            {
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    list = this.ReaderToList<T, TKey>(reader);
                }
            }
            return list;
        }

        public void Update<T, TKey>(T t) where T : BaseModel<TKey>
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

        public void Insert<T, TKey>(T t) where T : BaseModel<TKey>
        {
            if (!t.Validate<T>())
            {
                throw new Exception("数据不正确");
            }

            Type type = typeof(T);
            var propArray = type.GetProperties().Where(p => !p.Name.Equals("Id"));
            string columnString = string.Join(",", propArray.Select(p => $"[{p.GetColumnName()}]"));
            string valueString = string.Join(",", propArray.Select(p => $"@{p.GetColumnName()}"));
            var parameters = propArray.Select(p => new SqlParameter($"@{p.GetColumnName()}", p.GetValue(t) ?? DBNull.Value)).ToArray();
            //必须参数化  否则引号？  或者值里面还有引号
            string sql = $"INSERT INTO [{type.Name}] {columnString} VALUES {valueString}";
            using (SqlConnection conn = new SqlConnection(AppConfig.SqlServerConnString))
            {
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddRange(parameters);
                    conn.Open();
                    int iResult = command.ExecuteNonQuery();
                    if (iResult == 0)
                        throw new Exception("Insert数据失败");
                }
            }
        }

        public void Delete<T, TKey>(int id) where T : BaseModel<TKey>
        {
            Type type = typeof(T);
            string sql = $"DELETE FROM [{type.Name}] WHERE Id={id}";
            using (SqlConnection conn = new SqlConnection(AppConfig.SqlServerConnString))
            {
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    int iResult = command.ExecuteNonQuery();
                    if (iResult == 0)
                        throw new Exception("Delete数据失败");
                }
            }
        }

        public int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(AppConfig.SqlServerConnString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    //foreach (SqlParameter param in parameters)
                    //{
                    //    cmd.Parameters.Add(param);
                    //}
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(AppConfig.SqlServerConnString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }
            }
        }

        //只用来执行查询结果比较少的sql
        public DataTable ExecuteDataTable(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(AppConfig.SqlServerConnString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataSet dataset = new DataSet();
                        adapter.Fill(dataset);
                        return dataset.Tables[0];
                    }
                }
            }
        }

        /// <summary>
        /// 更新DataTable,通常和ExecuteDataTable配合用，sql参数一致
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="comText"></param>
        /// <param name="sql"></param>
        public void UpdateDataTable(DataTable dataTable, string sql)
        {
            if (dataTable == null)
            {
                return;
            }
            using (SqlConnection conn = new SqlConnection(AppConfig.SqlServerConnString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);
                        adapter.InsertCommand = sqlCommandBuilder.GetInsertCommand();
                        adapter.DeleteCommand = sqlCommandBuilder.GetDeleteCommand();
                        adapter.UpdateCommand = sqlCommandBuilder.GetUpdateCommand();
                        adapter.Update(dataTable);
                        dataTable.AcceptChanges();
                        adapter.RowUpdating += new SqlRowUpdatingEventHandler(sqlDataAdapter_RowUpdating);

                    }
                }
            }
        }

        #region PrivateMethod
        void sqlDataAdapter_RowUpdating(object sender, SqlRowUpdatingEventArgs e)
        {
            e.Status = UpdateStatus.Continue;
        }

        private List<T> ReaderToList<T,TKey>(SqlDataReader reader) where T : BaseModel<TKey>
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

        private List<T> ReaderToList<T, TKey>(DataTable table) where T : BaseModel<TKey>
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
