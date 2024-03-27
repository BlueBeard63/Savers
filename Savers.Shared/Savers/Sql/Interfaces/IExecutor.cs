namespace Savers.Shared.Savers.Sql.Interfaces;

public interface IExecutor
{
    SqlData Data { get; set; }
        
    bool Execute();
    bool Query<T>(out T return_value);
}