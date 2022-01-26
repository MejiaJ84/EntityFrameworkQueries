# EntityFrameworkQueries

## Getting Started
- VS 2022
- .NET 6
- [AP Database installed](create_ap.sql)

You may need to change the DB connection string located in the AP context class
By default it points to mssqllocaldb. If you AP script is not installed on mssqllocaldb, update the string
``` csharp
optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AP");
```
