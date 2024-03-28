using System;
using System.Reflection;
using Savers.Shared.Savers.Sql.Enums;

namespace Savers.Shared.Savers.Sql.Interfaces;

public interface IDatabaseColumn
{
    public string ColumnName { get; set; }
    public string ColumnType { get; set; }
    public int ColumnSize { get; set; }
    public ColumnFlag ColumnFlag { get; set; }
    
    public (string, string, string) GenerateDDLForColumn(PropertyInfo property_info);
    public (string, string) GenerateForeignTable(Type type);
    public int GetColumnSize(MemberInfo primary_key_info);
    public string CreateFlagString();
    public string GetColumnType(Type property_type);
    public bool IsPrimaryKey();
    public string GetColumnName();
    public bool HasFlag(ColumnFlag flag);

}