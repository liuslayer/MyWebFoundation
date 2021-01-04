using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebFoundation.DB
{
    public sealed class ScopeTransaction : IScopeTransaction, IDbTransaction
    {
        #region Fields

        private const string SoltName = "ScopeTransaction";

        internal IDbTransaction Raw { get; private set; }

        private static List<WeakReference<ScopeTransaction>> _scopes = new List<WeakReference<ScopeTransaction>>();

        internal static ScopeTransaction GetExistingTransaction(DbManager dbManager)
        {
            var refer = _scopes.FirstOrDefault(r =>
            {
                ScopeTransaction sc;
                return (r.TryGetTarget(out sc) && sc.Connection == dbManager.Connection);
            });
            if (refer == null)
            {
                return null;
            }
            ScopeTransaction result;
            refer.TryGetTarget(out result);

            return result;
        }

        private bool IsCompleted { get; set; }

        #endregion Fields

        #region Constructors / Destructors

        internal ScopeTransaction(IDbTransaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            this.Raw = transaction;
            _scopes.Add(new WeakReference<ScopeTransaction>(this));
        }

        public Action<IScopeTransaction> Disposed;

        ~ScopeTransaction()
        {
            this.Dispose();
        }

        #endregion Constructors / Destructors

        #region ITransactionScope members

        public void Complete()
        {
            this.IsCompleted = true;
        }

        #endregion ITransactionScope members

        #region IDbTransaction members

        public IDbConnection Connection
        {
            get { return this.Raw.Connection; }
        }

        public IsolationLevel IsolationLevel
        {
            get { return this.Raw.IsolationLevel; }
        }

        void IDbTransaction.Commit()
        {
            this.Raw.Commit();
        }

        void IDbTransaction.Rollback()
        {
            this.Raw.Rollback();
        }

        #endregion IDbTransaction members

        #region IDisposable members

        public void Dispose()
        {
            if (this.IsCompleted)
            {
                this.Raw.Commit();
            }
            else
            {
                this.Raw.Rollback();
            }
            var refer = _scopes.FirstOrDefault(r =>
            {
                ScopeTransaction sc;
                return (r.TryGetTarget(out sc) && sc == this);
            });
            _scopes.Remove(refer);
            this.Raw.Dispose();
            if (this.Disposed != null)
            {
                this.Disposed(this);
                this.Disposed = null;
            }
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable members
    }
}
