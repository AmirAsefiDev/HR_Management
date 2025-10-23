using HR_Management.Domain;

namespace HR_Management.UnitTests.Common.SeedData;

public class LeaveRequestStatusHistorySeedData
{
    public static List<LeaveRequestStatusHistory> GetList()
    {
        return
        [
            new LeaveRequestStatusHistory { Id = 1, LeaveRequestId = 1, LeaveStatusId = 1, Comment = "Test1" },
            new LeaveRequestStatusHistory { Id = 2, LeaveRequestId = 1, LeaveStatusId = 1, Comment = "Test2" },
            new LeaveRequestStatusHistory { Id = 3, LeaveRequestId = 1, LeaveStatusId = 1, Comment = "Test3" }
        ];
    }
}