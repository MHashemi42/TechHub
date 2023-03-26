using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechHub.Core.Repositories;
using TechHub.Core.Services;
using TechHub.Infrastructure.Data;
using TechHub.Infrastructure.Data.Repositories;
using TechHub.Infrastructure.Services;

namespace TechHub.Infrastructure;

public static class Dependencies
{
    public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(c =>
        {
            c.UseSqlServer(configuration.GetConnectionString("TechHubConnection"));
        });

        services.Configure<IdentityOptions>(options =>
        {
            options.User.RequireUniqueEmail = true;
        });

        AddRepositories(services);
        AddServices(services);

        services.AddAutoMapper(typeof(Dependencies).Assembly);
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IPostService, PostService>();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
    }
}
