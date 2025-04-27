using FormSubmissionApi.Models;
using Microsoft.EntityFrameworkCore;
using FormSubmissionApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

//services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy => policy.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

//Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//Build the app
var app = builder.Build();

//middleware and configure the HTTP request pipeline
app.UseMiddleware<CodeCheckMiddleware>(); // Custom Middleware FIRST

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngularApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
