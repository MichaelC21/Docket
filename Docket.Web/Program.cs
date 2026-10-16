using Docket.Core.Entities;
using Docket.Persistence.Context;
using Docket.Web.Data;
using Docket.Web.Health;
using DotNetEnv;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

Env.NoClobber().TraversePath().Load();

var builder = WebApplication.CreateBuilder(args);


var connectionString = Environment.GetEnvironmentVariable("DOCKET_CONNECTION_STRING");

if (String.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string is not set");
}

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<DocketDBContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddHealthChecks()
    .AddCheck<DatabaseHealthCheck>("database", tags: new[] { "ready" });

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var myService = services.GetRequiredService<DocketDBContext>();
    myService.Database.Migrate();
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.MapHealthChecks("/healthz", new HealthCheckOptions
{
    Predicate = check => !check.Tags.Contains("ready")
});
app.MapHealthChecks("/readyz", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready")
});

app.MapPost("/api/enqueue", async (EnqueueRequest request, DocketDBContext db) =>
{
    if (String.IsNullOrEmpty(request.handler))
    {
        return Results.BadRequest("handler is required");
    }

    Job job = new Job(request.handler, request.payload ?? "");
    await db.Jobs.AddAsync(job);

    var rowsChanged = await db.SaveChangesAsync();

    return rowsChanged <= 0 ? Results.Problem("Failed to save job") : Results.Ok(job.JobID.ToString());
});

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();
app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();


app.Run();


public partial class Program { }
