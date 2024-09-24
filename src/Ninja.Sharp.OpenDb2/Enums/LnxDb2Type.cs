// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

namespace OpenDb2.Enums
{
    public enum LnxDb2Type
    {
        Invalid = 0,
        SmallInt = 1,
        Integer = 2,
        BigInt = 3,
        Real = 4,
        Double = 5,
        Float = 6,
        Decimal = 7,
        Numeric = 8,
        Date = 9,
        Time = 10,
        Timestamp = 11,
        Char = 12,
        VarChar = 13,
        LongVarChar = 14,
        Binary = 15,
        VarBinary = 16,
        LongVarBinary = 17,
        Graphic = 18,
        VarGraphic = 19,
        LongVarGraphic = 20,
        Clob = 21,
        Blob = 22,
        DbClob = 23,
        Datalink = 24,
        RowId = 25,
        Xml = 26,
        Real370 = 27,
        DecimalFloat = 28,
        DynArray = 29,
        BigSerial = 30,
        BinaryXml = 31,
        TimeStampWithTimeZone = 32,
        Cursor = 33,
        Serial = 34,
        Int8 = 35,
        Serial8 = 36,
        Money = 37,
        DateTime = 38,
        Text = 39,
        Byte = 40,
        [Obsolete("Current incompatabality between the IBM Data Server Provider for .NET and the Client SDK IBM Informix .NET Provider")]
        Char1 = 1001,
        SmallFloat = 1002,
        Null = 1003,
        [Obsolete("Current incompatabality between the IBM Data Server Provider for .NET and the Client SDK IBM Informix .NET Provider")]
        IntervalYearMonth = 1004,
        [Obsolete("Current incompatabality between the IBM Data Server Provider for .NET and the Client SDK IBM Informix .NET Provider")]
        IntervalDayFraction = 1005,
        NChar = 1006,
        NVarChar = 1007,
        [Obsolete("Current incompatabality between the IBM Data Server Provider for .NET and the Client SDK IBM Informix .NET Provider")]
        Set = 1008,
        [Obsolete("Current incompatabality between the IBM Data Server Provider for .NET and the Client SDK IBM Informix .NET Provider")]
        MultiSet = 1009,
        [Obsolete("Current incompatabality between the IBM Data Server Provider for .NET and the Client SDK IBM Informix .NET Provider")]
        List = 1010,
        [Obsolete("Current incompatabality between the IBM Data Server Provider for .NET and the Client SDK IBM Informix .NET Provider")]
        Row = 1011,
        [Obsolete("Current incompatabality between the IBM Data Server Provider for .NET and the Client SDK IBM Informix .NET Provider")]
        SQLUDTVar = 1012,
        [Obsolete("Current incompatabality between the IBM Data Server Provider for .NET and the Client SDK IBM Informix .NET Provider")]
        SQLUDTFixed = 1013,
        [Obsolete("Current incompatabality between the IBM Data Server Provider for .NET and the Client SDK IBM Informix .NET Provider")]
        SmartLobLocator = 1014,
        Boolean = 1015,
        Other = 1016
    }
}
