using Microsoft.Extensions.Options;
using SPTS_Writer.Data;
using SPTS_Writer.Data.Abstraction;
using SPTS_Writer.Entities;
using SPTS_Writer.Models.Config;
using SPTS_Writer.Services;

public static class DependencyInjection
{
    public static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        // Bind MongoDbConfig from appsettings
        services.RegisterMongoDbContext(configuration);
        services.RegisterRepositories();
        services.RegisterAuthentication();
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
        services.AddScoped<TestService>();
        services.AddScoped<SchoolService>();
        services.AddScoped<HistoryService>();
        services.AddScoped<QuestionService>();
    }
}
