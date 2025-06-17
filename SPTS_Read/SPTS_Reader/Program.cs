var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<MongoDbContext>(sp =>
{
    string connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");//"mongodb://localhost:<random-ass-number-that-not-write-one>";
    string databaseName = "SPTS_Reader"; // Change this to your new database name
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

app.Run();
