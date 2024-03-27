using System.Data;

namespace Savers.Shared.Savers.Sql;

public class DataParam(string name, object obj, DbType? db_type = null)
{
    public string ParamName { get; } = name;
    public object ParamObject { get; } = obj;
    public DbType? DbType { get; set; } = db_type;

    public override bool Equals(object obj) =>
        obj is DataParam param && Equals(param);

    private bool Equals(DataParam other) =>
        ParamName == other.ParamName && ParamObject.Equals(other.ParamObject);

    public override int GetHashCode()
    {
        unchecked
        {
            var hash_code = ParamName.GetHashCode();
            hash_code = (hash_code * 397) ^ ParamObject.GetHashCode();
            return hash_code;
        }
    }
}