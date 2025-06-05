using Microsoft.AspNetCore.Identity;

namespace School_Management_System.Helpers
{
    public static class RoleSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            if (roleManager == null)
                throw new InvalidOperationException("RoleManager<IdentityRole> is not registered.");

            if (userManager == null)
                throw new InvalidOperationException("UserManager<IdentityUser> is not registered.");

            // 1. Seed default roles
            string[] roles = { "Admin", "Teacher", "Student", "Parent" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var result = await roleManager.CreateAsync(new IdentityRole(role));
                    if (!result.Succeeded)
                    {
                        throw new Exception($"Failed to create role '{role}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
            }

            // 2. Seed a default Admin user (optional but highly recommended)
            var defaultAdminEmail = "admin@school.com";
            var defaultAdminUser = await userManager.FindByEmailAsync(defaultAdminEmail);

            if (defaultAdminUser == null)
            {
                var adminUser = new IdentityUser
                {
                    UserName = defaultAdminEmail,
                    Email = defaultAdminEmail,
                    EmailConfirmed = true
                };

                var createUserResult = await userManager.CreateAsync(adminUser, "Admin@123"); // Use a strong password
                if (!createUserResult.Succeeded)
                {
                    throw new Exception($"Failed to create default admin user: {string.Join(", ", createUserResult.Errors.Select(e => e.Description))}");
                }

                var addRoleResult = await userManager.AddToRoleAsync(adminUser, "Admin");
                if (!addRoleResult.Succeeded)
                {
                    throw new Exception($"Failed to assign Admin role to default admin user: {string.Join(", ", addRoleResult.Errors.Select(e => e.Description))}");
                }
            }
        }
    }
}
