// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

using Ninja.Sharp.OpenDb2.Interfaces;
using System.Collections.Frozen;

namespace OpenDb2.Enums
{
    /// <summary>
    /// Maps platform-agnostic <see cref="Db2Type"/> values to the corresponding
    /// <see cref="WinDb2Type"/> or <see cref="LnxDb2Type"/> at runtime based on the current OS.
    /// </summary>
    public static class Db2TypeMapper
    {
        private static readonly FrozenDictionary<Db2Type, WinDb2Type> s_toWin = new Dictionary<Db2Type, WinDb2Type>
        {
            [Db2Type.SmallInt] = WinDb2Type.SmallInt,
            [Db2Type.Integer] = WinDb2Type.Integer,
            [Db2Type.BigInt] = WinDb2Type.BigInt,
            [Db2Type.Real] = WinDb2Type.Single,
            [Db2Type.Double] = WinDb2Type.Double,
            [Db2Type.Decimal] = WinDb2Type.Decimal,
            [Db2Type.Numeric] = WinDb2Type.Numeric,
            [Db2Type.Char] = WinDb2Type.Char,
            [Db2Type.VarChar] = WinDb2Type.VarChar,
            [Db2Type.LongVarChar] = WinDb2Type.LongVarChar,
            [Db2Type.Binary] = WinDb2Type.Binary,
            [Db2Type.VarBinary] = WinDb2Type.VarBinary,
            [Db2Type.LongVarBinary] = WinDb2Type.LongVarBinary,
            [Db2Type.Date] = WinDb2Type.DBDate,
            [Db2Type.Time] = WinDb2Type.DBTime,
            [Db2Type.Timestamp] = WinDb2Type.DBTimeStamp,
            [Db2Type.Clob] = WinDb2Type.LongVarWChar,
            [Db2Type.Blob] = WinDb2Type.LongVarBinary,
            [Db2Type.Xml] = WinDb2Type.LongVarWChar,
            [Db2Type.Boolean] = WinDb2Type.Boolean,
        }.ToFrozenDictionary();

        private static readonly FrozenDictionary<Db2Type, LnxDb2Type> s_toLnx = new Dictionary<Db2Type, LnxDb2Type>
        {
            [Db2Type.SmallInt] = LnxDb2Type.SmallInt,
            [Db2Type.Integer] = LnxDb2Type.Integer,
            [Db2Type.BigInt] = LnxDb2Type.BigInt,
            [Db2Type.Real] = LnxDb2Type.Real,
            [Db2Type.Double] = LnxDb2Type.Double,
            [Db2Type.Decimal] = LnxDb2Type.Decimal,
            [Db2Type.Numeric] = LnxDb2Type.Numeric,
            [Db2Type.Char] = LnxDb2Type.Char,
            [Db2Type.VarChar] = LnxDb2Type.VarChar,
            [Db2Type.LongVarChar] = LnxDb2Type.LongVarChar,
            [Db2Type.Binary] = LnxDb2Type.Binary,
            [Db2Type.VarBinary] = LnxDb2Type.VarBinary,
            [Db2Type.LongVarBinary] = LnxDb2Type.LongVarBinary,
            [Db2Type.Date] = LnxDb2Type.Date,
            [Db2Type.Time] = LnxDb2Type.Time,
            [Db2Type.Timestamp] = LnxDb2Type.Timestamp,
            [Db2Type.Clob] = LnxDb2Type.Clob,
            [Db2Type.Blob] = LnxDb2Type.Blob,
            [Db2Type.Xml] = LnxDb2Type.Xml,
            [Db2Type.Boolean] = LnxDb2Type.Boolean,
        }.ToFrozenDictionary();

        /// <summary>
        /// Converts a <see cref="Db2Type"/> to the corresponding <see cref="WinDb2Type"/>.
        /// </summary>
        /// <exception cref="NotSupportedException">Thrown when the type has no Windows mapping.</exception>
        public static WinDb2Type ToWindows(Db2Type type)
        {
            return s_toWin.TryGetValue(type, out var result)
                ? result
                : throw new NotSupportedException($"Db2Type '{type}' has no Windows (OleDb) mapping.");
        }

        /// <summary>
        /// Converts a <see cref="Db2Type"/> to the corresponding <see cref="LnxDb2Type"/>.
        /// </summary>
        /// <exception cref="NotSupportedException">Thrown when the type has no Linux mapping.</exception>
        public static LnxDb2Type ToLinux(Db2Type type)
        {
            return s_toLnx.TryGetValue(type, out var result)
                ? result
                : throw new NotSupportedException($"Db2Type '{type}' has no Linux (IBM.Data.Db2) mapping.");
        }
    }
}
