namespace Savers.Shared.Savers.Sql.Interfaces;

public interface IClause<out T>
{
    SqlData Data { get; set; }
        
    IClause<T> Where(params (string, object)[] filters);
    IClause<T> WhereStarts(params (string, string)[] filters);
    IClause<T> WhereEnds(params (string, string)[] filters);
    IClause<T> WhereContains(params (string, string)[] filters);

    IClause<T> OrderBy(Order order, params string[] columns);
    IClause<T> OrderBy(params (string, Order)[] columns);

    IClause<T> Limit(int count);
        
    IExecutor Finalise();
}