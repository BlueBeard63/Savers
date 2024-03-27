using System;

namespace Savers.MySql.Tables.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class DatabaseTable(string table_name) : Attribute
{
    internal string TableName { get; set; } = table_name;
}