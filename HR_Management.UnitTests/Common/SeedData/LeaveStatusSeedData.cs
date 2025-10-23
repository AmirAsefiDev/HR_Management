using HR_Management.Domain;

namespace HR_Management.UnitTests.Common.SeedData;

public static class LeaveStatusSeedData
{
    public static List<LeaveStatus> GetList()
    {
        return
        [
            new LeaveStatus { Id = 1, Name = "Special", Description = "Test", DateCreated = DateTime.UtcNow },
            new LeaveStatus { Id = 2, Name = "Emergency", Description = "Test", DateCreated = DateTime.UtcNow },
            new LeaveStatus { Id = 3, Name = "Force", Description = "Test", DateCreated = DateTime.UtcNow }
        ];
    }
}