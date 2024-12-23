using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace chatapp_blazor.Models;

public class DataDbContext(DbContextOptions<DataDbContext> options)
    : IdentityDbContext<User>(options)
{
    public DbSet<Message> Messages => Set<Message>();
}