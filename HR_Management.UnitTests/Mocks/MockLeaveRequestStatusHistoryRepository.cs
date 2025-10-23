using HR_Management.Application.Contracts.Persistence;
using HR_Management.UnitTests.Common.SeedData;
using Moq;

namespace HR_Management.UnitTests.Mocks;

public class MockLeaveRequestStatusHistoryRepository
{
    public static Mock<ILeaveRequestStatusHistoryRepository> HasAnyLeaveHistoryWithStatusIdAsync()
    {
        var LeaveRequestStatusHistories = LeaveRequestStatusHistorySeedData.GetList();
        var mockRepo = new Mock<ILeaveRequestStatusHistoryRepository>();

        mockRepo.Setup(la => la.HasAnyLeaveHistoryWithStatusIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int leaveStatusId) =>
                LeaveRequestStatusHistories.Any(la => la.LeaveStatusId == leaveStatusId));

        return mockRepo;
    }
}