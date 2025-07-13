using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using SPTS_Writer.Eventbus;

// Removed the line causing the error as GuidRepresentationMode is no longer supported in newer versions of MongoDB.Driver
// BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;

// Instead, configure the GuidSerializer directly
BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));

var builder = WebApplication.CreateBuilder(args);

//_ = builder.Host.UseSerilog(
//    (hostContext, loggerConfiguration) =>
//        _ = loggerConfiguration.ReadFrom.Configuration(builder.Configuration)
//);

// Add services to the container.
builder.Services.AddDependencyInjection(builder.Configuration);
builder.Services.AddAuthorizationPolicies();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();
