using HR_Management.Application.Contracts.Persistence;
using HR_Management.Domain;
using HR_Management.UnitTests.Common.SeedData;
using MockQueryable;
using Moq;

namespace HR_Management.UnitTests.Mocks;

public static class MockLeaveRequestStatusHistoryRepository
{
    public static Mock<ILeaveRequestStatusHistoryRepository> GetLeaveRequestStatusHistoriesListMock()
    {
        var leaveRequestStatusHistories = LeaveRequestStatusHistorySeedData.GetList();
        var mockQueryable = leaveRequestStatusHistories.BuildMock();

        var mockRepo = new Mock<ILeaveRequestStatusHistoryRepository>();

        mockRepo.Setup(lsh => lsh.GetLeaveRequestStatusHistoriesWithDetails())
            .Returns(mockQueryable);

        return mockRepo;
    }

    public static Mock<ILeaveRequestStatusHistoryRepository> GetLeaveRequestStatusHistoriesByLeaveRequestIdMock()
    {
        var leaveRequestStatusHistories = LeaveRequestStatusHistorySeedData.GetList();
        var mockQueryable = leaveRequestStatusHistories.BuildMock();

        var mockRepo = new Mock<ILeaveRequestStatusHistoryRepository>();
        mockRepo.Setup(lsh => lsh.GetLeaveRequestStatusHistoriesByLeaveRequestId(It.IsAny<int>()))
            .Returns(mockQueryable);

        return mockRepo;
    }

    public static Mock<ILeaveRequestStatusHistoryRepository> HasAnyLeaveHistoryWithStatusIdMock()
    {
        var leaveRequestStatusHistories = LeaveRequestStatusHistorySeedData.GetList();
        var mockRepo = new Mock<ILeaveRequestStatusHistoryRepository>();

        mockRepo.Setup(la => la.HasAnyLeaveHistoryWithStatusIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int leaveStatusId) =>
                leaveRequestStatusHistories.Any(lsh => lsh.LeaveStatusId == leaveStatusId));

        return mockRepo;
    }

    public static Mock<ILeaveRequestStatusHistoryRepository> HasAnyLeaveHistoryWithRequestIdMock()
    {
        var leaveRequestStatusHistories = LeaveRequestStatusHistorySeedData.GetList();
        var mockRepo = new Mock<ILeaveRequestStatusHistoryRepository>();

        mockRepo.Setup(lsh => lsh.HasAnyLeaveHistoryWithRequestIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int leaveRequestId) =>
                leaveRequestStatusHistories.Any(lsh => lsh.LeaveRequestId == leaveRequestId));

        return mockRepo;
    }

    public static Mock<ILeaveRequestStatusHistoryRepository> AddMock()
    {
        var leaveRequestStatusHistories = LeaveRequestStatusHistorySeedData.GetList();
        var mockRepo = new Mock<ILeaveRequestStatusHistoryRepository>();

        mockRepo.Setup(lsh => lsh.AddAsync(It.IsAny<LeaveRequestStatusHistory>()))
            .ReturnsAsync((LeaveRequestStatusHistory leaveRequestStatusHistory) =>
            {
                leaveRequestStatusHistories.Add(leaveRequestStatusHistory);
                return leaveRequestStatusHistory;
            });

        return mockRepo;
    }
}