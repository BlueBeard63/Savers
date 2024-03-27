namespace Savers.Shared.Savers.Sql.Interfaces;

public interface ISqlSaver<T> : ISaver
{
    // ReSharper disable once InconsistentNaming
    string GenerateTableDDL();
    IStatement<T> StartQuery();
}