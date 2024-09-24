// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

using System.Data;

namespace OpenDb2.Interfaces.Linux
{
    public interface ILnxDb2Connection : IDb2Connection
    {
        ILnxDb2Transaction BeginTransaction();
        ILnxDb2Command CreateCommand(string commandText, CommandType commandType);
        ILnxDb2Command CreateCommand(string commandText, CommandType commandType, IDb2Transaction transaction);
    }
}
