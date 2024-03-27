using System.Collections.Generic;
using Savers.MySql.Statements;
using Savers.MySql.Tables;
using Savers.Shared;
using Savers.Shared.Savers;
using Savers.Shared.Savers.Sql;
using Savers.Shared.Savers.Sql.Interfaces;

namespace Savers.MySql;

public class MySqlSaver<T> : ISqlSaver<T>
{
    public SaverType SaverType => SaverType.Sql;
    private string ConnectionPath { get; set; }

    public void Activate(string location)
    {
        ConnectionPath = location;
        
        var executor = new MySqlExecutor(new SqlData
        {
            ConnectionPath = ConnectionPath,
            Filters = new Dictionary<string, (string, int)>(),
            Params = new List<DataParam>()
        });

        executor.Execute(GenerateTableDDL());
    }

    public void Deactivate()
    {
    }

    public string GenerateTableDDL() =>
        GenerateTable.Generate(typeof(T));

    public IStatement<T> StartQuery() =>
        new MySqlStatements<T>(new SqlData
        {
            ConnectionPath = ConnectionPath,
            Filters = new Dictionary<string, (string, int)>(),
            Params = new List<DataParam>()
        });
}