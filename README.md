[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Nuget](https://img.shields.io/nuget/v/Ninja.Sharp.OpenDb2?style=plastic)](https://www.nuget.org/packages/Ninja.Sharp.OpenDb2)
![NuGet Downloads](https://img.shields.io/nuget/dt/Ninja.Sharp.OpenDb2)
[![issues - Ninja.Sharp.OpenDb2](https://img.shields.io/github/issues/thesharpninjas/Ninja.Sharp.OpenDb2)](https://github.com/thesharpninjas/Ninja.Sharp.OpenDb2/issues)
[![stars - Ninja.Sharp.OpenDb2](https://img.shields.io/github/stars/thesharpninjas/Ninja.Sharp.OpenDb2?style=social)](https://github.com/thesharpninjas/Ninja.Sharp.OpenDb2)

# Ninja.Sharp.OpenDb2

A lightweight .NET library that provides a clean abstraction layer over IBM DB2 database connectivity.
It wraps the underlying ADO.NET drivers behind testable interfaces, making it straightforward to use dependency injection, write unit tests with mocked connections, and deploy the same codebase across Windows, Linux, and macOS.

The library targets .NET 8, .NET 9, and .NET 10.

## Installation

Install the package from NuGet:

```
dotnet add package Ninja.Sharp.OpenDb2
```

Or via the Package Manager Console in Visual Studio:

```
Install-Package Ninja.Sharp.OpenDb2
```

## How It Works

On Windows the library uses `System.Data.OleDb` to communicate with DB2.
On Linux and macOS it relies on the official IBM.Data.Db2 driver instead.

Both paths expose the same programming model through platform-specific interfaces (`IWinDb2Connection` / `ILnxDb2Connection`) that share a common base (`IDb2Connection`).
The connection is registered as a **scoped** service, so each DI scope (typically one per HTTP request) gets its own connection instance.

## Service Registration

### Automatic platform detection

The `AddDb2Services` method inspects the runtime OS at startup and registers the correct connection type automatically.
This is the recommended approach for applications that need to run on more than one platform:

``` csharp
builder.Services
   .AddDb2Services(connectionString, configuration);
```

On Windows this registers `IWinDb2Connection`; on Linux and macOS it registers `ILnxDb2Connection`.
If the platform is not recognized, a `NotSupportedException` is thrown at startup so you get an immediate, clear failure.

### Explicit platform registration

When the target platform is known at build time you can register the connection directly.

On Windows:

``` csharp
builder.Services
   .AddWinDb2Services(connectionString, configuration);
```

On Linux or macOS:

``` csharp
builder.Services
   .AddLnxDb2Services(connectionString, configuration);
```

All registration methods validate their arguments. Passing a `null` service collection throws `ArgumentNullException`; passing a `null`, empty, or whitespace connection string throws `ArgumentException`.

## Usage on Windows

Inject `IWinDb2Connection` into your repository or service class through constructor injection.
The connection must be opened explicitly before use and should be disposed when it is no longer needed.

### Stored procedure with parameters

``` csharp
public class Db2Repository(IWinDb2Connection connection)
{
    public async Task DoSomething(string param1, int param2)
    {
        using (connection)
        {
            await connection.Open();

            using var cmd = connection.CreateCommand("STORED_PROCEDURE_NAME", CommandType.StoredProcedure);

            cmd.AddParam("@PARAM1", WinDb2Type.VarChar, 10, param1);
            cmd.AddParam("@PARAM2", WinDb2Type.Decimal, 6, param2);
            cmd.AddParam("@OUTPARAM", WinDb2Type.VarChar, 250, ParameterDirection.Output);

            await cmd.ExecuteNonQuery();

            string? outputResult = cmd.ReadParam("@OUTPARAM") as string;
        }
    }
}
```

### Reading rows with ExecuteReader

``` csharp
public class Db2Repository(IWinDb2Connection connection)
{
    public async Task<List<string>> GetValues()
    {
        using (connection)
        {
            await connection.Open();

            using var cmd = connection.CreateCommand("SELECT FIELD1 FROM MY_TABLE", CommandType.Text);

            using var reader = await cmd.ExecuteReader();

            var results = new List<string>();

            while (await reader.ReadAsync())
            {
                results.Add(reader.GetValue("FIELD1").ToString()!);
            }

            return results;
        }
    }
}
```

### Query with transaction and DataAdapter

Transactions are started through `BeginTransaction` and must be explicitly committed or rolled back.
The transaction object is passed to `CreateCommand` so that the command participates in the same unit of work.

``` csharp
public class Db2Repository(IWinDb2Connection connection)
{
    public async Task DoSomething()
    {
        using (connection)
        {
            await connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                using var cmd = connection.CreateCommand("QUERY", CommandType.Text, transaction);

                using var adapter = cmd.CreateDataAdapter();

                DataTable dt = new();

                adapter.Fill(dt);

                var output = dt
                    .AsEnumerable()
                    .FirstOrDefault(row => row.Field<string>("FIELD1") == "VALUE");

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }
        }
    }
}
```

## Usage on Linux

The Linux API mirrors the Windows one. Inject `ILnxDb2Connection` instead and use `LnxDb2Type` for parameter types.

### Stored procedure with parameters

``` csharp
public class Db2Repository(ILnxDb2Connection connection)
{
    public async Task DoSomething(string param1, int param2)
    {
        using (connection)
        {
            await connection.Open();

            using var cmd = connection.CreateCommand("STORED_PROCEDURE_NAME", CommandType.StoredProcedure);

            cmd.AddParam("@PARAM1", LnxDb2Type.NVarChar, 10, param1);
            cmd.AddParam("@PARAM2", LnxDb2Type.Decimal, 6, param2);
            cmd.AddParam("@OUTPARAM", LnxDb2Type.VarChar, 250, ParameterDirection.Output);

            await cmd.ExecuteNonQuery();

            string? outputResult = cmd.ReadParam("@OUTPARAM") as string;
        }
    }
}
```

### Reading rows with ExecuteReader

``` csharp
public class Db2Repository(ILnxDb2Connection connection)
{
    public async Task<List<string>> GetValues()
    {
        using (connection)
        {
            await connection.Open();

            using var cmd = connection.CreateCommand("SELECT FIELD1 FROM MY_TABLE", CommandType.Text);

            using var reader = await cmd.ExecuteReader();

            var results = new List<string>();

            while (await reader.ReadAsync())
            {
                results.Add(reader.GetValue("FIELD1").ToString()!);
            }

            return results;
        }
    }
}
```

### Query with transaction and DataAdapter

``` csharp
public class Db2Repository(ILnxDb2Connection connection)
{
    public async Task DoSomething()
    {
        using (connection)
        {
            await connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                using var cmd = connection.CreateCommand("QUERY", CommandType.Text, transaction);

                using var adapter = cmd.CreateDataAdapter();

                DataTable dt = new();

                adapter.Fill(dt);

                var output = dt
                    .AsEnumerable()
                    .FirstOrDefault(row => row.Field<string>("FIELD1") == "VALUE");

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }
        }
    }
}
```

## Supported Platforms

| Operating System | Driver | Interface | Type Enum |
|---|---|---|---|
| Windows | System.Data.OleDb | `IWinDb2Connection` | `WinDb2Type` |
| Linux | IBM.Data.Db2 | `ILnxDb2Connection` | `LnxDb2Type` |
| macOS | IBM.Data.Db2 | `ILnxDb2Connection` | `LnxDb2Type` |

## Architecture Overview

The library is organized around three core abstractions, each with a Windows and a Linux implementation:

| Abstraction | Windows | Linux / macOS |
|---|---|---|
| `IDb2Connection` | `WinDb2Connection` (OleDb) | `LnxDb2Connection` (IBM.Data.Db2) |
| `IDb2Command` | `WinDb2Command` | `LnxDb2Command` |
| `IDb2Transaction` | `WinDb2Transaction` | `LnxDb2Transaction` |

Service registration is handled by the `ServiceRegistration` static class, which exposes the `AddDb2Services`, `AddWinDb2Services`, and `AddLnxDb2Services` extension methods on `IServiceCollection`.

## License

Repository source code is available under the MIT License. See [LICENSE.txt](LICENSE.txt) for details.

## Contributing

Thank you for considering to help out with the source code!
If you'd like to contribute, please fork, fix, commit and send a pull request for the maintainers to review and merge into the main code base.

**Getting started with Git and GitHub**

 * [Setting up Git](https://docs.github.com/en/get-started/getting-started-with-git/set-up-git)
 * [Fork the repository](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/working-with-forks/fork-a-repo)
 * [Open an issue](https://github.com/thesharpninjas/Ninja.Sharp.OpenDb2/issues) if you encounter a bug or have a suggestion for improvements/features
