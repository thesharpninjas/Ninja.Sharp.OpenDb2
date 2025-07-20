[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Nuget](https://img.shields.io/nuget/v/Ninja.Sharp.OpenDb2?style=plastic)](https://www.nuget.org/packages/Ninja.Sharp.OpenDb2)
![NuGet Downloads](https://img.shields.io/nuget/dt/Ninja.Sharp.OpenDb2)
[![issues - Ninja.Sharp.OpenDb2](https://img.shields.io/github/issues/thesharpninjas/Ninja.Sharp.OpenDb2)](https://github.com/thesharpninjas/Ninja.Sharp.OpenDb2/issues)
[![stars - Ninja.Sharp.OpenDb2](https://img.shields.io/github/stars/thesharpninjas/Ninja.Sharp.OpenDb2?style=social)](https://github.com/thesharpninjas/Ninja.Sharp.OpenDb2)

# Ninja.Sharp.OpenDb2

This .NET library provides an abstraction layer over DB2 database access methods, facilitating dependency injection and enabling easier mocking and testing of database interactions.

## Generic Service registration

You can register the OpenDB2 service simply with dependency injection:

``` csharp
builder.services
   .AddDb2Services(connectionString, configuration);
```

## Usage - On Windows

Windows Service registration:

``` csharp
builder.services
   .AddWinDb2Services(connectionString, configuration);
```

Calling stored procedure:

``` csharp
public class Db2Repository(IWinDb2Connection connection)
{
    public async Task DoSomething(string param1, int param2)
    {
        using (connection)
        {
            await connection.Open();

            // Creating command

            using var cmd = connection.CreateCommand("STORED_PROCEDURE_NAME", CommandType.StoredProcedure);

            // Creating parameters

            cmd.AddParam("@PARAM1", WinDb2Type.VarChar, 10, param1);
            cmd.AddParam("@PARAM2", WinDb2Type.Decimal, 6, param2);
            cmd.AddParam("@OUTPARAM", WinDb2Type.VarChar, 250, ParameterDirection.Output);

            // Reading output parameter

            await cmd.ExecuteNonQuery();

            string? outputResult = cmd.ReadParam("@OUTPARAM") as string;

            // Data retrieval from Reader

            using var reader = await cmd.ExecuteReader();

            while (await reader.ReadAsync())
            {
                var field1 = reader.GetValue("FIELD1").ToString();
                var field2 = reader.GetValue("FIELD2").ToString();
            }
        }
    }
}
```

Executing query with transaction and DataAdapter

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
                // Creating command

                using var cmd = connection.CreateCommand("QUERY", CommandType.Text, transaction);

                // Creating data adapter

                using var adapter = cmd.CreateDataAdapter();

                // Filling and reading DataTable

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

## Usage - On Linux

Linux Service registration:

``` csharp
builder.services
   .AddLnxDb2Services(connectionString, configuration);
```

Calling stored procedure:

``` csharp
public class Db2Repository(ILnxDb2Connection connection)
{
    public async Task DoSomething(string param1, int param2)
    {
        using (connection)
        {
            await connection.Open();

            // Creating command

            using var cmd = connection.CreateCommand("STORED_PROCEDURE_NAME", CommandType.StoredProcedure);

            // Creating parameters

            cmd.AddParam("@PARAM1", LnxDb2Type.NVarChar, 10, param1);
            cmd.AddParam("@PARAM2", LnxDb2Type.Decimal, 6, param2);
            cmd.AddParam("@OUTPARAM", LnxDb2Type.VarChar, 250, ParameterDirection.Output);

            // Reading output parameter

            await cmd.ExecuteNonQuery();

            string? outputResult = cmd.ReadParam("@OUTPARAM") as string;

            // Data retrieval from Reader

            using var reader = await cmd.ExecuteReader();

            while (await reader.ReadAsync())
            {
                var field1 = reader.GetValue("FIELD1").ToString();
                var field2 = reader.GetValue("FIELD2").ToString();
            }
        }
    }
}
```

Executing query with transaction and DataAdapter

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
                // Creating command

                using var cmd = connection.CreateCommand("QUERY", CommandType.Text, transaction);

                // Creating data adapter

                using var adapter = cmd.CreateDataAdapter();

                // Filling and reading DataTable

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

## Licensee
Repository source code is available under MIT License, see license in the source.

## Contributing
Thank you for considering to help out with the source code!
If you'd like to contribute, please fork, fix, commit and send a pull request for the maintainers to review and merge into the main code base.

**Getting started with Git and GitHub**

 * [Setting up Git](https://docs.github.com/en/get-started/getting-started-with-git/set-up-git)
 * [Fork the repository](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/working-with-forks/fork-a-repo)
 * [Open an issue](https://github.com/thesharpninjas/Ninja.Sharp.OpenDb2/issues) if you encounter a bug or have a suggestion for improvements/features
