using System.Collections;
using System.Collections.Generic;
using Savers.MySql;
using Savers.Tests.MySql;
using Xunit;

namespace Savers.Tests.MySQL.Generation
{
    public class TableGeneration
    {
        [Theory]
        [ClassData(typeof(NormalTableGenerationTestData))]
        public void NormalTableTest(string table_generated, string table_test)
        {
            Assert.True(table_test == table_generated,
                $@"Table Generation Was not Equal to Table Test.
                Table Generation: {table_generated}
                Table Test:{table_test}");
        }

        [Theory]
        [ClassData(typeof(ForeignKeyTableGenerationTestData))]
        public void ForeignKeyTableTest(string table_generated, string table_test)
        {
            Assert.True(table_generated == table_test,
                $"Table Generation Was not Equal to Table Test.\nTable Generation:\n{table_generated}\n\nTable Test:\n{table_test}");
        }
    }

    public class NormalTableGenerationTestData : IEnumerable<object[]>
    {
        private static readonly IEnumerable<object[]> _TestObjects = new[]
        {
            new object[]
            {
                new MySqlSaver<TestClass>().GenerateTableDDL(),
                "CREATE TABLE IF NOT EXISTS TestClass ( ID INT NOT NULL AUTO_INCREMENT, TestClassString VARCHAR(255), CONSTRAINT PK_TestClass PRIMARY KEY (ID) );"
            }
        };

        public IEnumerator<object[]> GetEnumerator() => _TestObjects.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class ForeignKeyTableGenerationTestData : IEnumerable<object[]>
    {
        private static readonly IEnumerable<object[]> _TestObjects = new[]
        {
            new object[]
            {
                new MySqlSaver<TestClass2>().GenerateTableDDL(),
                "CREATE TABLE IF NOT EXISTS TestClass3 ( ID INT NOT NULL AUTO_INCREMENT, TestClassString VARCHAR(255), CONSTRAINT PK_TestClass3 PRIMARY KEY (ID) ); CREATE TABLE IF NOT EXISTS TestClass2 ( ID INT NOT NULL AUTO_INCREMENT, TestClass2String VARCHAR(255) NOT NULL, TestClassLink INT NOT NULL, CONSTRAINT PK_TestClass2 PRIMARY KEY (ID), CONSTRAINT FK_TestClass2_TestClassLink FOREIGN KEY (TestClassLink) REFERENCES TestClass3(ID) );"
            }
        };

        public IEnumerator<object[]> GetEnumerator() => _TestObjects.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}