using Savers.Shared.Savers.Sql;
using Savers.Shared.Savers.Sql.Interfaces;
using Savers.SqlLite.Clauses;

namespace Savers.SqlLite.Statements
{
    internal class SqlLiteStatements<T>(SqlData sql_data) : IStatement<T>
    {
        public SqlData Data { get; set; } = sql_data;

        public IClause<T> Select(params string[] columns)
        {
            return new SqlLiteClauses<T>(Data);
        }

        public IClause<T> Count()
        {
            return new SqlLiteClauses<T>(Data);
        }

        public IClause<T> Sum(string column)
        {
            return new SqlLiteClauses<T>(Data);
        }

        public IClause<T> Average(params string[] columns)
        {
            return new SqlLiteClauses<T>(Data);
        }

        public IClause<T> Update(params (string, object)[] values)
        {
            return new SqlLiteClauses<T>(Data);
        }

        public IExecutor Insert(T obj)
        {
            return new SqlLiteExecutor(Data);
        }
    }
}