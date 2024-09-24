// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

using OpenDb2.Enums;
using System.Data;

namespace OpenDb2.Interfaces.Windows
{
    public interface IWinDb2Command : IDb2Command
    {
        void AddParam(string parameterName, WinDb2Type type, object value);
        void AddParam(string parameterName, WinDb2Type type, int size, object value);
        void AddParam(string parameterName, WinDb2Type type, int size, ParameterDirection direction);
    }
}
