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
        public void ColumnTypeDetection(string detected, string correct)
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
                "INT"
            },
            new object[]
            {
                Column.GetColumnType(typeof(long)),
                "BIGINT"
            },
            new object[]
            {
                Column.GetColumnType(typeof(bool)),
                "BIT"
            },
            new object[]
            {
                Column.GetColumnType(typeof(char)),
                "CHAR"
            },
            new object[]
            {
                Column.GetColumnType(typeof(Guid)),
                "CHAR"
            },
            new object[]
            {
                Column.GetColumnType(typeof(DateTime)),
                "DATETIME"
            },
            new object[]
            {
                Column.GetColumnType(typeof(decimal)),
                "DECIMAL"
            },
            new object[]
            {
                Column.GetColumnType(typeof(float)),
                "FLOAT"
            },
            new object[]
            {
                Column.GetColumnType(typeof(string)),
                "VARCHAR"
            },
            new object[]
            {
                Column.GetColumnType(typeof(byte)),
                "TINYINT"
            },
            new object[]
            {
                Column.GetColumnType(typeof(byte[])),
                "VARBINARY"
            },
            new object[]
            {
                Column.GetColumnType(typeof(uint)),
                "VARCHAR"
            },
        };

        public IEnumerator<object[]> GetEnumerator() => TestObjects.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}