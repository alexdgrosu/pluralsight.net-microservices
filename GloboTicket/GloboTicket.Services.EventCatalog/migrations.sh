dotnet add package Microsoft.EntityFrameworkCore.SqlServer

dotnet tool install dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design

dotnet dotnet-ef migrations add InitiaCreate -o Migrations/
dotnet dotnet-ef database update
