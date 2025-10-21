using HR_Management.Domain;

namespace HR_Management.UnitTests.Common.SeedData;

public static class LeaveTypeSeedData
{
    public static List<LeaveType> GetList()
    {
        return
        [
            new LeaveType { Id = 1, DefaultDay = 5, Name = "Test1" },
            new LeaveType { Id = 2, DefaultDay = 10, Name = "Test2" },
            new LeaveType { Id = 3, DefaultDay = 15, Name = "Test3" }
        ];
    }
}