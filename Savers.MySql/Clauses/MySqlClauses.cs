using System.Linq;
using Savers.MySql.Statements;
using Savers.Shared.Exceptions;
using Savers.Shared.Savers.Sql;
using Savers.Shared.Savers.Sql.Interfaces;

namespace Savers.MySql.Clauses;

internal class MySqlClauses<T>(SqlData data) : IClause<T>
{
    public SqlData Data { get; set; } = data;

    public IClause<T> Where(params (string, object)[] filters)
    {
        if (!filters.Any())
        {
            throw new NoFilterConditions();
        }

        var filter_parts = filters.Select(x =>
            {
                var param = ParameterConverter.GetParam(new DataColumn
                {
                    ColumnName = x.Item1,
                    ColumnValue = x.Item2,
                    ColumnType = x.Item2.GetType()
                }, Data.Params);

                Data.Params.Add(param);

                return param;
            })
            .Select(x => $"{filters.Single().Item1} = {x.ParamName}");

        var filter_sql = string.Join(" AND ", filter_parts);

        if (Data.Filters.ContainsKey("WHERE"))
        {
            var current_filter = Data.Filters["WHERE"].Item1;
            Data.Filters["WHERE"] = (current_filter + $" AND {filter_sql}", 1);
        }
        else
        {
            Data.Filters.Add("WHERE", (filter_sql, 1));
        }

        return new MySqlClauses<T>(Data);
    }

    public IClause<T> WhereStarts(params (string, string)[] filters)
    {
        if (!filters.Any())
        {
            throw new NoFilterConditions();
        }

        var filter_parts = filters.Select(x =>
            {
                var param = ParameterConverter.GetParam(new DataColumn
                {
                    ColumnName = x.Item1,
                    ColumnValue = x.Item2,
                    ColumnType = x.Item2.GetType()
                }, Data.Params);

                Data.Params.Add(param);

                return (x, param);
            })
            .Select(x => $"{x.x.Item1} = {x.param.ParamName}%");

        var filter_sql = string.Join(" AND ", filter_parts);

        if (Data.Filters.ContainsKey("WHERE"))
        {
            var current_filter = Data.Filters["WHERE"].Item1;
            Data.Filters["WHERE"] = (current_filter + $" AND {filter_sql}", 1);
        }
        else
        {
            Data.Filters.Add("WHERE", (filter_sql, 1));
        }

        return new MySqlClauses<T>(Data);
    }

    public IClause<T> WhereEnds(params (string, string)[] filters)
    {
        if (!filters.Any())
        {
            throw new NoFilterConditions();
        }

        var filter_parts = filters.Select(x =>
            {
                var param = ParameterConverter.GetParam(new DataColumn
                {
                    ColumnName = x.Item1,
                    ColumnValue = x.Item2,
                    ColumnType = x.Item2.GetType()
                }, Data.Params);

                Data.Params.Add(param);

                return (x, param);
            })
            .Select(x => $"{x.x.Item1} = %{x.param.ParamName}");

        var filter_sql = string.Join(" AND ", filter_parts);

        if (Data.Filters.ContainsKey("WHERE"))
        {
            var current_filter = Data.Filters["WHERE"].Item1;
            Data.Filters["WHERE"] = (current_filter + $" AND {filter_sql}", 1);
        }
        else
        {
            Data.Filters.Add("WHERE", (filter_sql, 1));
        }

        return new MySqlClauses<T>(Data);
    }

    public IClause<T> WhereContains(params (string, string)[] filters)
    {
        if (!filters.Any())
        {
            throw new NoFilterConditions();
        }

        var filter_parts = filters.Select(x =>
            {
                var param = ParameterConverter.GetParam(new DataColumn
                {
                    ColumnName = x.Item1,
                    ColumnValue = x.Item2,
                    ColumnType = x.Item2.GetType()
                }, Data.Params);

                Data.Params.Add(param);

                return (x, param);
            })
            .Select(x => $"{x.x.Item1} = %{x.param.ParamName}%");

        var filter_sql = string.Join(" AND ", filter_parts);

        if (Data.Filters.ContainsKey("WHERE"))
        {
            var current_filter = Data.Filters["WHERE"].Item1;
            Data.Filters["WHERE"] = (current_filter + $" AND {filter_sql}", 1);
        }
        else
        {
            Data.Filters.Add("WHERE", (filter_sql, 1));
        }

        return new MySqlClauses<T>(Data);
    }

    public IClause<T> OrderBy(Order order, params string[] columns)
    {
        if (!columns.Any())
        {
            throw new NoColumnsToOrderBy();
        }

        var ordered_columns = columns.Select(x => $"{x} {(order == Order.Ascending ? "ASC" : "DESC")}");
        if (Data.Filters.ContainsKey("ORDER BY"))
        {
            var order_by = Data.Filters["ORDER BY"];
            Data.Filters["ORDER BY"] = (order_by.Item1 + ", " + string.Join(", ", ordered_columns), order_by.Item2);
        }
        else
        {
            Data.Filters.Add("ORDER BY", (string.Join(", ", ordered_columns), int.MaxValue-1));
        }
        
        return new MySqlClauses<T>(Data);
    }

    public IClause<T> OrderBy(params (string, Order)[] columns)
    {
        if (!columns.Any())
        {
            throw new NoColumnsToOrderBy();
        }

        var ordered_columns = columns.Select(x => $"{x.Item1} {(x.Item2 == Order.Ascending ? "ASC" : "DESC")}");
        if (Data.Filters.ContainsKey("ORDER BY"))
        {
            var order_by = Data.Filters["ORDER BY"];
            Data.Filters["ORDER BY"] = (order_by.Item1 + ", " + string.Join(", ", ordered_columns), order_by.Item2);
        }
        else
        {
            Data.Filters.Add("ORDER BY", (string.Join(", ", ordered_columns), int.MaxValue-1));
        }

        return new MySqlClauses<T>(Data);
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
                Data.Filters["LIMIT"] = (count.ToString(), int.MaxValue);
                return new MySqlClauses<T>(Data);
        }
    }

    public IExecutor Finalise()
    {
        return new MySqlExecutor(Data);
    }
}