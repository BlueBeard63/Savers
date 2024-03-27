using System.Reflection;
using Savers.MySql.Tables.Attributes;
using Savers.Shared.Exceptions;

namespace Savers.MySql.Tables;

public static class GenerateTable
{
    private const string TableFormat = "CREATE TABLE IF NOT EXISTS {0} ( {1}, {2} );";
    private static Dictionary<Type, (string, PrimaryKeyData)> TypesToTableData { get; set; } = new();

    public static string Generate(Type type)
    {
        if (!type.GetCustomAttributes<DatabaseTable>().Any())
        {
            throw new CannotGetTableName();
        }

        var table_data = type.GetCustomAttribute<DatabaseTable>();
        var properties = type.GetProperties();

        if (!properties.Any())
        {
            throw new NoPropertiesToSerialize();
        }

        var property_columns = properties
            .Where(x => x.GetCustomAttributes<DatabaseColumn>().Any() || !x.GetCustomAttributes<DatabaseIgnore>().Any())
            .Select(x => (x,
                x.GetCustomAttributes<DatabaseColumn>().Any()
                    ? x.GetCustomAttribute<DatabaseColumn>()
                    : new DatabaseColumn(x.Name)))
            .ToList();

        if (!property_columns.Any(x => x.Item2.IsPrimaryKey()))
        {
            throw new NoPrimaryKeyFound("No Primary Key Found For Type: " + type.Name);
        }
        
        var primary_key = property_columns.First(x => x.Item2.IsPrimaryKey());
        
        var columns = property_columns
            .Select(x => x.Item2.GenerateDDLForColumn(x.x))
            .ToList();

        var reference_tables = columns
            .Where(x => !string.IsNullOrWhiteSpace(x.Item3))
            .Select(x => x.Item3)
            .ToList();
        
        var fields = columns
            .Select(x => x.Item1);
        
        var constraints = columns
            .Select(x => x.Item2)
            .Where(x => !string.IsNullOrEmpty(x))
            .Select(x => string.Format(x, table_data.TableName));

        var table =
            string.Format(
            TableFormat,
            table_data.TableName,
            string.Join(", ", fields),
            string.Join(", ", constraints));

        TypesToTableData.Add(type, (table_data.TableName, new PrimaryKeyData(primary_key.Item2.GetColumnName(), primary_key.x)));

        return reference_tables.Any() ? string.Join(" ", reference_tables) + " " + table : table;
    }

    public static bool DoesExist(Type type) => 
        TypesToTableData.ContainsKey(type);

    public static string GetTableName(Type type) => 
        TypesToTableData[type].Item1;

    public static PrimaryKeyData GetPrimaryKey(Type type) => 
        TypesToTableData[type].Item2;
}