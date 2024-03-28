using Savers.Shared.Savers;
using Savers.Shared.Savers.Sql;
using Savers.Shared.Savers.Sql.Interfaces;
using Savers.SqlLite.Statements;

namespace Savers.SqlLite
{
    public class SqlLiteSaver<T> : ISqlSaver<T>
    {
        public SaverType SaverType => SaverType.Sql;
        private string Connection { get; set; }
        
        public void Activate(string location)
        {
            Connection = location;
            
            var table = GenerateTableDDL();
            // Generate Executor
            // Execute Table DDL
        }

        public void Deactivate()
        {
        }

        public string GenerateTableDDL() => 
            TableGeneration.Generate(typeof(T));

        public IStatement<T> StartQuery() =>
            new SqlLiteStatements<T>(new SqlData(Connection));
    }
}