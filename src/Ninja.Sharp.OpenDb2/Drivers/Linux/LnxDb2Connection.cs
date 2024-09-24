// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

using IBM.Data.Db2;
using OpenDb2.Interfaces;
using OpenDb2.Interfaces.Linux;
using System.Data;

namespace OpenDb2.Drivers.Linux
{
    public class LnxDb2Connection(string connectionString) : ILnxDb2Connection
    {
        private readonly DB2Connection _connection = new(connectionString);

        public async Task Open() => await _connection.OpenAsync();

        public async Task Close() => await _connection.CloseAsync();

        public ILnxDb2Transaction BeginTransaction() => new LnxDb2Transaction(_connection.BeginTransaction());

        public ILnxDb2Command CreateCommand(string commandText, CommandType commandType)
        {
            var command = _connection.CreateCommand();

            command.CommandText = commandText;
            command.CommandType = commandType;

            return new LnxDb2Command(command);
        }

        public ILnxDb2Command CreateCommand(string commandText, CommandType commandType, IDb2Transaction transaction)
        {
            var command = _connection.CreateCommand();

            command.CommandText = commandText;
            command.CommandType = commandType;
            command.Transaction = (DB2Transaction)transaction.Transaction;

            return new LnxDb2Command(command);
        }

        public void Dispose() => _connection.Dispose();
    }
}
