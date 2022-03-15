global using Data;
global using Entities;
global using FastEndpoints;
global using FastEndpoints.Validation;
global using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSqlServer<CourseLibraryContext>(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDoc();

var app = builder.Build();
app.UseFastEndpoints();
app.UseOpenApi();
app.UseSwaggerUi3(s => s.ConfigureDefaults());
app.Run();