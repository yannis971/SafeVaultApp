
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class SafeVaultDbContext : IdentityDbContext<IdentityUser>
{
    public SafeVaultDbContext(DbContextOptions<SafeVaultDbContext> options)
        : base(options)
    {
    }
}
