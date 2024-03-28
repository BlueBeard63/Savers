using System;
using System.Collections;
using System.Collections.Generic;
using Savers.MySql.Tables.Attributes;
using Savers.MySql.Tables.Enums;
using Xunit;

namespace Savers.Tests.MySQL.Misc
{
    public class EColumnTypeTests
    {
        [Theory]
        [ClassData(typeof(ColumnTypeTestData))]
        public void Test1(EColumnType detected, EColumnType correct)
        {
            Assert.True(detected == correct, $"detected != correct\nDetected: {detected}\nCorrect: {correct}");
        }
    }

    public class ColumnTypeTestData : IEnumerable<object[]>
    {
        private static readonly DatabaseColumn Column = new("Test");
        
        private static readonly IEnumerable<object[]> TestObjects = new[]
        {
            new object[]
            {
                Column.GetColumnType(typeof(int)),
                EColumnType.INT
            },
            new object[]
            {
                Column.GetColumnType(typeof(long)),
                EColumnType.BIGINT
            },
            new object[]
            {
                Column.GetColumnType(typeof(bool)),
                EColumnType.BIT
            },
            new object[]
            {
                Column.GetColumnType(typeof(char)),
                EColumnType.CHAR
            },
            new object[]
            {
                Column.GetColumnType(typeof(Guid)),
                EColumnType.CHAR
            },
            new object[]
            {
                Column.GetColumnType(typeof(DateTime)),
                EColumnType.DATETIME
            },
            new object[]
            {
                Column.GetColumnType(typeof(decimal)),
                EColumnType.DECIMAL
            },
            new object[]
            {
                Column.GetColumnType(typeof(float)),
                EColumnType.FLOAT
            },
            new object[]
            {
                Column.GetColumnType(typeof(string)),
                EColumnType.VARCHAR
            },
            new object[]
            {
                Column.GetColumnType(typeof(byte)),
                EColumnType.TINYINT
            },
            new object[]
            {
                Column.GetColumnType(typeof(byte[])),
                EColumnType.VARBINARY
            },
            new object[]
            {
                Column.GetColumnType(typeof(uint)),
                EColumnType.VARCHAR
            },
        };

        public IEnumerator<object[]> GetEnumerator() => TestObjects.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}