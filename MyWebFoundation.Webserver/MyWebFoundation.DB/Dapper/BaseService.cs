using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using Npgsql;

namespace MyWebFoundation.DB.Dapper
{
    public abstract class BaseService : IBaseService
    {
        protected static int Timeout = DbManager.Timeout;
        private string _connectionStrings = string.Empty;

        protected IDbConnection GetSqlConnection()
        {
            IDbConnection conn = new SqlConnection(_connectionStrings);
            conn.Open();
            return conn;

        }

        public IDbTransaction Transaction
        {
            get
            {
                var scop = ScopeTransaction.GetExistingTransaction(DBManager);
                if (scop == null)
                {
                    return null;
                }
                else
                {
                    return scop.Raw;
                }
            }
        }

        /// <summary>
        /// if return null, mean it already has transaction outer.
        /// </summary>
        /// <returns>ScopeTransaction</returns>
        public virtual IScopeTransaction CreateScopeTransaction()
        {
            var tran = this.Transaction;
            if (tran != null)
            {
                return null;
            }
            else
            {
                var scopTran = (ScopeTransaction)DBManager.Connection.StartTransaction();

                return scopTran;
            }
        }

        public DbManager DBManager { get; private set; }

        public BaseService(DbManager dbManager)
        {
            this.DBManager = dbManager;
        }

        public T Get<T>(int id) where T : class
        {
            return DBManager.Connection.Get<T>(id, Transaction, Timeout);
        }

        public List<T> GetAllList<T>() where T : class
        {
            return DBManager.Connection.GetList<T>(null, Transaction, Timeout).ToList();

        }

        public List<T> GetListWithCondition<T>(object whereConditions) where T : class
        {
            return DBManager.Connection.GetList<T>(whereConditions).ToList();
        }

        public List<T> GetListWithCondition<T>(string conditions) where T : class
        {
            return DBManager.Connection.GetList<T>(conditions).ToList();
        }

        public List<T> GetListPaged<T>(int pageNumber, int rowsPerPage, string conditions, string orderby) where T : class
        {
            return DBManager.Connection.GetListPaged<T>(pageNumber, rowsPerPage, conditions, orderby).ToList();
        }

        public int? Insert<T>(T entityToInsert) where T : class
        {
            using (var tran = CreateScopeTransaction())
            {
                var newId = DBManager.Connection.Insert<T>(entityToInsert, Transaction, Timeout);
                if (tran != null)
                {
                    tran.Complete();
                }
                return newId;
            }
        }

        public void Insert<T>(IEnumerable<T> entitiesToInsert) where T : class
        {
            using (var tran = CreateScopeTransaction())
            {
                foreach (var entityToInsert in entitiesToInsert)
                {
                    DBManager.Connection.Insert<T>(entityToInsert, Transaction, Timeout);
                }
                if (tran != null)
                {
                    tran.Complete();
                }
            }
        }

        public int Update<T>(T entityToUpdate) where T : class
        {
            return DBManager.Connection.Update<T>(entityToUpdate, Transaction, Timeout);
        }

        public void Update<T>(IEnumerable<T> entitiesToUpdate) where T : class
        {
            using (var tran = CreateScopeTransaction())
            {
                foreach (var entityToUpdate in entitiesToUpdate)
                {
                    DBManager.Connection.Update<T>(entityToUpdate, Transaction, Timeout);
                }            
                if (tran != null)
                {
                    tran.Complete();
                }
            }
        }

        public int Delete<T>(int Id) where T : class
        {
            return DBManager.Connection.Delete<T>(Id);
        }

        public int Delete<T>(T entityToDel) where T : class
        {
            return DBManager.Connection.Delete<T>(entityToDel);
        }

        public int DeleteList<T>(object whereConditions) where T : class
        {
            using (var tran = CreateScopeTransaction())
            {
                var count = DBManager.Connection.DeleteList<T>(whereConditions);
                if (tran != null)
                {
                    tran.Complete();
                }
                return count;
            }
        }

        public int DeleteList<T>(string conditions) where T : class
        {
            using (var tran = CreateScopeTransaction())
            {
                var count = DBManager.Connection.DeleteList<T>(conditions);
                if (tran != null)
                {
                    tran.Complete();
                }
                return count;
            }
        }

        public int RecordCount<T>(object whereConditions) where T : class
        {
            return DBManager.Connection.RecordCount<T>(whereConditions);
        }

        public int RecordCount<T>(string conditions = "") where T : class
        {
            return DBManager.Connection.RecordCount<T>(conditions);
        }

        public Guid GetGuid()
        {
            return SimpleCRUD.SequentialGuid();
        }

        public virtual void Dispose()
        {
            if (this.DBManager != null)
            {
                this.DBManager.Dispose();
            }
        }
    }
}
