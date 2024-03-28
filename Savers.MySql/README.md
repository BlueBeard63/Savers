This is the MySql Saver. Example of how to use to generate the DDL is below:

```csharp
new MySqlSaver<Map>()
    .Activate(ConnectionString);
```

To use the Sql generation, it you would need to either create a new ``MySqlSaver`` or use an already existing saver. After that you can call the ``StartQuery`` method which will allow you to easily create the Sql quickly. This can be seen below, where we start the query to create the selection statement.

```csharp
new MySqlSaver<TestClass>().StartQuery().Select("TestClassString")
```

After choosing which statement to use, you can then add on a number of clauses such as the ``Where``, ``OrderBy`` or other clauses. These will automatically get ordered into the correct order needed for the Sql. You can see examples for the different clauses below.

## Statements List:
### Selection Statement:
Select | All Parameters:
```csharp
new MySqlSaver<TestClass>().StartQuery().Select()
```

Select | With Parameters:
```csharp
new MySqlSaver<TestClass>().StartQuery().Select("TestClassString")
```

---

### Count Statement:
```csharp
new MySqlSaver<TestClass>().StartQuery().Count()
```

---

### Sum Statement:
```csharp
new MySqlSaver<TestClass>().StartQuery().Sum("TestValue")
```

---

### Average Statement:
Average | One Field:
```csharp
new MySqlSaver<TestClass>().StartQuery().Average("TestValue")
```

Average | Multiple Fields:
```csharp
new MySqlSaver<TestClass>().StartQuery().Average("TestValue", "TestValue2").Finalise()
```

---

### Insert Statement:
```csharp
new MySqlSaver<TestClass>().StartQuery().Insert(new TestClass { TestClassString = "TestString" })
```

---

### Update Statement:
Update | One Field:
```csharp
new MySqlSaver<TestClass>().StartQuery().Update(("TestClassString", "Example")).Finalise()
```

Update | Multiple Fields:
```csharp
new MySqlSaver<TestClass>().StartQuery().Update(("TestClassString", "Example"), ("TestExampleInt", 5)).Finalise()
```

## Clauses:
### Where Clause:
Where Start:
```csharp
new MySqlSaver<TestClass>()
    .StartQuery()
    .Count()
    .WhereStarts(("TestValueString", "abc"))
    .Finalise()
```

Where End:
```csharp
new MySqlSaver<TestClass>()
    .StartQuery()
    .Count()
    .WhereEnds(("TestValueString", "abc"))
    .Finalise()
```

Where Contains:
```csharp
new MySqlSaver<TestClass>()
    .StartQuery()
    .Count()
    .WhereContains(("TestValueString", "abc"))
    .Finalise()
```

Where (Normal):
```csharp
new MySqlSaver<TestClass>()
    .StartQuery()
    .Count()
    .Where(("TestValueString", "abc"))
    .Finalise()
```

---

### Order By Clause:
Order By | All Ordering By Ascending:
```csharp
new MySqlSaver<TestClass>()
    .StartQuery()
    .Count()
    .OrderBy(Order.Ascending, "TestValue", "TestValue2")
    .Finalise()
```

Order By | All Ordering By Decending:
```csharp
new MySqlSaver<TestClass>()
    .StartQuery()
    .Count()
    .OrderBy(Order.Descending, "TestValue", "TestValue2")
    .Finalise()
```

Order By | Decending and Ascending:
```csharp
new MySqlSaver<TestClass>()
    .StartQuery()
    .Count()
    .OrderBy(("TestValue", Order.Ascending), ("TestValue2", Order.Descending))
    .Finalise()
```

---

### Limit Clause:
```csharp
 new MySqlSaver<TestClass>()
    .StartQuery()
    .Count()
    .Limit(10)
    .Finalise()
```
