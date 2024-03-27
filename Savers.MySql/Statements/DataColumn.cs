using System;

namespace Savers.MySql.Statements;

public struct DataColumn
{
    public string ColumnName { get; set; }
    public object ColumnValue { get; set; }
    public Type ColumnType { get; set; }
}