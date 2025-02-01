using Supabase;
using SupabaseDemo.Api.Contracts;
using SupabaseDemo.Api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

#region Configure Supabase

builder.Services.AddScoped<Client>(_ => new Client(
    builder.Configuration["Supabase:Url"]!,
    builder.Configuration["Supabase:Key"],
    new SupabaseOptions
    {
        AutoRefreshToken = true,
        AutoConnectRealtime = true
    }));

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

#region Endpoints

// create newsletter
app.MapPost("/newsletter", async (
    CreateNewsletterRequest request,
    Client client) =>
{
    var newsletter = new Newsletter
    {
        Name = request.Name,
        Description = request.Description,
        ReadTime = request.ReadTime
    };

    var response = await client.From<Newsletter>().Insert(newsletter);

    var newNewsletter = response.Models.First(); 
    
    return Results.Ok(newNewsletter.Id);
});

// get newsletter
app.MapGet("/newsletters/{id:long}", async
    (long id, Client client) =>
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
       CreatedAt = newsletter.CreatedAt
   };
   
   return Results.Ok(newsletterResponse);
});

// delete newsletter
app.MapDelete("/newsletters/{id:long}", async
    (long id, Client client) =>
{
    await client.From<Newsletter>()
        .Where(n => n.Id == id)
        .Delete();

    return Results.NoContent();
});

#endregion

app.UseHttpsRedirection();

app.Run();