using System.Reflection;

namespace Savers.MySql.Tables;

public class PrimaryKeyData(string primary_key_name, PropertyInfo primary_key_info)
{
    internal string PrimaryKeyName { get; set; } = primary_key_name;
    internal PropertyInfo PrimaryKeyInfo { get; set; } = primary_key_info;
}