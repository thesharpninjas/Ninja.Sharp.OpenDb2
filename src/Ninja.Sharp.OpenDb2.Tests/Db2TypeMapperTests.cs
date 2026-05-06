// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

using OpenDb2.Enums;

namespace Ninja.Sharp.OpenDb2.Tests
{
    public class Db2TypeMapperTests
    {
        [Theory]
        [InlineData(Db2Type.SmallInt, WinDb2Type.SmallInt)]
        [InlineData(Db2Type.Integer, WinDb2Type.Integer)]
        [InlineData(Db2Type.BigInt, WinDb2Type.BigInt)]
        [InlineData(Db2Type.Real, WinDb2Type.Single)]
        [InlineData(Db2Type.Double, WinDb2Type.Double)]
        [InlineData(Db2Type.Decimal, WinDb2Type.Decimal)]
        [InlineData(Db2Type.Numeric, WinDb2Type.Numeric)]
        [InlineData(Db2Type.Char, WinDb2Type.Char)]
        [InlineData(Db2Type.VarChar, WinDb2Type.VarChar)]
        [InlineData(Db2Type.LongVarChar, WinDb2Type.LongVarChar)]
        [InlineData(Db2Type.Binary, WinDb2Type.Binary)]
        [InlineData(Db2Type.VarBinary, WinDb2Type.VarBinary)]
        [InlineData(Db2Type.LongVarBinary, WinDb2Type.LongVarBinary)]
        [InlineData(Db2Type.Date, WinDb2Type.DBDate)]
        [InlineData(Db2Type.Time, WinDb2Type.DBTime)]
        [InlineData(Db2Type.Timestamp, WinDb2Type.DBTimeStamp)]
        [InlineData(Db2Type.Clob, WinDb2Type.LongVarWChar)]
        [InlineData(Db2Type.Blob, WinDb2Type.LongVarBinary)]
        [InlineData(Db2Type.Xml, WinDb2Type.LongVarWChar)]
        [InlineData(Db2Type.Boolean, WinDb2Type.Boolean)]
        public void ToWindows_Should_Map_Correctly(Db2Type input, WinDb2Type expected)
        {
            var result = Db2TypeMapper.ToWindows(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(Db2Type.SmallInt, LnxDb2Type.SmallInt)]
        [InlineData(Db2Type.Integer, LnxDb2Type.Integer)]
        [InlineData(Db2Type.BigInt, LnxDb2Type.BigInt)]
        [InlineData(Db2Type.Real, LnxDb2Type.Real)]
        [InlineData(Db2Type.Double, LnxDb2Type.Double)]
        [InlineData(Db2Type.Decimal, LnxDb2Type.Decimal)]
        [InlineData(Db2Type.Numeric, LnxDb2Type.Numeric)]
        [InlineData(Db2Type.Char, LnxDb2Type.Char)]
        [InlineData(Db2Type.VarChar, LnxDb2Type.VarChar)]
        [InlineData(Db2Type.LongVarChar, LnxDb2Type.LongVarChar)]
        [InlineData(Db2Type.Binary, LnxDb2Type.Binary)]
        [InlineData(Db2Type.VarBinary, LnxDb2Type.VarBinary)]
        [InlineData(Db2Type.LongVarBinary, LnxDb2Type.LongVarBinary)]
        [InlineData(Db2Type.Date, LnxDb2Type.Date)]
        [InlineData(Db2Type.Time, LnxDb2Type.Time)]
        [InlineData(Db2Type.Timestamp, LnxDb2Type.Timestamp)]
        [InlineData(Db2Type.Clob, LnxDb2Type.Clob)]
        [InlineData(Db2Type.Blob, LnxDb2Type.Blob)]
        [InlineData(Db2Type.Xml, LnxDb2Type.Xml)]
        [InlineData(Db2Type.Boolean, LnxDb2Type.Boolean)]
        public void ToLinux_Should_Map_Correctly(Db2Type input, LnxDb2Type expected)
        {
            var result = Db2TypeMapper.ToLinux(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void All_Db2Type_Values_Should_Have_Windows_Mapping()
        {
            foreach (var type in Enum.GetValues<Db2Type>())
            {
                var exception = Record.Exception(() => Db2TypeMapper.ToWindows(type));
                Assert.Null(exception);
            }
        }

        [Fact]
        public void All_Db2Type_Values_Should_Have_Linux_Mapping()
        {
            foreach (var type in Enum.GetValues<Db2Type>())
            {
                var exception = Record.Exception(() => Db2TypeMapper.ToLinux(type));
                Assert.Null(exception);
            }
        }
    }
}
