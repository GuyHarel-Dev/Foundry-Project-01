using WebApi.Extensions;
using WebApi.MiddleWares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add this project services
builder.Services.AddOpenApi();
builder.Services.AddSwaggerDocumentation();

builder.Services.AddAzFoundryService();
builder.Services.AddAzFoundryClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerDocumentation();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseMiddleware<HttpErrorHandlingMiddleware>();

app.Run();
