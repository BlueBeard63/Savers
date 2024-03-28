using System.Linq;
using Savers.Shared.Exceptions;
using Savers.Shared.Savers.Sql;
using Savers.Shared.Savers.Sql.Interfaces;

namespace Savers.SqlLite.Clauses;

internal class SqlLiteClauses<T>(SqlData data) : IClause<T>
{
    public SqlData Data { get; } = data;

    public IClause<T> Where(params (string, object)[] filters)
    {
        if (!filters.Any())
        {
            throw new NoFilterConditions();
        }

        return this;
    }

    public IClause<T> WhereStarts(params (string, string)[] filters)
    {
        if (!filters.Any())
        {
            throw new NoFilterConditions();
        }

        return this;
    }

    public IClause<T> WhereEnds(params (string, string)[] filters)
    {
        if (!filters.Any())
        {
            throw new NoFilterConditions();
        }

        return this;
    }

    public IClause<T> WhereContains(params (string, string)[] filters)
    {
        if (!filters.Any())
        {
            throw new NoFilterConditions();
        }

        return this;
    }

    public IClause<T> OrderBy(Order order, params string[] columns)
    {
        if (!columns.Any())
        {
            throw new NoColumnsToOrderBy();
        }

        return this;
    }

    public IClause<T> OrderBy(params (string, Order)[] columns)
    {
        if (!columns.Any())
        {
            throw new NoColumnsToOrderBy();
        }

        return this;
    }

    public IClause<T> Limit(int count)
    {
        switch (count)
        {
            case 0:
                throw new LimitCannotBeZero();
            case < 0:
                throw new LimitCannotBeNegative();
            default:
                return this;
        }
    }

    public IExecutor Finalise() => new SqlLiteExecutor(Data);
}