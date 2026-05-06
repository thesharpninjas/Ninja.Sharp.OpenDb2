// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

using IBM.Data.Db2;
using OpenDb2.Interfaces.Linux;
using System.Data;

namespace OpenDb2.Drivers.Linux
{
    /// <summary>
    /// Linux and macOS DB2 transaction wrapper around <see cref="DB2Transaction"/>.
    /// </summary>
    /// <param name="transaction">The underlying <see cref="DB2Transaction"/>.</param>
    public class LnxDb2Transaction(DB2Transaction transaction) : ILnxDb2Transaction
    {
        private readonly DB2Transaction _transaction = transaction;
        private bool _disposed;

        /// <inheritdoc />
        public IDbTransaction Transaction => _transaction;

        /// <inheritdoc />
        public void Commit()
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            _transaction.Commit();
        }

        /// <inheritdoc />
        public void Rollback()
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            _transaction.Rollback();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            _transaction.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
