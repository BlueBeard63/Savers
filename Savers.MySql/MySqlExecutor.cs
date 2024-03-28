using System.Collections.Generic;
using Dapper;
using MySql.Data.MySqlClient;
using Savers.Shared.Savers.Sql;
using Savers.Shared.Savers.Sql.Interfaces;

namespace Savers.MySql;

internal class MySqlExecutor(SqlData data) : IExecutor
{
    public SqlData Data { get; set; } = data;

    public bool Execute()
    {
        try
        {
            using var conn = new MySqlConnection(Data.ConnectionPath);

            var data_params = new DynamicParameters();

            foreach (var param in Data.Params)
            {
                data_params.Add(param.ParamName, param.ParamObject, param.DbType);
            }

            conn.Execute(Data.Sql, data_params);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool Query<T>(out T return_value)
    {
        var query = QueryEnumerable<T>(out var return_types);
        return_value = (T)return_types;

        return query;
    }

    private bool QueryEnumerable<T>(out IEnumerable<T> return_type)
    {
        try
        {
            using var conn = new MySqlConnection(Data.ConnectionPath);
            var data_params = new DynamicParameters();

            foreach (var param in Data.Params)
            {
                data_params.Add(param.ParamName, param.ParamObject, param.DbType);
            }

            return_type = conn.Query<T>(Data.Sql, data_params);
            return true;
        }
        catch
        {
            return_type = new List<T>();
            return false;
        }
    }

    internal bool Execute(string ddl)
    {
        try
        {
            using var conn = new MySqlConnection(Data.ConnectionPath);
            conn.Execute(ddl);
            return true;
        }
        catch
        {
            return false;
        }
    }
}