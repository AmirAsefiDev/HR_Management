using HR_Management.Application.Contracts.Persistence;
using HR_Management.UnitTests.Common.SeedData;
using Moq;

namespace HR_Management.UnitTests.Mocks;

public static class MockLeaveAllocationRepository
{
    public static Mock<ILeaveAllocationRepository> HasAnyLeaveAllocationWithTypeIdMock()
    {
        var leaveAllocations = LeaveAllocationSeedData.GetList();
        var mockRepo = new Mock<ILeaveAllocationRepository>();

        mockRepo.Setup(la => la.HasAnyLeaveAllocationWithTypeIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int leaveTypeId) =>
                leaveAllocations.Any(la => la.LeaveTypeId == leaveTypeId));

        return mockRepo;
    }
}