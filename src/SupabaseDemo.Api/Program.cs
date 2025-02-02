using System.Security.Claims;
using DotNetEnv;
using Supabase;
using SupabaseDemo.Api.Contracts;
using SupabaseDemo.Api.Extensions;
using SupabaseDemo.Api.Models;
using Client = Supabase.Client;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

#region Configure Supabase

var supabaseUrl = Environment.GetEnvironmentVariable("SUPABASE_URL")!;
var supabaseKey = Environment.GetEnvironmentVariable("SUPABASE_KEY");

builder.Services.AddScoped<Client>(_ => new Client(
    supabaseUrl,
    supabaseKey,
    new SupabaseOptions
    {
        AutoRefreshToken = true,
        AutoConnectRealtime = true
    }));

#endregion

#region Configure JWT

builder.Services.AddJwtBearerAuthentication(builder.Configuration);

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();

#region Endpoints

app.MapGet("/newsletters/{id:long}", async (long id, Client client) =>
{
    var response = await client.From<Newsletter>()
        .Where(n => n.Id == id)
        .Get();
    
    var newsletter = response.Models.FirstOrDefault();

    if (newsletter is null)
    {
        return Results.NotFound();
    }

    var newsletterResponse = new NewsletterResponse
    {
        Id = newsletter.Id,
        Name = newsletter.Name,
        Description = newsletter.Description,
        ReadTime = newsletter.ReadTime,
        CreatedAt = newsletter.CreatedAt,
        ImageUrl = client.Storage.From("cover-images")
            .GetPublicUrl($"newsletter-{id}.png")
    };
    
    return Results.Ok(newsletterResponse);
});

app.MapDelete("/newsletters/{id:long}", async (long id, Client client) =>
{
    await client.From<Newsletter>()
        .Where(n => n.Id == id)
        .Delete();
    
    await client.Storage.From("cover-images")
        .Remove([$"newsletter-{id}.png"]);

    return Results.NoContent();
});

app.MapGet("/users", (ClaimsPrincipal principal) =>
{
    var claims = principal.Claims.ToDictionary(c => c.Type, c => c.Value);
    return Results.Ok(claims);
}).RequireAuthorization();

#endregion

app.MapControllers();
app.UseHttpsRedirection();

app.Run();