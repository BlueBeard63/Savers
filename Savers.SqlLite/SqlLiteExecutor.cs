using Savers.Shared.Savers.Sql;
using Savers.Shared.Savers.Sql.Interfaces;

namespace Savers.SqlLite;

internal class SqlLiteExecutor(SqlData data) : IExecutor
{
    public SqlData Data { get; set; } = data;

    public bool Execute()
    {
        return false;
    }

    public bool Query<T>(out T return_value)
    {
        return_value = default;
        return false;
    }

    internal bool Execute(string ddl)
    {
        return false;
    }
}