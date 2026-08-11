using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using WorkTracker.Application.Interfaces;
using WorkTracker.Application.Services;
using WorkTracker.Infrastructure.Data;
using WorkTracker.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Blazor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// MudBlazor
builder.Services.AddMudServices();

// Database (SQLite)
var dbPath = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
    "WorkTracker", "worktracker.db");
Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite($"Data Source={dbPath}"));

// App services
builder.Services.AddScoped<WorkDayRepository>();
builder.Services.AddScoped<IWorkDayService, WorkDayService>();

var app = builder.Build();

// Auto-migrate on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<WorkTracker.Web.Components.App>()
    .AddInteractiveServerRenderMode();

app.Run();
