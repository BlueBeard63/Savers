using System.Linq;
using System.Reflection;
using Savers.MySql.Clauses;
using Savers.MySql.Tables.Attributes;
using Savers.MySql.Tables.Enums;
using Savers.Shared.Savers.Sql;
using Savers.Shared.Savers.Sql.Interfaces;

namespace Savers.MySql.Statements;

internal class MySqlStatements<T>(SqlData sql_data) : IStatement<T>
{
    private readonly string _tableName = typeof(T).GetCustomAttribute<DatabaseTable>().TableName;

    public SqlData Data { get; set; } = sql_data;

    public IClause<T> Select(params string[] columns)
    {
        Data.StatementSql = $"SELECT {(columns.Any() ? string.Join(" AND ", columns) : "*")} FROM {_tableName}";
        return new MySqlClauses<T>(Data);
    }

    public IClause<T> Count()
    {
        Data.StatementSql = $"SELECT COUNT(*) AS counter FROM {_tableName}";
        return new MySqlClauses<T>(Data);
    }

    public IClause<T> Sum(string column)
    {
        Data.StatementSql = $"SELECT SUM({column}) AS sum FROM {_tableName}";
        return new MySqlClauses<T>(Data);
    }

    public IClause<T> Average(params string[] columns)
    {
        Data.StatementSql = columns.Length > 1
            ? $"SELECT (AVG({string.Join(") + AVG(", columns)}))/2 AS average FROM {_tableName}"
            : $"SELECT AVG({columns[0]}) AS average FROM {_tableName}";
        return new MySqlClauses<T>(Data);
    }

    public IClause<T> Update(params (string, object)[] values)
    {
        var parameters = values.Select(x => new DataColumn
            {
                ColumnName = x.Item1,
                ColumnValue = x.Item2,
                ColumnType = x.Item2.GetType()
            })
            .Select(x =>
            {
                var param = ParameterConverter.GetParam(x, Data.Params);
                Data.Params.Add(param);

                return (ColumnData: x, ColumnParam: param);
            })
            .ToList();

        Data.StatementSql = $"UPDATE {_tableName} SET {string.Join(", ", values.Select(x => x.Item1 + " = " + parameters.Find(y => y.ColumnData.ColumnName == x.Item1).ColumnParam.ParamName))}";
        return new MySqlClauses<T>(Data);
    }

    public IExecutor Insert(T obj)
    {
        var properties = obj.GetType().GetProperties()
            .Where(x => x.GetValue(obj) != null && !x.GetCustomAttributes<DatabaseIgnore>().Any());

        var columns = properties
            .Select(x => (x.GetCustomAttributes<DatabaseColumn>().Any(), x))
            .Select(x => (x.x, x.Item1 ? x.x.GetCustomAttribute<DatabaseColumn>() : new DatabaseColumn(x.x.Name)))
            .Where(x => !x.Item2.HasFlag(ColumnFlag.AutoIncrement))
            .Select(x => new DataColumn
            {
                ColumnName = x.Item2.ColumnName,
                ColumnValue = x.x.GetValue(obj),
                ColumnType = x.x.PropertyType
            })
            .Select(x =>
            {
                var param = ParameterConverter.GetParam(x, Data.Params);
                Data.Params.Add(param);

                return (ColumnData: x, ColumnParam: param);
            })
            .ToList();

        Data.StatementSql = string.Format(
            "INSERT INTO {2} ({0}) VALUES ({1})",
            string.Join(", ", columns.Select(x => x.ColumnData.ColumnName)),
            string.Join(", ", columns.Select(x => x.ColumnParam.ParamName)),
            _tableName
        );

        return new MySqlExecutor(Data);
    }
}