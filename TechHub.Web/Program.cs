using Microsoft.AspNetCore.Identity;
using Serilog;
using TechHub.Infrastructure.Data;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Host.UseSerilog((context, configuration) => 
    configuration.ReadFrom.Configuration(context.Configuration));

TechHub.Infrastructure.Dependencies.ConfigureServices(builder.Configuration, builder.Services);

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

WebApplication app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    IServiceProvider scopedProvider = scope.ServiceProvider;
    try
    {
        UserManager<ApplicationUser> userManager = scopedProvider
            .GetRequiredService<UserManager<ApplicationUser>>();

        RoleManager<IdentityRole> roleManager = scopedProvider
            .GetRequiredService<RoleManager<IdentityRole>>();

        AppDbContext dbContext = scopedProvider.GetRequiredService<AppDbContext>();
        await AppDbContextSeed.SeedDataAsync(dbContext, userManager, roleManager);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");    
    app.UseHsts();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
