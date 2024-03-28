using Savers.Shared.Savers.Sql.Interfaces;

namespace Savers.SqlLite.Tables.Attributes;

public class DatabaseTable(string table_name) : IDatabaseTable
{
    public string TableName { get; set; } = table_name;
}