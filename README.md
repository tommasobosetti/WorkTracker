# WorkTracker

App personale per la gestione di timbrature, permessi, ferie e smart working.

## Stack

- **Blazor Server** (.NET 8) — frontend + backend in un unico progetto
- **MudBlazor** — component library Material Design
- **Entity Framework Core + SQLite** — database locale, zero configurazione

## Struttura

```
WorkTracker.Domain          → Modelli e enum (nessuna dipendenza esterna)
WorkTracker.Infrastructure  → DbContext, Repository, Migrations
WorkTracker.Application     → Servizi, Interfacce, DTO
WorkTracker.Web             → Componenti Blazor, Layout, Pagine
```

## Prerequisiti

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 (Community) oppure VS Code + estensione C# Dev Kit

## Avvio

```bash
cd WorkTracker.Web
dotnet run
```

Il database SQLite viene creato automaticamente in:
`%LocalAppData%\WorkTracker\worktracker.db`

L'app sarà disponibile su: `https://localhost:5001`

## Pagine

| Route | Descrizione |
|---|---|
| `/` | Oggi — timbra entrata/uscita, gestisci permessi orari |
| `/calendario` | Vista mensile con colori per tipo giornata |
| `/riepilogo` | Statistiche mensili e elenco giorni |
| `/giorno/YYYY-MM-DD` | Dettaglio e modifica di un giorno specifico |
