## Overview

During my time working with *C#*, I designed and created many different projects that used databases and methods which exported them as model class files. This led me to the idea: what if we could use the models themselves to generate and communicate with the database?

Around the same time, I began developing APIs using JavaScript and databases through **[Sequelize](https://sequelize.org/)**. Since using LINQ in my code was already common, I thought it would be interesting to create something similar in concept.

## Development Approach

While exploring this idea, I discussed it with a few friends as well as my dad. My friends were using **SQLite** for their databases since they wanted to apply the concept in their own projects.

I decided to make the system templatable so that it could work with multiple database systems — I wanted to use **MySQL**, while my friends preferred **SQLite**. At the time, I had recently started using interfaces and thought they would be a good fit for this application.

Additionally, I wanted the system to be easily testable. This would allow me to quickly design and generate features without introducing regressions, ultimately speeding up development by reducing the need for manual testing. I chose **xUnit** for this purpose, with some help from my dad, who recommended it due to his prior experience and its ease of setup.

### System Structure

#### Development of the Statement Interface
A typical database query starts with a statement such as *SELECT*, *COUNT*, or *UPDATE*. Therefore, I wanted to make these methods available and allow users to add clauses for filtering or ordering results.

```cs
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
```


#### Development of the Clause Interface
Typically, you use multiple clauses within a single query — for example, filtering with a WHERE clause and then ordering the results. For instance, you might want to select users by their ID and then order them by name.

```cs
public interface IClause<out T>
{
    public SqlData Data { get; }

    IClause<T> Where(params (string, object)[] filters);
    IClause<T> WhereStarts(params (string, string)[] filters);
    IClause<T> WhereEnds(params (string, string)[] filters);
    IClause<T> WhereContains(params (string, string)[] filters);

    IClause<T> OrderBy(Order order, params string[] columns);
    IClause<T> OrderBy(params (string, Order)[] columns);

    IClause<T> Limit(int count);
        
    IExecutor Finalise();
}
```

### Example Query
```cs
new MySqlSaver<TestClass>()
    .StartQuery()
    .Select()
    .Where(("TestValueString", "abc"))
    .Finalise();
```

This query is applied to the `TestClass` model. It assembles a query that selects all data from `TestClass`, filters the results where `TestValueString` equals `"abc"`, and then finalizes the query for execution.
