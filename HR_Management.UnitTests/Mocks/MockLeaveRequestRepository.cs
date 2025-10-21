using HR_Management.Application.Contracts.Persistence;
using HR_Management.UnitTests.Common.SeedData;
using Moq;

namespace HR_Management.UnitTests.Mocks;

public static class MockLeaveRequestRepository
{
    public static Mock<ILeaveRequestRepository> HasAnyLeaveAllocationWithTypeIdMock()
    {
        var leaveRequest = LeaveRequestSeedData.GetList();
        var mockRepo = new Mock<ILeaveRequestRepository>();

        mockRepo.Setup(lr => lr.HasAnyLeaveRequestWithTypeIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int leaveTypeId) =>
                leaveRequest.Any(lr => lr.LeaveTypeId == leaveTypeId));

        return mockRepo;
    }
}