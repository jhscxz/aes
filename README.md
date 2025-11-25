# aes

<img src="./grb-zg-og.png" align="left" alt="Grad Zagreb" width="60">

Alat za evidenciju, kontrolu, obradu i plaćanje računa režijskih troškova za stanove u imovini Grada Zagreba.

## Tehnologije
- .NET 8 (ASP.NET Core MVC + Identity)
- Entity Framework Core (SQL Server)
- Serilog za logiranje (Console + MSSQL sink)
- Application Insights telemetrija
- EPPlus za rad s Excel datotekama

## Preduvjeti
- .NET SDK 8.0
- SQL Server (lokalni ili u Dockeru). Primjer pokretanja u Dockeru na portu 1401:
  ```bash
  docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=sdJdBwZ8q7rAxTg6fVGP437sZtYz8D" -p 1401:1433 -d mcr.microsoft.com/mssql/server:2022-latest
  ```
- (Preporuka) postaviti vlastiti connection string preko `dotnet user-secrets` ili varijabli okruženja.

## Brzi start
1. `dotnet restore`
2. Postavi connection string (ako ne koristiš zadani iz `appsettings.json`):
   ```bash
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost,1401;Database=aes;User=SA;Password=<lozinka>;;TrustServerCertificate=true;MultipleActiveResultSets=True;"
   ```
3. Primijeni migracije:
   ```bash
   dotnet ef database update
   ```
4. Pokreni aplikaciju:
   ```bash
   dotnet run
   ```
5. Aplikacija će biti dostupna na `https://localhost:5001` ili `http://localhost:5000`.

## Napomene
- Registracija novih korisnika je onemogućena (redirect na prijavu); za dodavanje korisnika omogući registracijske rute u `Startup.Configure` ili dodaj korisnika ručno u Identity tablice.
- Logovi se zapisuju u tablicu `EventLogging.Logs` (Serilog MSSQL sink) i na konzolu.
- Uploadi se spremaju u `UploadedFiles/*` i `wwwroot/Uploaded/`.

## Korisna struktura koda
- `Controllers/` – MVC kontroleri za stanove, predmete i račune
- `Data/ApplicationDbContext.cs` – EF Core context
- `Services/`, `Repositories/`, `UnitOfWork/` – poslovna logika i pristup podacima
- `Migrations/` – EF Core migracije
- `wwwroot/` – statički resursi (JS/CSS)
