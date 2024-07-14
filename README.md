# Ninja.Sharp.OpenDb2

This .NET library provides an abstraction layer over DB2 database access methods, facilitating dependency injection and enabling easier mocking and testing of database interactions.

## Packages
--------
 
| Package | NuGet Stable | 
| ------- | ------------ | 
| [OpenDb2](https://www.nuget.org/packages/Ninja.Sharp.OpenDb2/) | [![OpenDb2](https://img.shields.io/badge/nuget-1.1.0-blue)](https://www.nuget.org/packages/OpenDb2/)

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
