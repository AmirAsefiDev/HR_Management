using HR_Management.Domain;

namespace HR_Management.UnitTests.Common.SeedData;

public static class LeaveRequestSeedData
{
    public static List<LeaveRequest> GetList()
    {
        return
        [
            new LeaveRequest { Id = 1, LeaveTypeId = 1, LeaveStatusId = 1, RequestComments = "Test1" },
            new LeaveRequest { Id = 2, LeaveTypeId = 1, LeaveStatusId = 1, RequestComments = "Test2" },
            new LeaveRequest { Id = 3, LeaveTypeId = 1, LeaveStatusId = 1, RequestComments = "Test3" }
        ];
    }
}