// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

using OpenDb2.Enums;
using System.Data;
using System.Data.Common;

namespace OpenDb2.Interfaces
{
    /// <summary>
    /// Represents a command to be executed against a DB2 database.
    /// </summary>
    public interface IDb2Command : IDisposable
    {
        /// <summary>
        /// Adds a parameter to the command with the specified name and value.
        /// </summary>
        /// <param name="parameterName">The name of the parameter to add.</param>
        /// <param name="value">The value of the parameter to add.</param>
        void AddParam(string parameterName, object value);

        /// <summary>
        /// Adds a parameter using a platform-agnostic <see cref="Db2Type"/> that is automatically
        /// mapped to the correct platform-specific type at runtime.
        /// </summary>
        /// <param name="parameterName">The name of the parameter to add.</param>
        /// <param name="type">The platform-agnostic data type.</param>
        /// <param name="value">The value of the parameter.</param>
        void AddParam(string parameterName, Db2Type type, object value);

        /// <summary>
        /// Adds a parameter using a platform-agnostic <see cref="Db2Type"/> with a specified size.
        /// </summary>
        /// <param name="parameterName">The name of the parameter to add.</param>
        /// <param name="type">The platform-agnostic data type.</param>
        /// <param name="size">The size of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        void AddParam(string parameterName, Db2Type type, int size, object value);

        /// <summary>
        /// Adds a parameter using a platform-agnostic <see cref="Db2Type"/> with a specified size and direction.
        /// </summary>
        /// <param name="parameterName">The name of the parameter to add.</param>
        /// <param name="type">The platform-agnostic data type.</param>
        /// <param name="size">The size of the parameter.</param>
        /// <param name="direction">The direction of the parameter.</param>
        void AddParam(string parameterName, Db2Type type, int size, ParameterDirection direction);

        /// <summary>
        /// Retrieves the value of a parameter with the specified name.
        /// </summary>
        /// <param name="parameterName">The name of the parameter to retrieve.</param>
        /// <returns>The value of the specified parameter.</returns>
        object ReadParam(string parameterName);

        /// <summary>
        /// Executes the command against the database and returns the number of rows affected.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a result of the number of rows affected.</returns>
        Task<int> ExecuteNonQuery();

        /// <summary>
        /// Executes the command against the database and returns a data reader for reading the results.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a result of a <see cref="DbDataReader"/> object for reading the results.</returns>
        Task<DbDataReader> ExecuteReader();

        /// <summary>
        /// Creates a data adapter for use with the command.
        /// </summary>
        /// <returns>A <see cref="DbDataAdapter"/> object associated with the command.</returns>
        DbDataAdapter CreateDataAdapter();
    }
}
