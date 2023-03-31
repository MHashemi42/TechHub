using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TechHub.Core.Entities;

namespace TechHub.Infrastructure.Data;

public static class AppDbContextSeed
{
    private const string _adminUserName = "TechHubAdmin";

    public static async Task SeedDataAsync(AppDbContext dbContext,
        UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        if (dbContext.Database.IsSqlServer())
        {
            dbContext.Database.Migrate();
        }

        await SeedIdentityAsync(userManager, roleManager);

        await SeedPostAndTagAsync(dbContext, userManager, _adminUserName);
    }
    private static async Task SeedIdentityAsync(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {

        if (await roleManager.FindByNameAsync("Admin") is null)
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        if (await userManager.FindByNameAsync(_adminUserName) is not null)
        {
            return;
        }

        string adminDisplayName = "TechHub";
        string adminEmail = "techhubadmin@gamil.com";
        var adminUser = new ApplicationUser
        {
            UserName = _adminUserName,
            DisplayName = adminDisplayName,
            Email = adminEmail
        };

        await userManager.CreateAsync(adminUser, "Pass@word1");
        adminUser = await userManager.FindByNameAsync(_adminUserName);
        await userManager.AddToRoleAsync(adminUser!, "Admin");
    }

    private static async Task SeedPostAndTagAsync(AppDbContext dbContext,
        UserManager<ApplicationUser> userManager, string adminUserName)
    {
        if (await dbContext.Posts.AnyAsync())
        {
            return;
        }

        var tags = new Tag[]
        {
                new Tag { Name = "نرم افزار و اپلیکیشن", Slug = "software-application" },
                new Tag { Name = "لپ تاپ", Slug = "laptop" }
        };

        dbContext.Tags.AddRange(tags);
        ApplicationUser? admin = await userManager.FindByNameAsync(adminUserName);
        var posts = new Post[]
        {
            new Post
            {
                Title = "بکاپ واتساپ ؛ نحوه پشتبیان گیری از چت، فایل ها و تمام داده های واتساپ در اندروید",
                Slug = "how-to-backup-whatsapp-on-android-transfer-to-another-phone",
                DatePublished = DateTimeOffset.UtcNow,
                AuthorId = admin!.Id,
                Content = "<p>چت‌های واتساپ فقط در حافظه گوشی ذخیره شده و اگر پاک شوند دیگر نمی‌توان به آن‌ دسترسی داشت. در این مطلب روش‌های بکاپ گرفتن از واتساپ را شرح خواهیم داد</p>",
                Tags = new Tag[]{ tags[0] },
                ThumbnailPath = "images/thumbnails/adem-ay-zs-41Br0WhQ-unsplash.jpg"
            },
            new Post
            {
                Title = "روش‌های ارتقای کامپیوتر‌های قدیمی برای افزایش سرعت و بهبود عملکرد",
                Slug = "the-five-best-pc-upgrades-to-improve-performance",
                DatePublished = DateTimeOffset.UtcNow,
                AuthorId = admin!.Id,
                Content = "<p>در این مقاله بهترین روش‌های ارتقای کامپیوترها و کیس‌های قدیمی را معرفی کرده و توضیح داده‌ایم که چگونه این کار باعث افزایش و بهبود کارایی آن‌ها می‌شود.</p>",
                Tags = new Tag[] { tags[1] },
                ThumbnailPath = "images/thumbnails/domenico-loia-hGV2TfOh0ns-unsplash.jpg"
            }
        };

        dbContext.Posts.AddRange(posts);
        await dbContext.SaveChangesAsync(default);
    }
}
