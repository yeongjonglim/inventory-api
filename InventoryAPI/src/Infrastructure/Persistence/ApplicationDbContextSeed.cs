using InventoryAPI.Domain.Entities;
using InventoryAPI.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using InventoryAPI.Domain.Enums;

namespace InventoryAPI.Infrastructure.Persistence;

public static class ApplicationDbContextSeed
{
    public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        var administratorRole = new IdentityRole("Administrator");

        if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await roleManager.CreateAsync(administratorRole);
        }

        var administrator = new ApplicationUser
        {
            UserName = "administrator@localhost", Email = "administrator@localhost"
        };

        if (userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await userManager.CreateAsync(administrator, "Administrator1!");
            await userManager.AddToRolesAsync(administrator, new[] {administratorRole.Name});
        }
    }

    public static async Task SeedSampleDataAsync(ApplicationDbContext context)
    {
        // Seed, if necessary
        if (!context.TyrePrices.Any())
        {
            var ct6 = new TyrePattern {Brand = TyreBrand.Viking, Series = "CityTech CT6"};
            var small = new TyreSize {Width = 175, Profile = 70, Diameter = 13};
            var tyrePrices = new List<TyrePrice>
            {
                new()
                {
                    TyreSize = new TyreSize {Width = 155, Profile = 70, Diameter = 12},
                    TyrePattern = ct6,
                    SellingPrice = 130,
                },
                new()
                {
                    TyreSize = new TyreSize {Width = 165, Profile = 60, Diameter = 13},
                    TyrePattern = ct6,
                    SellingPrice = 130,
                },
                new()
                {
                    TyreSize = small,
                    TyrePattern = ct6,
                    SellingPrice = 135,
                },
                new()
                {
                    TyreSize = small,
                    TyrePattern = new TyrePattern {Brand = TyreBrand.Goodyear, Series = "GT3"},
                    SellingPrice = 135,
                }
            };
            
            await context.TyrePrices.AddRangeAsync(tyrePrices);

            await context.SaveChangesAsync();
        }
    }
}