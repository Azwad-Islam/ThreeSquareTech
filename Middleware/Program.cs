using MiddlewareExample.Middlewares;  // Import middleware namespace
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging();  // Ensure logging is enabled

var app = builder.Build();

app.UseResponseTimeLogger();  // Register the response time logging middleware

app.MapGet("/", () => "Hello, World!");  // Simple test endpoint

app.Run();
