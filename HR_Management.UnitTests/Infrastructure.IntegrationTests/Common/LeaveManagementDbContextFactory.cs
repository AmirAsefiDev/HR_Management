using HR_Management.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HR_Management.UnitTests.Infrastructure.IntegrationTests.Common;

public static class TestDbContextFactory
{
    public static LeaveManagementDbContext Create(Action<LeaveManagementDbContext>? seedAction = null)
    {
        var options = new DbContextOptionsBuilder<LeaveManagementDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Creates unique Test Database in memory
            .Options;

        var context = new LeaveManagementDbContext(options);
        context.Database.EnsureCreated();

        // Seed data if provided
        seedAction?.Invoke(context);

        context.SaveChanges();
        return context;
    }

    public static void Destroy(LeaveManagementDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}