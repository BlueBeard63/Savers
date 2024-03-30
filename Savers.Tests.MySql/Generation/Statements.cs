using System.Collections;
using System.Collections.Generic;
using Savers.MySql;
using Savers.Tests.MySql;
using Xunit;

namespace Savers.Tests.MySQL.Generation
{
    public class Statements
    {
        [Theory]
        [ClassData(typeof(SelectStatementTestData))]
        public void SelectStatement(string generated_sql, string test_sql)
        {
            Assert.True(generated_sql == test_sql,
                $"Generated SQL != Test SQL\nGenerated SQL:\n{generated_sql}\nTest SQL:\n{test_sql}");
        }

        [Theory]
        [ClassData(typeof(CountStatementTestData))]
        public void CountStatement(string generated_sql, string test_sql)
        {
            Assert.True(generated_sql == test_sql,
                $"Generated SQL != Test SQL\nGenerated SQL:\n{generated_sql}\nTest SQL:\n{test_sql}");
        }

        [Theory]
        [ClassData(typeof(SumStatementTestData))]
        public void SumStatement(string generated_sql, string test_sql)
        {
            Assert.True(generated_sql == test_sql,
                $"Generated SQL != Test SQL\nGenerated SQL:\n{generated_sql}\nTest SQL:\n{test_sql}");
        }

        [Theory]
        [ClassData(typeof(AverageStatementTestData))]
        public void AverageStatement(string generated_sql, string test_sql)
        {
            Assert.True(generated_sql == test_sql,
                $"Generated SQL != Test SQL\nGenerated SQL:\n{generated_sql}\nTest SQL:\n{test_sql}");
        }

        [Theory]
        [ClassData(typeof(InsertStatementTestData))]
        public void InsertStatement(string generated_sql, string test_sql)
        {
            Assert.True(generated_sql == test_sql,
                $"Generated SQL != Test SQL\nGenerated SQL:\n{generated_sql}\nTest SQL:\n{test_sql}");
        }

        [Theory]
        [ClassData(typeof(UpdateStatementTestData))]
        public void UpdateStatement(string generated_sql, string test_sql)
        {
            Assert.True(generated_sql == test_sql,
                $"Generated SQL != Test SQL\nGenerated SQL:\n{generated_sql}\nTest SQL:\n{test_sql}");
        }
    }

    public class SelectStatementTestData : IEnumerable<object[]>
    {
        private static readonly IEnumerable<object[]> _TestObjects = new[]
        {
            new object[]
            {
                new MySqlSaver<TestClass>().StartQuery().Select("TestClassString").Finalise().Data.Sql,
                "SELECT TestClassString FROM TestClass;"
            },
            
            new object[]
            {
                new MySqlSaver<TestClass>().StartQuery().Select().Finalise().Data.Sql,
                "SELECT * FROM TestClass;"
            }
        };

        public IEnumerator<object[]> GetEnumerator() => _TestObjects.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
    
    public class CountStatementTestData : IEnumerable<object[]>
    {
        private static readonly IEnumerable<object[]> _TestObjects = new[]
        {
            new object[]
            {
                new MySqlSaver<TestClass>().StartQuery().Count().Finalise().Data.Sql,
                "SELECT COUNT(*) AS counter FROM TestClass;"
            }
        };

        public IEnumerator<object[]> GetEnumerator() => _TestObjects.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
    
    public class SumStatementTestData : IEnumerable<object[]>
    {
        private static readonly IEnumerable<object[]> _TestObjects = new[]
        {
            new object[]
            {
                new MySqlSaver<TestClass>().StartQuery().Sum("TestValue").Finalise().Data.Sql,
                "SELECT SUM(TestValue) AS sum FROM TestClass;"
            }
        };

        public IEnumerator<object[]> GetEnumerator() => _TestObjects.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
    
    public class AverageStatementTestData : IEnumerable<object[]>
    {
        private static readonly IEnumerable<object[]> _TestObjects = new[]
        {
            new object[]
            {
                new MySqlSaver<TestClass>().StartQuery().Average("TestValue").Finalise().Data.Sql,
                "SELECT AVG(TestValue) AS average FROM TestClass;"
            },
            new object[]
            {
                new MySqlSaver<TestClass>().StartQuery().Average("TestValue", "TestValue2").Finalise().Data.Sql,
                "SELECT (AVG(TestValue) + AVG(TestValue2))/2 AS average FROM TestClass;"
            }
        };

        public IEnumerator<object[]> GetEnumerator() => _TestObjects.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
    
    public class InsertStatementTestData : IEnumerable<object[]>
    {
        private static readonly IEnumerable<object[]> _TestObjects = new[]
        {
            new object[]
            {
                new MySqlSaver<TestClass>().StartQuery().Insert(new TestClass { TestClassString = "TestString" }).Data.Sql,
                "INSERT INTO TestClass (TestClassString) VALUES (@TestClassString);"
            }
        };

        public IEnumerator<object[]> GetEnumerator() => _TestObjects.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class UpdateStatementTestData : IEnumerable<object[]>
    {
        private static readonly IEnumerable<object[]> _TestObjects = new[]
        {
            new object[]
            {
                new MySqlSaver<TestClass>().StartQuery().Update(("TestClassString", "Example")).Finalise().Data.Sql,
                "UPDATE TestClass SET TestClassString = @TestClassString;"
            },
            new object[]
            {
                new MySqlSaver<TestClass>().StartQuery().Update(("TestClassString", "Example"), ("TestExampleInt", 5)).Finalise().Data.Sql,
                "UPDATE TestClass SET TestClassString = @TestClassString, TestExampleInt = @TestExampleInt;"
            },
        };

        public IEnumerator<object[]> GetEnumerator() => _TestObjects.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}