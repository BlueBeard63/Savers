using System;
using System.Collections;
using System.Collections.Generic;
using Savers.MySql.Tables.Attributes;
using Savers.MySql.Tables.Enums;
using Xunit;

namespace Savers.Tests.MySQL.Misc
{
    public class ColumnTypeTests
    {
        [Theory]
        [ClassData(typeof(ColumnTypeTestData))]
        public void Test1(ColumnType detected, ColumnType correct)
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
                ColumnType.INT
            },
            new object[]
            {
                Column.GetColumnType(typeof(long)),
                ColumnType.BIGINT
            },
            new object[]
            {
                Column.GetColumnType(typeof(bool)),
                ColumnType.BIT
            },
            new object[]
            {
                Column.GetColumnType(typeof(char)),
                ColumnType.CHAR
            },
            new object[]
            {
                Column.GetColumnType(typeof(Guid)),
                ColumnType.CHAR
            },
            new object[]
            {
                Column.GetColumnType(typeof(DateTime)),
                ColumnType.DATETIME
            },
            new object[]
            {
                Column.GetColumnType(typeof(decimal)),
                ColumnType.DECIMAL
            },
            new object[]
            {
                Column.GetColumnType(typeof(float)),
                ColumnType.FLOAT
            },
            new object[]
            {
                Column.GetColumnType(typeof(string)),
                ColumnType.VARCHAR
            },
            new object[]
            {
                Column.GetColumnType(typeof(byte)),
                ColumnType.TINYINT
            },
            new object[]
            {
                Column.GetColumnType(typeof(byte[])),
                ColumnType.VARBINARY
            },
            new object[]
            {
                Column.GetColumnType(typeof(uint)),
                ColumnType.VARCHAR
            },
        };

        public IEnumerator<object[]> GetEnumerator() => TestObjects.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}