# SolarSystems
random line

Útmutató a projekt teszteléséhez



Projekt letöltése GitHub-ról:

  Visual Studio -> clone repository -> https://github.com/aronburjan/SolarSystems.git

SQL Server Express/SSMS letöltése:

  SQL Server Express letöltése: https://www.microsoft.com/en-us/sql-server/sql-server-downloads
  
  Microsoft SQL Server Management Studio letöltése: https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16


Dependencies telepítése:
Visual Studio -> Tools -> NuGet Package Manager -> Manage NuGet Packages For Solution

  Microsoft.EntityFrameworkCore 7.0.4
  
  Microsoft.EntityFrameworkCore.Design 7.0.4
  
  Microsoft.EntityFrameworkCore.SqlServer 7.0.4
  
  Microsoft.EntityFrameworkCore.Tool 6.0.15
  
  Microsoft.VisualStudio.Web.CodeGeneration 6.0.13
  
  Swashbuckle.AspNetCore 6.5.0



SQL Server konfigurálása:

  Visual Studio -> View -> SQL Server Object Explorer -> Add SQL Server -> Local -> DESKTOP-QM7PL5T\SQLEXPRESS (a te gépeden más lesz) -> Connect
  
  Microsoft SQL Server Management Studio -> Server name: DESKTOP-QM7PL5T\SQLEXPRESS (a te gépeden más lesz) -> Connect
  
  
  
Adatbázis connection string konfigurálása:

  Visual Studio -> apsettings.json -> első sorba illeszd be: "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS; Data Source=.\\SQLExpress; Initial Catalog=SolarSystemsDb;Integrated Security=True;Pooling=False;Database=master;Trusted_Connection=True;TrustServerCertificate=True"
  },
  
  
  
Adatbázis migráció:

  Visual Studio -> NuGet Package Manager -> Package Manager Console 
  
  PM> dotnet ef migrations add InitialMigration
  
  PM> dotnet ef database update
  
  Ha itt esetleg a build során hiba van, törölni kell a Migrations mappa tartalmát a Solution Explorerben.
  
  
  
Indítsd el a projektet. Ha minden jól meg, meg fog nyitni egy Console ablakot, illetve böngészőben a Swagger UI-t. Itt már ki is lehet próbálni az API hívásokat.
  
  
  
XAMPP konfigurálása:

  letöltés: https://www.apachefriends.org/
  
  XAMPP -> explorer -> htdocs mappába másold be a SolarSystems projektből a Website mappát -> indítsd el az apache-t -> admin
  
  http://localhost/Websites linkre menj fel, indítsd el a LoginPage.html -t
