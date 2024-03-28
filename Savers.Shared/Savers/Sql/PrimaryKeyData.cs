using System.Reflection;

namespace Savers.Shared.Savers.Sql;

public class PrimaryKeyData(string primary_key_name, PropertyInfo primary_key_info)
{
    public string PrimaryKeyName { get; set; } = primary_key_name;
    public PropertyInfo PrimaryKeyInfo { get; set; } = primary_key_info;
}