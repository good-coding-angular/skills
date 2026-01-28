using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var maxFileSize = 83886080;

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(
        options =>
        {
            options.AddPolicy(
                name: MyAllowSpecificOrigins,
                policy =>
                {
                    Console.WriteLine("Loaded Cors policy");
                    policy.WithOrigins("*");
                    policy.WithMethods("*");
                    policy.WithHeaders("*");
                }
            );
        }
    );
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors(MyAllowSpecificOrigins);

if (app.Environment.IsProduction()) {
    Console.WriteLine("Environment: Production");
}

if (app.Environment.IsDevelopment()) {
    Console.WriteLine("Environment: Development");
}

app.MapControllers();

app.Run();
