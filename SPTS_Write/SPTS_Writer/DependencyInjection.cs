﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using SPTS_Writer.Data;
using SPTS_Writer.Data.Abstraction;
using SPTS_Writer.Entities;
using SPTS_Writer.Eventbus;
using SPTS_Writer.Eventbus.Publishers;
using SPTS_Writer.Eventbus.ViewChanges;
using SPTS_Writer.Models.Config;
using SPTS_Writer.Services;
using SPTS_Writer.Services.Abstraction;
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
       //Data Sync Service
        services.AddScoped<TestChangePublish>();
        services.AddScoped<UsersChangePublish>();

        services.AddScoped<TestView>();
        services.AddScoped<UserView>();
        services.AddHostedService<ViewHostServices>();
    }

    private static void RegisterMongoDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbConfig>(configuration.GetSection("MongoDbConfig"));
        services.AddScoped<MongoDbContext>(sp =>
        {
            var config = sp.GetRequiredService<IOptions<MongoDbConfig>>().Value;
            return new MongoDbContext(config.ConnectionString, config.DatabaseName);
        });
        services.AddScoped<IMongoDatabase>(sp =>
        {
            var config = sp.GetRequiredService<IOptions<MongoDbConfig>>().Value;
            var client = new MongoDB.Driver.MongoClient(config.ConnectionString);
            return client.GetDatabase(config.DatabaseName);
        });
    }

    private static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRepository<User>, Repository<User>>();
        services.AddScoped<UserRepository>();
        services.AddScoped<IRepository<Answer>, Repository<Answer>>();
        services.AddScoped<IRepository<Test>, Repository<Test>>();
        services.AddScoped<IRepository<School>, Repository<School>>();
        services.AddScoped<IRepository<History>, Repository<History>>();
        services.AddScoped<IRepository<Question>, Repository<Question>>();
    }

    private static void RegisterAuthentication(this IServiceCollection services)
    {
        services.AddScoped<Authen>();
        services.AddScoped<ITestService,TestService>();
        services.AddScoped<SchoolService>();
        services.AddScoped<HistoryService>();
        services.AddScoped<QuestionService>();
        services.AddScoped<UserService>();
        
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IQuestionService, QuestionService>();
       

        services.AddScoped<IQuestionService,QuestionService>();
        services.AddScoped<IUserService,UserService>();

		services.AddScoped<INotificationRepository, NotificationRepository>();
		services.AddScoped<INotificationService, NotificationService>();


	}

	private static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(o =>
        {
            o.SwaggerDoc(
                "v1",
                new OpenApiInfo { Title = "SPTS_Writer", Version = "v1" }
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
            .AddAuthentication(options =>
			{
				options.DefaultScheme = "Access";            // ✅ Default for [Authorize]
				options.DefaultChallengeScheme = "Access";   // ✅ Default challenge scheme
			})
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