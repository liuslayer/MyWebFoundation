using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using System.Linq;
using Common;
using System.Data.SqlClient;

namespace MyWebFoundation.DB.Dapper
{
    public abstract class BaseService : IBaseService
    {
        private string _connectionStrings = string.Empty;

        protected IDbConnection GetSqlConnection()
        {
            IDbConnection conn = new SqlConnection(_connectionStrings);
            conn.Open();
            return conn;

        }
        public BaseService(string connectionStrings)
        {
            this._connectionStrings = connectionStrings;
        }

        public T Get<T>(int id) where T : class
        {
            using (IDbConnection conn = GetSqlConnection())
            {
                return conn.Get<T>(id);
            }
        }

        public List<T> GetAllList<T>() where T : class
        {
            using (IDbConnection conn = GetSqlConnection())
            {
                return conn.GetList<T>().ToList();
            }
        }

        public List<T> GetListWithCondition<T>(object whereConditions) where T : class
        {
            using (IDbConnection conn = GetSqlConnection())
            {
                return conn.GetList<T>(whereConditions).ToList();
            }
        }

        public List<T> GetListWithCondition<T>(string conditions) where T : class
        {
            using (IDbConnection conn = GetSqlConnection())
            {
                return conn.GetList<T>(conditions).ToList();
            }
        }

        public List<T> GetListPaged<T>(int pageNumber, int rowsPerPage, string conditions, string orderby) where T : class
        {
            using (IDbConnection conn = GetSqlConnection())
            {
                return conn.GetListPaged<T>(pageNumber, rowsPerPage, conditions, orderby).ToList();
            }
        }

        public int? Insert<T>(T entityToInsert) where T : class
        {
            using (IDbConnection conn = GetSqlConnection())
            {
                return conn.Insert<T>(entityToInsert);
            }
        }

        public int Update<T>(T entityToUpdate) where T : class
        {
            using (IDbConnection conn = GetSqlConnection())
            {
                return conn.Update<T>(entityToUpdate);
            }
        }

        public int Delete<T>(int Id) where T : class
        {
            using (IDbConnection conn = GetSqlConnection())
            {
                return conn.Delete<T>(Id);
            }
        }

        public int Delete<T>(T entityToDel) where T : class
        {
            using (IDbConnection conn = GetSqlConnection())
            {
                return conn.Delete<T>(entityToDel);
            }
        }

        public int DeleteList<T>(object whereConditions) where T : class
        {
            using (IDbConnection conn = GetSqlConnection())
            {
                return conn.DeleteList<T>(whereConditions);
            }
        }

        public int DeleteList<T>(string conditions) where T : class
        {
            using (IDbConnection conn = GetSqlConnection())
            {
                return conn.DeleteList<T>(conditions);
            }
        }

        public int RecordCount<T>(object whereConditions) where T : class
        {
            using (IDbConnection conn = GetSqlConnection())
            {
                return conn.RecordCount<T>(whereConditions);
            }
        }

        public int RecordCount<T>(string conditions = "") where T : class
        {
            using (IDbConnection conn = GetSqlConnection())
            {
                return conn.RecordCount<T>(conditions);
            }
        }

        public Guid GetGuid()
        {
            return SimpleCRUD.SequentialGuid();
        }
    }
}
