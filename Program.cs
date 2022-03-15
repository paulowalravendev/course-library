using Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSqlServer<CourseLibraryContext>(builder.Configuration.GetConnectionString("DefaultConnection"));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();