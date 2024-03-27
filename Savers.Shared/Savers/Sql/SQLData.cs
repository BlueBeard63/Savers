namespace Savers.Shared.Savers.Sql;

public class SqlData
{
    public string Sql => StatementSql + FilterSql + ";";
    public string ConnectionPath { get; set; }
    public string StatementSql { get; set; }
    public string FilterSql => string.Join(" ", Filters.OrderBy(x => x.Value.Item2).Select(x => $"{x.Key} {x.Value.Item1}"));

    public Dictionary<string, (string, int)> Filters { get; set; } = new();

    public List<DataParam> Params { get; set; } = new();
}