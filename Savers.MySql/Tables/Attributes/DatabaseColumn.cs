using System.Reflection;
using Savers.MySql.Tables.Enums;

namespace Savers.MySql.Tables.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class DatabaseColumn : Attribute
{
    internal string ColumnName { get; set; }
    private ColumnType ColumnType { get; set; }
    private int ColumnSize { get; set; }
    private ColumnFlag ColumnFlag { get; set; }

    public DatabaseColumn(string column_name, ColumnFlag column_flag)
    {
        ColumnSize = 0;
        ColumnName = column_name;
        ColumnFlag = column_flag;
    }

    public DatabaseColumn(string column_name)
    {
        ColumnSize = 0;
        ColumnName = column_name;
        ColumnFlag = ColumnFlag.None;
    }

    // ReSharper disable once InconsistentNaming
    public (string, string, string) GenerateDDLForColumn(PropertyInfo property_info)
    {
        ColumnType = GetColumnType(property_info.PropertyType);

        var constraint = "";
        var foreign_table_dll = "";

        if (ColumnFlag.HasFlag(ColumnFlag.ForeignKey))
        {
            var type = property_info.PropertyType;
            var generate_foreign_table = GenerateForeignTable(type);

            constraint = generate_foreign_table.Item1;
            foreign_table_dll = generate_foreign_table.Item2;
        }

        if (ColumnFlag.HasFlag(ColumnFlag.PrimaryKey))
        {
            constraint = "CONSTRAINT PK_{0}" + $" PRIMARY KEY ({ColumnName})";
        }

        var field_flags = CreateFlagString();
        var field =
            $"{ColumnName} {ColumnType.ToString().Replace('_', ' ')}{(ColumnSize != 0 ? $"({ColumnSize})" : "")}{(!string.IsNullOrEmpty(field_flags) ? " " + field_flags : "")}";

        return (field, constraint, foreign_table_dll);
    }

    private (string, string) GenerateForeignTable(Type type)
    {
        var ddl = "";

        if (!GenerateTable.DoesExist(type))
        {
            ddl = GenerateTable.Generate(type);
        }

        var type_table_name = GenerateTable.GetTableName(type);
        var type_primary_key = GenerateTable.GetPrimaryKey(type);

        ColumnType = GetColumnType(type_primary_key.PrimaryKeyInfo.PropertyType);
        ColumnSize = GetColumnSize(type_primary_key.PrimaryKeyInfo);

        var constraint = "CONSTRAINT FK_{0}_" + ColumnName +
                         $" FOREIGN KEY ({ColumnName}) REFERENCES {type_table_name}({type_primary_key.PrimaryKeyName})";

        return (constraint, ddl);
    }

    private int GetColumnSize(MemberInfo primary_key_info)
    {
        var column = primary_key_info.GetCustomAttribute<DatabaseColumn>();
        return column.ColumnSize;
    }

    private string CreateFlagString()
    {
        var flag_dict = new Dictionary<string, int>();
        if (ColumnFlag.HasFlag(ColumnFlag.AutoIncrement))
        {
            flag_dict.Add("AUTO_INCREMENT", 99);
        }

        if (ColumnFlag.HasFlag(ColumnFlag.NotNull))
        {
            flag_dict.Add("NOT NULL", 100);
        }

        return string.Join(" ", flag_dict.OrderByDescending(x => x.Value).Select(x => x.Key));
    }

    public ColumnType GetColumnType(Type property_type)
    {
        if (property_type == typeof(int))
        {
            return ColumnType.INT;
        }

        if (property_type == typeof(long))
        {
            return ColumnType.BIGINT;
        }

        if (property_type == typeof(bool))
        {
            return ColumnType.BIT;
        }

        if (property_type == typeof(char))
        {
            return ColumnType.CHAR;
        }

        if (property_type == typeof(Guid))
        {
            ColumnSize = 36;
            return ColumnType.CHAR;
        }

        if (property_type == typeof(DateTime))
        {
            return ColumnType.DATETIME;
        }

        if (property_type == typeof(decimal))
        {
            return ColumnType.DECIMAL;
        }

        if (property_type == typeof(float))
        {
            return ColumnType.FLOAT;
        }

        if (property_type == typeof(string))
        {
            ColumnSize = 255;
            return ColumnType.VARCHAR;
        }

        if (property_type == typeof(byte))
        {
            return ColumnType.TINYINT;
        }

        if (property_type == typeof(byte[]))
        {
            return ColumnType.VARBINARY;
        }

        ColumnSize = 255;
        return ColumnType.VARCHAR;
    }

    public bool IsPrimaryKey() =>
        ColumnFlag.HasFlag(ColumnFlag.PrimaryKey);

    public string GetColumnName() =>
        ColumnName;
    
    public bool HasFlag(ColumnFlag flag) =>
        ColumnFlag.HasFlag(flag);
}