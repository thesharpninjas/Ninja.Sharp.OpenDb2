using OpenDb2.Interfaces.Windows;
using System.Data;
using System.Data.OleDb;

namespace OpenDb2.Drivers.Windows
{
    public class WinDb2Transaction(OleDbTransaction transaction) : IWinDb2Transaction
    {
        private readonly OleDbTransaction _transaction = transaction;

        public IDbTransaction Transaction => _transaction;

        public void Commit() => _transaction.Commit();

        public void Rollback() => _transaction.Rollback();

        public void Dispose() => _transaction.Dispose();
    }
}
