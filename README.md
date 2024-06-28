# Ninja.Sharp.OpenDb2

An open source client for integration with IBM DB2

## Usage

On Windows:

``` csharp
builder.services
   .AddWinDb2Services(connectionString, configuration);
```

On Linux:

``` csharp
builder.services
   .AddLnxDb2Services(connectionString, configuration);
```