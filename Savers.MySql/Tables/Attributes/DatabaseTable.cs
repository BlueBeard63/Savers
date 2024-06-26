using System;
using Savers.Shared.Savers.Sql.Interfaces;

namespace Savers.MySql.Tables.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class DatabaseTable(string table_name) : Attribute, IDatabaseTable
{
    public string TableName { get; set; } = table_name;
}