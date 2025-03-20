// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

using IBM.Data.Db2;
using OpenDb2.Interfaces.Linux;
using System.Data;

namespace OpenDb2.Drivers.Linux
{
    public class LnxDb2Transaction(DB2Transaction transaction) : ILnxDb2Transaction
    {
        private readonly DB2Transaction _transaction = transaction;

        public IDbTransaction Transaction => _transaction;

        /// <inheritdoc />
        public void Commit() => _transaction.Commit();

        /// <inheritdoc />
        public void Rollback() => _transaction.Rollback();

        /// <inheritdoc />
        public void Dispose() => _transaction.Dispose();
    }
}
