using System.Collections.Generic;
using System.Linq;

namespace Savers.Shared.Savers.Sql;

public class SqlData(string connection_path)
{
    public string Sql => StatementSql + FilterSql + ";";
    public string ConnectionPath { get; private set; } = connection_path;
    public string StatementSql { get; set; }
    public string FilterSql => string.Join(" ", Filters.OrderBy(x => x.Value.Item2).Select(x => $"{x.Key} {x.Value.Item1}"));

    public Dictionary<string, (string, int)> Filters { get; set; } = [];

    public List<DataParam> Params { get; set; } = [];
}