using System;
using System.Configuration;
using System.Data;
using Dapper;
using MyWebFoundation.Framework.Configs;
using MyWebFoundation.Framework.Extensions;

namespace MyWebFoundation.DB.Dapper
{
    public class DbManager : IDisposable
    {
        private static bool _dapperInit = false;

        private bool _disposed;
        public static int Timeout { get; set; }

        public DatabaseType DBType { get; private set; }

        public string ConnectionStr { get; private set; }

        private IDbConnection _conn { get; set; }

        private static object _sync = new object();

        /// <summary>
        /// Return open connection
        /// </summary>
        public IDbConnection Connection
        {
            get
            {
                return _conn;
            }
        }

        public static DbManager FromConfig(string connectionName = "DefaultConnection")
        {
            Configuration config = null;
            if (System.Web.HttpContext.Current != null)
            {
                config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            }
            else
            {
                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            }
            string connString = config.ConnectionStrings.ConnectionStrings[connectionName].ConnectionString;

            var dbtypestr = config.ReadAppSettingsValue("DatabaseType", "SqlServer");
            DatabaseType dbtype;
            if (!Enum.TryParse<DatabaseType>(dbtypestr, out dbtype))
            {
                dbtype = DatabaseType.SqlServer;
            }

            InitializeDapper(config, dbtype);

            return new DbManager(connString, dbtype);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
        private static void InitializeDapper(Configuration config, DatabaseType dbtype)
        {
            if (_dapperInit)
            {
                return;
            }
            Timeout = config.ReadAppSettingsValue("DBTimeOut", "30").ToInt();

            switch (dbtype)
            {
                case DatabaseType.Unknown:
                    throw new NotSupportedException("DatabaseType.Unknown");

                case DatabaseType.SqlServer:
                    SimpleCRUD.SetDialect(SimpleCRUD.Dialect.SQLServer);
                    break;

                case DatabaseType.OfficialOracle:

                case DatabaseType.Oracle:
                    throw new NotSupportedException("Oracle");

                case DatabaseType.MySql:
                    SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
                    break;

                case DatabaseType.PostgreSql:
                    SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);
                    break;

                case DatabaseType.SQLite:
                    SimpleCRUD.SetDialect(SimpleCRUD.Dialect.SQLite);
                    break;

                case DatabaseType.OleDb:
                    throw new NotSupportedException("DatabaseType.OleDb");

                case DatabaseType.Odbc:
                    throw new NotSupportedException("DatabaseType.Odbc");

                default:
                    throw new NotSupportedException("DatabaseType.Unknown");
            }

            _dapperInit = true;
        }

        /// <summary>
        /// Create a new Sql database connection
        /// </summary>
        /// <param name="connString">The name of the connection string</param>
        /// <param name="databaseType">specify the database type, default is SQLServer</param>
        public DbManager(string connString, DatabaseType databaseType = DatabaseType.SqlServer)
        {
            this.DBType = databaseType;
            // Use first?
            if (connString == string.Empty)
            {
                connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            }
            this.ConnectionStr = connString;
            _conn = DbProvider.CreateConnection(databaseType, connString);
        }

        public static void ReConnectionDB(ref DbManager dbManager)
        {
            var connString = dbManager.ConnectionStr;
            var databaseType = dbManager.DBType;
            try
            {
                if (dbManager != null)
                {
                    dbManager.Dispose();
                }
                dbManager = new DbManager(connString, databaseType);
            }
            catch (Exception ex)
            {
                typeof(DbManager).Error(System.Reflection.MethodBase.GetCurrentMethod().Name + ":Failed to connect database! ", ex);
                throw;
            }
        }

        ~DbManager()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this._disposed)
            {
                if (disposing)
                {
                    this._conn.Close();
                    this._conn = null;
                }
                _disposed = true;
            }
        }
    }
}
