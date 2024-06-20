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
