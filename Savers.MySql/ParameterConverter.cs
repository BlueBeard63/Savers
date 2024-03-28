using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Savers.Shared.Savers.Sql;
using DataColumn = Savers.MySql.Statements.DataColumn;

namespace Savers.MySql;

internal class ParameterConverter
{
    public static DataParam GetParam(DataColumn column, List<DataParam> data_params)
    {
        var property_name = "@" + column.ColumnName;
        if (data_params.Any(x => x.ParamName == property_name))
        {
            property_name += data_params.Count(x => x.ParamName == property_name);
        }

        DataParam param;
        if (column.ColumnType == typeof(byte[]))
        {
            param = new DataParam(property_name, (byte[])column.ColumnValue, DbType.Binary);
            return param;
        }

        if (column.ColumnType == typeof(Guid))
        {
            param = new DataParam(property_name, ((Guid)column.ColumnValue).ToString());
            return param;
        }

        param = new DataParam(property_name, column.ColumnValue);
        return param;
    }
}