namespace Savers.Shared.Savers.Sql.Interfaces;

public interface IStatement<T>
{
    SqlData Data { get; set; }
        
    IClause<T> Select(params string[] columns);
    IClause<T> Count();
    IClause<T> Sum(string column);
    IClause<T> Average(params string[] columns);
    IClause<T> Update(params (string, object)[] values);
        
    IExecutor Insert(T obj);
}