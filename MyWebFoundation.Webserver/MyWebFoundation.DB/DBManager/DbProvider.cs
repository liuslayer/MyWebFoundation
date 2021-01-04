using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebFoundation.DB
{
    internal static class DbProvider
    {
        public static DbProviderFactory GetFactory(DatabaseType dbKind)
        {
            var name = dbKind.GetProviderName();
            return DbProviderFactories.GetFactory(name);
        }

        public static IDbConnection CreateConnection(DatabaseType dbKind, string connectionString)
        {
            if (connectionString == null)
                throw new ArgumentNullException("connectionString");
            var connection = GetFactory(dbKind).CreateConnection();
            connection.ConnectionString = connectionString;
            return connection;
        }

        public static ScopeTransaction StartTransaction(this IDbConnection connection)
        {
            return connection.StartTransactionCore(null);
        }

        public static ScopeTransaction StartTransaction(this IDbConnection connection, IsolationLevel isolationLevel)
        {
            return connection.StartTransactionCore(isolationLevel);
        }

        private static ScopeTransaction StartTransactionCore(this IDbConnection connection, IsolationLevel? isolationLevel)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            var transaction = isolationLevel.HasValue
                            ? connection.BeginTransaction(isolationLevel.Value)
                            : connection.BeginTransaction();
            return transaction.Wrap();
        }

        public static ScopeTransaction Wrap(this IDbTransaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            return new ScopeTransaction(transaction);
        }
    }
}
