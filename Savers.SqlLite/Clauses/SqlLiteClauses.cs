using Savers.Shared.Savers.Sql;
using Savers.Shared.Savers.Sql.Interfaces;

namespace Savers.SqlLite.Clauses;

internal class SqlLiteClauses<T>(SqlData data) : IClause<T>
{
    public SqlData Data { get; } = data;

    public IClause<T> Where(params (string, object)[] filters)
    {
        throw new System.NotImplementedException();
    }

    public IClause<T> WhereStarts(params (string, string)[] filters)
    {
        throw new System.NotImplementedException();
    }

    public IClause<T> WhereEnds(params (string, string)[] filters)
    {
        throw new System.NotImplementedException();
    }

    public IClause<T> WhereContains(params (string, string)[] filters)
    {
        throw new System.NotImplementedException();
    }

    public IClause<T> OrderBy(Order order, params string[] columns)
    {
        throw new System.NotImplementedException();
    }

    public IClause<T> OrderBy(params (string, Order)[] columns)
    {
        throw new System.NotImplementedException();
    }

    public IClause<T> Limit(int count)
    {
        throw new System.NotImplementedException();
    }

    public IExecutor Finalise()
    {
        throw new System.NotImplementedException();
    }
}