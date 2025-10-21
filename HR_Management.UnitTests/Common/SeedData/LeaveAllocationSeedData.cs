using HR_Management.Domain;

namespace HR_Management.UnitTests.Common.SeedData;

public static class LeaveAllocationSeedData
{
    public static List<LeaveAllocation> GetList()
    {
        return
        [
            new LeaveAllocation { Id = 1, LeaveTypeId = 1 },
            new LeaveAllocation { Id = 2, LeaveTypeId = 1 },
            new LeaveAllocation { Id = 3, LeaveTypeId = 1 }
        ];
    }
}