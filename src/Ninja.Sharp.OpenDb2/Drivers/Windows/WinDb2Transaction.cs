// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

using OpenDb2.Interfaces.Windows;
using System.Data;
using System.Data.OleDb;

namespace OpenDb2.Drivers.Windows
{
    public class WinDb2Transaction(OleDbTransaction transaction) : IWinDb2Transaction
    {
        private readonly OleDbTransaction _transaction = transaction;

        public IDbTransaction Transaction => _transaction;

        /// <inheritdoc />
        public void Commit() => _transaction.Commit();

        /// <inheritdoc />
        public void Rollback() => _transaction.Rollback();

        /// <inheritdoc />
        public void Dispose() => _transaction.Dispose();
    }
}
