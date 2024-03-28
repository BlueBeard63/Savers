using System;
using System.Reflection;
using Savers.Shared.Savers.Sql.Enums;
using Savers.Shared.Savers.Sql.Interfaces;

namespace Savers.SqlLite.Tables.Attributes;

public class DatabaseColumn : IDatabaseColumn
{
    public string ColumnName { get; set; }
    public string ColumnType { get; set; }
    public int ColumnSize { get; set; }
    public ColumnFlag ColumnFlag { get; set; }
    
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
    
    public (string, string, string) GenerateDDLForColumn(PropertyInfo property_info)
    {
        throw new NotImplementedException();
    }

    public (string, string) GenerateForeignTable(Type type)
    {
        throw new NotImplementedException();
    }

    public int GetColumnSize(MemberInfo primary_key_info)
    {
        throw new NotImplementedException();
    }

    public string CreateFlagString()
    {
        throw new NotImplementedException();
    }

    public string GetColumnType(Type property_type)
    {
        throw new NotImplementedException();
    }

    public bool IsPrimaryKey()
    {
        throw new NotImplementedException();
    }

    public string GetColumnName()
    {
        throw new NotImplementedException();
    }

    public bool HasFlag(ColumnFlag flag)
    {
        throw new NotImplementedException();
    }
}