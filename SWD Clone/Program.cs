

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<MongoDbContext>(sp =>
{
    string connectionString = "mongodb://localhost:27017";
    string databaseName = "SWDClone"; // Change this to your new database name
    return new MongoDbContext(connectionString, databaseName);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
string connectionString = "mongodb://localhost:27017";
string databaseName = "SWDClone"; // Change this to your new database name

// Create an instance of the ApplicationDbContext
var dbContext = new MongoDbContext(connectionString, databaseName);
// Optionally, seed the database with initial data
dbContext.SeedData();

app.Run();
