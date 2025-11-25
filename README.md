# aes

<img src="./grb-zg-og.png" align="left" alt="Grad Zagreb" width="60">

Alat za evidenciju, kontrolu, obradu i plaćanje računa režijskih troškova za stanove u imovini Grada Zagreba.

## Arhitektura
- ASP.NET Core 8 MVC + Identity (login, autorizacija, zabrana registracije rute).
- EF Core (SQL Server) za pristup bazi; sloj repozitorija + Unit of Work.
- Servisi za domenske poslove (racuni, stanovi, ODS, dopisi, upload/parsiranje).
- Serilog za logove i Application Insights za telemetriju.

## Tehnologije
- .NET 8, C# 11
- Entity Framework Core, Identity
- Serilog
- EPPlus (rad s Excelom)

## Pokretanje ukratko
1. Postaviti `DefaultConnection` u `appsettings.json`.
2. `dotnet restore`
3. `dotnet ef database update`
4. `dotnet run` (startna ruta: `Stanovi/Index`)
