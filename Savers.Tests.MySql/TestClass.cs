using Savers.MySql.Tables.Attributes;
using Savers.Shared.Savers.Sql.Enums;

namespace Savers.Tests.MySql
{
    [DatabaseTable("TestClass")]
    public class TestClass
    {
        [DatabaseColumn("ID", ColumnFlag.AutoIncrement | ColumnFlag.NotNull | ColumnFlag.PrimaryKey)]
        public int Id { get; set; }
        
        [DatabaseColumn("TestClassString")]
        public string TestClassString { get; set; }
    }

    [DatabaseTable("TestClass2")]
    public class TestClass2
    {
        [DatabaseColumn("ID", ColumnFlag.PrimaryKey | ColumnFlag.AutoIncrement | ColumnFlag.NotNull)]
        public int Id { get; set; }
        
        [DatabaseColumn("TestClass2String", ColumnFlag.NotNull)]
        public string TestClass2String { get; set; }
        
        [DatabaseColumn("TestClassLink", ColumnFlag.ForeignKey | ColumnFlag.NotNull)]
        public TestClass3 TestClassLink { get; set; }
    }
    
    [DatabaseTable("TestClass3")]
    public class TestClass3
    {
        [DatabaseColumn("ID", ColumnFlag.AutoIncrement | ColumnFlag.NotNull | ColumnFlag.PrimaryKey)]
        public int Id { get; set; }
        
        [DatabaseColumn("TestClassString")]
        public string TestClassString { get; set; }
    }
}