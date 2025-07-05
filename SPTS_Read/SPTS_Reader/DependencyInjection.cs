using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SPTS_Reader.Data;
using SPTS_Reader.Data.Abstraction;

//using SPTS_Reader.Data.Abstraction;
using SPTS_Reader.Entities;
using SPTS_Reader.Models.Config;
using SPTS_Reader.Services;
using SPTS_Reader.Services.Abstraction;

using System.Text;

public static class DependencyInjection
{
    public static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwagger();
        services.AddCors();
        // Bind MongoDbConfig from appsettings
        services.RegisterMongoDbContext(configuration);
        services.RegisterRepositories();
        services.RegisterAuthentication();
        services.AddJwtAuthentication(configuration);
    }

    private static void RegisterMongoDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbConfig>(configuration.GetSection("MongoDbConfig"));
        services.AddScoped<MongoDbContext>(sp =>
        {
            var config = sp.GetRequiredService<IOptions<MongoDbConfig>>().Value;
            return new MongoDbContext(config.ConnectionString, config.DatabaseName);
        });
    }

    private static void RegisterRepositories(this IServiceCollection services)
    {
        //services.AddScoped<IRepository<User>, Repository<User>>();
        //services.AddScoped<UserRepository>();
        //services.AddScoped<IRepository<Answer>, Repository<Answer>>();
        services.AddScoped<IRepository<Test>, Repository<Test>>();
        services.AddScoped<IRepository<History>, Repository<History>>();
        //services.AddScoped<IRepository<School>, Repository<School>>();
        services.AddScoped<ISpecializationsRecommendationRepository, SpecializationsRecommendationRepository>();
        services.AddScoped<ISchoolRepository, SchoolRepository>();
    }

    private static void RegisterAuthentication(this IServiceCollection services)
    {
        //services.AddScoped<Authen>();
        services.AddScoped<TestService>();
        services.AddScoped<HistoryService>();
        //services.AddScoped<SchoolService>();
        services.AddScoped<ISpecializationsRecommendationService, SpecializationsRecommendationService>();
        services.AddScoped<UserService>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<ITestService, TestService>();
    }

    private static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(o =>
        {
            o.SwaggerDoc(
                "v1",
                new OpenApiInfo { Title = "SPTS_Reader", Version = "v1" }
            );
            // add xml comments for api
            //var xmlFile = $"{AssemblyReference.Assembly.GetName().Name}.xml";
            //var apiXmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //o.IncludeXmlComments(apiXmlPath);
            // var repoXmlFile = "NET1814_MilkShop.Repositories.xml";
            // var repoXmlPath = Path.Combine(AppContext.BaseDirectory, repoXmlFile);
            // o.IncludeXmlComments(repoXmlPath);
            o.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                }
            );
            o.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                }
            );
        });
    }

    private static void AddCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });
    }

    private static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthentication()
            .AddJwtBearer(
                "Access",
                o =>
                {
                    o.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidIssuer = configuration["Jwt:Issuer"],
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(configuration["Jwt:AccessTokenKey"]!)
                            ),
                            ClockSkew = TimeSpan.FromMinutes(0)
                        };
                }
            )
            .AddJwtBearer(
                "Refresh",
                o =>
                {
                    o.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidIssuer = configuration["Jwt:Issuer"],
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(configuration["Jwt:RefreshTokenKey"]!)
                            ),
                            ClockSkew = TimeSpan.FromMinutes(0)
                        };
                }
            );
    }
}
