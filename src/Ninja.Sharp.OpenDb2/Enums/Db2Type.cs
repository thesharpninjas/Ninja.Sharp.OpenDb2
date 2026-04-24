// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

namespace OpenDb2.Enums
{
    /// <summary>
    /// Platform-agnostic DB2 data types. These are automatically mapped at runtime
    /// to the appropriate platform-specific type (<see cref="WinDb2Type"/> or <see cref="LnxDb2Type"/>).
    /// </summary>
    public enum Db2Type
    {
        SmallInt,
        Integer,
        BigInt,
        Real,
        Double,
        Decimal,
        Numeric,
        Char,
        VarChar,
        LongVarChar,
        Binary,
        VarBinary,
        LongVarBinary,
        Date,
        Time,
        Timestamp,
        Clob,
        Blob,
        Xml,
        Boolean
    }
}
