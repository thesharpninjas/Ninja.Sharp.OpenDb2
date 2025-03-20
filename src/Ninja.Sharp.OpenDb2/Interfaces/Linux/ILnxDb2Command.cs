// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

using OpenDb2.Enums;
using System.Data;

namespace OpenDb2.Interfaces.Linux
{
    /// <summary>
    /// Represents a command interface for interacting with a DB2 database on Linux systems.
    /// </summary>
    public interface ILnxDb2Command : IDb2Command
    {
        /// <summary>
        /// Adds a parameter to the DB2 command with the specified name, type, and value.
        /// </summary>
        /// <param name="parameterName">The name of the parameter to add.</param>
        /// <param name="type">The data type of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        void AddParam(string parameterName, LnxDb2Type type, object value);

        /// <summary>
        /// Adds a parameter to the DB2 command with the specified name, type, size, and value.
        /// </summary>
        /// <param name="parameterName">The name of the parameter to add.</param>
        /// <param name="type">The data type of the parameter.</param>
        /// <param name="size">The size of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        void AddParam(string parameterName, LnxDb2Type type, int size, object value);

        /// <summary>
        /// Adds a parameter to the DB2 command with the specified name, type, size, and direction.
        /// </summary>
        /// <param name="parameterName">The name of the parameter to add.</param>
        /// <param name="type">The data type of the parameter.</param>
        /// <param name="size">The size of the parameter.</param>
        /// <param name="direction">The direction of the parameter (e.g., Input, Output).</param>
        void AddParam(string parameterName, LnxDb2Type type, int size, ParameterDirection direction);
    }
}