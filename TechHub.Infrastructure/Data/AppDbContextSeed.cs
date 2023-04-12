using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TechHub.Core.Entities;

namespace TechHub.Infrastructure.Data;

public static class AppDbContextSeed
{
    private const string ADMIN_USERNAME = "TechHubAdmin";

    public static async Task SeedDataAsync(AppDbContext dbContext,
        UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
        IConfiguration configuration)
    {
        if (dbContext.Database.IsSqlServer())
        {
            dbContext.Database.Migrate();
        }

        await SeedAdminUserAsync(userManager, roleManager, configuration);

        await SeedPostAndTagAsync(dbContext, userManager);
    }

    private static async Task SeedAdminUserAsync(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        const string ADMIN_ROLE = "Admin";
        if (await roleManager.FindByNameAsync(ADMIN_ROLE) is null)
        {
            await roleManager.CreateAsync(new IdentityRole(ADMIN_ROLE));
        }

        if (await userManager.FindByNameAsync(ADMIN_USERNAME) is not null)
        {
            return;
        }

        const string ADMIN_DISPLAYNAME = "TechHub Admin";
        string? adminEmail = configuration["TechHubAdminEmail"];
        string? adminPassword = configuration["TechHubAdminPassword"];

        if (string.IsNullOrWhiteSpace(adminEmail))
        {
            throw new InvalidOperationException("The TechHubAdminEmail environment variable is not set.");
        }

        if (string.IsNullOrWhiteSpace(adminPassword))
        {
            throw new InvalidOperationException("The TechHubAdminPassword environment variable is not set.");
        }

        var adminUser = new ApplicationUser
        {
            UserName = ADMIN_USERNAME,
            DisplayName = ADMIN_DISPLAYNAME,
            Email = adminEmail
        };

        await userManager.CreateAsync(adminUser, adminPassword);
        adminUser = await userManager.Users
            .SingleAsync(u => u.UserName == ADMIN_USERNAME);
        await userManager.AddToRoleAsync(adminUser, ADMIN_ROLE);
    }

    private static async Task SeedPostAndTagAsync(AppDbContext dbContext,
        UserManager<ApplicationUser> userManager)
    {
        if (await dbContext.Posts.AnyAsync())
        {
            return;
        }

        Tag[] tags = CreateTags();
        dbContext.Tags.AddRange(tags);

        ApplicationUser admin = await userManager.Users
            .SingleAsync(u => u.UserName == ADMIN_USERNAME);

        Post[] posts = CreatePosts(admin.Id, tags);
        dbContext.Posts.AddRange(posts);

        await dbContext.SaveChangesAsync(default);
    }

    private static Post[] CreatePosts(string adminId, Tag[] tags)
    {
        return new Post[]
        {
            new Post
            {
                Title = "بکاپ واتساپ ؛ نحوه پشتبیان گیری از چت، فایل ها و تمام داده های واتساپ در اندروید",
                Slug = "how-to-backup-whatsapp-on-android-transfer-to-another-phone",
                DatePublished = DateTimeOffset.UtcNow,
                AuthorId = adminId,
                Content = "<p>چت‌های واتساپ فقط در حافظه گوشی ذخیره شده و اگر پاک شوند دیگر نمی‌توان به آن‌ دسترسی داشت. در این مطلب روش‌های بکاپ گرفتن از واتساپ را شرح خواهیم داد</p>",
                Tags = new Tag[]{ tags[0] },
                ThumbnailPath = "images/thumbnails/adem-ay-zs-41Br0WhQ-unsplash.jpg"
            },
            new Post
            {
                Title = "روش‌های ارتقای کامپیوتر‌های قدیمی برای افزایش سرعت و بهبود عملکرد",
                Slug = "the-five-best-pc-upgrades-to-improve-performance",
                DatePublished = DateTimeOffset.UtcNow,
                AuthorId = adminId,
                Content = "<p>در این مقاله بهترین روش‌های ارتقای کامپیوترها و کیس‌های قدیمی را معرفی کرده و توضیح داده‌ایم که چگونه این کار باعث افزایش و بهبود کارایی آن‌ها می‌شود.</p>",
                Tags = new Tag[] { tags[1] },
                ThumbnailPath = "images/thumbnails/domenico-loia-hGV2TfOh0ns-unsplash.jpg"
            }
        };
    }

    private static Tag[] CreateTags()
    {
        return new Tag[]
        {
            new Tag { Name = "نرم افزار و اپلیکیشن", Slug = "software-application" },
            new Tag { Name = "لپ تاپ", Slug = "laptop" }
        };
    }
}
