// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

using System.Data;

namespace OpenDb2.Interfaces.Windows
{
    public interface IWinDb2Connection : IDb2Connection
    {
        IWinDb2Transaction BeginTransaction();
        IWinDb2Command CreateCommand(string commandText, CommandType commandType);
        IWinDb2Command CreateCommand(string commandText, CommandType commandType, IDb2Transaction transaction);
    }
}
