using HR_Management.Domain;

namespace HR_Management.UnitTests.Common.SeedData;

public static class LeaveAllocationSeedData
{
    public static List<LeaveAllocation> GetList()
    {
        return
        [
            new LeaveAllocation
            {
                Id = 1, LeaveTypeId = 1, DateCreated = DateTime.UtcNow, Period = DateTime.UtcNow.Year, TotalDays = 10
            },
            new LeaveAllocation
            {
                Id = 2, LeaveTypeId = 2, DateCreated = DateTime.UtcNow, Period = DateTime.UtcNow.Year, TotalDays = 15
            },
            new LeaveAllocation
            {
                Id = 3, LeaveTypeId = 2, DateCreated = DateTime.UtcNow, Period = DateTime.UtcNow.Year, TotalDays = 20
            }
        ];
    }
}