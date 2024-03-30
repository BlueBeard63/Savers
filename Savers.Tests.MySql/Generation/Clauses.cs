using System.Collections;
using System.Collections.Generic;
using Savers.MySql;
using Savers.Shared.Savers.Sql;
using Savers.Tests.MySql;
using Xunit;

namespace Savers.Tests.MySQL.Generation
{
    public class Clauses
    {
        [Theory]
        [ClassData(typeof(WhereTestData))]
        public void Where(string generated, string manual)
        {
            Assert.True(generated == manual,
                $"Generated SQL != Test SQL\nGenerated:\n{generated}\nManual:\n{manual}");
        }

        [Theory]
        [ClassData(typeof(WhereStartsTestData))]
        public void WhereStarts(string generated, string manual)
        {
            Assert.True(generated == manual,
                $"Generated SQL != Test SQL\nGenerated:\n{generated}\nManual:\n{manual}");
        }

        [Theory]
        [ClassData(typeof(WhereEndsTestData))]
        public void WhereEnds(string generated, string manual)
        {
            Assert.True(generated == manual,
                $"Generated SQL != Test SQL\nGenerated:\n{generated}\nManual:\n{manual}");
        }

        [Theory]
        [ClassData(typeof(WhereContainsTestData))]
        public void WhereContains(string generated, string manual)
        {
            Assert.True(generated == manual,
                $"Generated SQL != Test SQL\nGenerated:\n{generated}\nManual:\n{manual}");
        }

        [Theory]
        [ClassData(typeof(OrderByTestData))]
        public void OrderBy(string generated, string manual)
        {
            Assert.True(generated == manual,
                $"Generated SQL != Test SQL\nGenerated:\n{generated}\nManual:\n{manual}");
        }

        [Theory]
        [ClassData(typeof(LimitTestData))]
        public void Limit(string generated, string manual)
        {
            Assert.True(generated == manual,
                $"Generated SQL != Test SQL\nGenerated:\n{generated}\nManual:\n{manual}");
        }
    }

    public class LimitTestData : IEnumerable<object[]>
    {
        private static readonly IEnumerable<object[]> _TestObjects = new[]
        {
            new object[]
            {
                new MySqlSaver<TestClass>()
                    .StartQuery()
                    .Count()
                    .Limit(10)
                    .Finalise().Data
                    .FilterSql,
                "LIMIT 10"
            }
        };

        public IEnumerator<object[]> GetEnumerator() => _TestObjects.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class OrderByTestData : IEnumerable<object[]>
    {
        private static readonly IEnumerable<object[]> _TestObjects = new[]
        {
            new object[]
            {
                new MySqlSaver<TestClass>()
                    .StartQuery()
                    .Count()
                    .OrderBy(Order.Ascending, "TestValue").Finalise().Data
                    .FilterSql,
                "ORDER BY TestValue ASC"
            },
            new object[]
            {
                new MySqlSaver<TestClass>()
                    .StartQuery()
                    .Count()
                    .OrderBy(Order.Descending, "TestValue").Finalise().Data
                    .FilterSql,
                "ORDER BY TestValue DESC"
            },
            new object[]
            {
                new MySqlSaver<TestClass>()
                    .StartQuery()
                    .Count()
                    .OrderBy(Order.Ascending, "TestValue", "TestValue2")
                    .Finalise().Data.FilterSql,
                "ORDER BY TestValue ASC, TestValue2 ASC"
            },
            new object[]
            {
                new MySqlSaver<TestClass>()
                    .StartQuery()
                    .Count()
                    .OrderBy(Order.Descending, "TestValue", "TestValue2")
                    .Finalise().Data.FilterSql,
                "ORDER BY TestValue DESC, TestValue2 DESC"
            },
            new object[]
            {
                new MySqlSaver<TestClass>()
                    .StartQuery()
                    .Count()
                    .OrderBy(("TestValue", Order.Ascending), ("TestValue2", Order.Descending))
                    .Finalise().Data.FilterSql,
                "ORDER BY TestValue ASC, TestValue2 DESC"
            }
        };

        public IEnumerator<object[]> GetEnumerator() => _TestObjects.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class WhereContainsTestData : IEnumerable<object[]>
    {
        private static readonly IEnumerable<object[]> _TestObjects = new[]
        {
            new object[]
            {
                new MySqlSaver<TestClass>()
                    .StartQuery()
                    .Count()
                    .WhereContains(("TestValueString", "abc"))
                    .Finalise().Data
                    .FilterSql,
                "WHERE TestValueString = %@TestValueString%"
            }
        };

        public IEnumerator<object[]> GetEnumerator() => _TestObjects.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class WhereEndsTestData : IEnumerable<object[]>
    {
        private static readonly IEnumerable<object[]> _TestObjects = new[]
        {
            new object[]
            {
                new MySqlSaver<TestClass>()
                    .StartQuery()
                    .Count()
                    .WhereEnds(("TestValueString", "abc"))
                    .Finalise().Data
                    .FilterSql,
                "WHERE TestValueString = %@TestValueString"
            }
        };

        public IEnumerator<object[]> GetEnumerator() => _TestObjects.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class WhereStartsTestData : IEnumerable<object[]>
    {
        private static readonly IEnumerable<object[]> _TestObjects = new[]
        {
            new object[]
            {
                new MySqlSaver<TestClass>()
                    .StartQuery()
                    .Count()
                    .WhereStarts(("TestValueString", "abc"))
                    .Finalise().Data
                    .FilterSql,
                "WHERE TestValueString = @TestValueString%"
            }
        };

        public IEnumerator<object[]> GetEnumerator() => _TestObjects.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class WhereTestData : IEnumerable<object[]>
    {
        private static readonly IEnumerable<object[]> _TestObjects = new[]
        {
            new object[]
            {
                new MySqlSaver<TestClass>()
                    .StartQuery()
                    .Count()
                    .Where(("TestValueString", "abc"))
                    .Finalise().Data
                    .FilterSql,
                "WHERE TestValueString = @TestValueString"
            }
        };

        public IEnumerator<object[]> GetEnumerator() => _TestObjects.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}