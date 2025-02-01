using Supabase;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

#region Configure Supabase

builder.Services.AddScoped<Client>(_ => new Client(
        builder.Configuration["SupabaseUrl"]!,
        builder.Configuration["SupabaseKey"],
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

app.UseHttpsRedirection();

app.Run();