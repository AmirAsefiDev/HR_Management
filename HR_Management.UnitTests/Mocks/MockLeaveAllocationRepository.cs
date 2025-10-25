using HR_Management.Application.Contracts.Persistence;
using HR_Management.Domain;
using HR_Management.UnitTests.Common.SeedData;
using MockQueryable;
using Moq;

namespace HR_Management.UnitTests.Mocks;

public static class MockLeaveAllocationRepository
{
    private static Mock<ILeaveAllocationRepository> CreateBaseMock(List<LeaveAllocation> leaveAllocations)
    {
        var mockRepo = new Mock<ILeaveAllocationRepository>();
        mockRepo.Setup(la => la.GetAllAsync())
            .ReturnsAsync(leaveAllocations);

        mockRepo.Setup(la => la.GetAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) => leaveAllocations.FirstOrDefault(la => la.Id == id));

        return mockRepo;
    }

    public static Mock<ILeaveAllocationRepository> HasAnyLeaveAllocationWithTypeIdMock()
    {
        var leaveAllocations = LeaveAllocationSeedData.GetList();
        var mockRepo = new Mock<ILeaveAllocationRepository>();

        mockRepo.Setup(la => la.HasAnyLeaveAllocationWithTypeIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int leaveTypeId) =>
                leaveAllocations.Any(la => la.LeaveTypeId == leaveTypeId));

        return mockRepo;
    }

    public static Mock<ILeaveAllocationRepository> AddMock()
    {
        var leaveAllocations = LeaveAllocationSeedData.GetList();
        var mockRepo = CreateBaseMock(leaveAllocations);

        mockRepo.Setup(la => la.AddAsync(It.IsAny<LeaveAllocation>()))
            .ReturnsAsync((LeaveAllocation leaveAllocation) =>
            {
                leaveAllocation.Id = 4;
                leaveAllocations.Add(leaveAllocation);
                return leaveAllocation;
            });

        return mockRepo;
    }

    public static Mock<ILeaveAllocationRepository> DeleteMock()
    {
        var leaveAllocations = LeaveAllocationSeedData.GetList();
        var mockRepo = CreateBaseMock(leaveAllocations);

        mockRepo.Setup(la => la.DeleteAsync(It.IsAny<LeaveAllocation>()))
            .Callback((LeaveAllocation deleted) =>
            {
                var existing = leaveAllocations.FirstOrDefault(la => la.Id == deleted.Id);
                if (existing != null)
                    leaveAllocations.Remove(existing);
            }).Returns(Task.CompletedTask);

        return mockRepo;
    }

    public static Mock<ILeaveAllocationRepository> GetLeaveAllocationDetailsMock()
    {
        var leaveAllocations = LeaveAllocationSeedData.GetList();
        var mockRepo = CreateBaseMock(leaveAllocations);

        mockRepo.Setup(la => la.GetLeaveAllocationWithDetailsAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) =>
                leaveAllocations.FirstOrDefault(la => la.Id == id));

        return mockRepo;
    }

    public static Mock<ILeaveAllocationRepository> GetLeaveAllocationsListMock()
    {
        var leaveAllocations = LeaveAllocationSeedData.GetList();
        var mockQueryable = leaveAllocations.BuildMock();

        var mockRepo = new Mock<ILeaveAllocationRepository>();

        mockRepo.Setup(la => la.GetLeaveAllocationsWithDetails())
            .Returns(mockQueryable);

        return mockRepo;
    }
}