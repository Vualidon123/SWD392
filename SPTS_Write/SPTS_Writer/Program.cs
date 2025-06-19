using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using SPTS_Writer.Data;
using SPTS_Writer.Models;
using SPTS_Writer.Service;

// Removed the line causing the error as GuidRepresentationMode is no longer supported in newer versions of MongoDB.Driver
// BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;

// Instead, configure the GuidSerializer directly
BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));

var builder = WebApplication.CreateBuilder(args);
// Add Repository to the container.
builder.Services.AddSingleton<IRepository<Test>, Repository<Test>>();
builder.Services.AddSingleton<IRepository<School>, Repository<School>>();
// Add services to the container.
builder.Services.AddSingleton<TestService>(); // Assuming you have a TestService that uses IRepository<Test>
builder.Services.AddSingleton<SchoolService>();




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
