using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebFoundation.DB
{
    /// <summary>
    /// Database type, SQLServer\ Oracle \ PostgreSQL \ other's database
    /// </summary>
    public enum DatabaseType
    {
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// SQL Server
        /// </summary>
        [ProviderName("System.Data.SqlClient")]
        SqlServer,

        /// <summary>
        /// SQL Server Compact
        /// </summary>
        [ProviderName("System.Data.SqlServerCe.4.0")]
        SqlServerCe,

        /// <summary>
        /// Oracle (Managed Driver) for Oracle.DataAccess.Client
        /// </summary>
        [ProviderName("Oracle.DataAccess.Client")]
        Oracle,

        /// <summary>
        /// OfficialOracle (Managed Driver) use the official driver
        /// </summary>
        [ProviderName("Oracle.ManagedDataAccess.Client")]
        OfficialOracle,

        /// <summary>
        /// MySQL / Amazon Aurora / MariaDB
        /// </summary>
        [ProviderName("MySql.Data.MySqlClient")]
        MySql,

        /// <summary>
        /// PostgreSQL
        /// </summary>
        [ProviderName("Npgsql")]
        PostgreSql,

        /// <summary>
        /// SQLite
        /// </summary>
        [ProviderName("System.Data.SQLite")]
        SQLite,

        /// <summary>
        /// OLE Database
        /// </summary>
        [ProviderName("System.Data.OleDb")]
        OleDb,

        /// <summary>
        /// ODBC
        /// </summary>
        [ProviderName("System.Data.Odbc")]
        Odbc,
    }
}
