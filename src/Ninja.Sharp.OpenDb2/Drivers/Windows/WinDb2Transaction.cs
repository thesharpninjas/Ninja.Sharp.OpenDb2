// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

using OpenDb2.Interfaces.Windows;
using System.Data;
using System.Data.OleDb;

namespace OpenDb2.Drivers.Windows
{
    /// <summary>
    /// Windows-specific DB2 transaction wrapper around <see cref="OleDbTransaction"/>.
    /// </summary>
    /// <param name="transaction">The underlying <see cref="OleDbTransaction"/>.</param>
    public class WinDb2Transaction(OleDbTransaction transaction) : IWinDb2Transaction
    {
        private readonly OleDbTransaction _transaction = transaction;
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
