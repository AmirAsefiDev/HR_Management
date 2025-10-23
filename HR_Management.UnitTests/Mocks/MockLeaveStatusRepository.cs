using HR_Management.Application.Contracts.Persistence;
using HR_Management.Domain;
using HR_Management.UnitTests.Common.SeedData;
using MockQueryable;
using Moq;

namespace HR_Management.UnitTests.Mocks;

public class MockLeaveStatusRepository
{
    private static Mock<ILeaveStatusRepository> CreateBaseMock(List<LeaveStatus> leaveStatuses)
    {
        var mockRepo = new Mock<ILeaveStatusRepository>();
        mockRepo.Setup(ls => ls.GetAllAsync()).ReturnsAsync(leaveStatuses);

        mockRepo.Setup(ls => ls.GetAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) => leaveStatuses.FirstOrDefault(ls => ls.Id == id));

        return mockRepo;
    }

    public static Mock<ILeaveStatusRepository> GetLeaveStatusesListMock()
    {
        var leaveStatuses = LeaveStatusSeedData.GetList();
        var mockQueryable = leaveStatuses.BuildMock();

        var mockRepo = new Mock<ILeaveStatusRepository>();
        mockRepo.Setup(ls => ls.GetLeaveStatusesWithDetails())
            .Returns(mockQueryable);

        return mockRepo;
    }

    public static Mock<ILeaveStatusRepository> GetLeaveStatusDetailsMock()
    {
        var leaveStatuses = LeaveStatusSeedData.GetList();
        var mockRepo = CreateBaseMock(leaveStatuses);

        return mockRepo;
    }

    public static Mock<ILeaveStatusRepository> GetLeaveStatusListSelection()
    {
        var leaveStatuses = LeaveStatusSeedData.GetList();
        var mockRepo = CreateBaseMock(leaveStatuses);

        return mockRepo;
    }

    public static Mock<ILeaveStatusRepository> AddMock()
    {
        var leaveStatuses = LeaveStatusSeedData.GetList();
        var mockRepo = CreateBaseMock(leaveStatuses);

        mockRepo.Setup(m => m.AddAsync(It.IsAny<LeaveStatus>()))
            .ReturnsAsync((LeaveStatus leaveStatus) =>
            {
                leaveStatus.Id = 4;
                leaveStatuses.Add(leaveStatus);
                return leaveStatus;
            });

        return mockRepo;
    }

    public static Mock<ILeaveStatusRepository> UpdateMock()
    {
        var leaveStatuses = LeaveStatusSeedData.GetList();
        var mockRepo = CreateBaseMock(leaveStatuses);

        var leaveStatusUpdate = new LeaveStatus
        {
            Id = 4,
            Name = "Essentials",
            Description = "Test Changed"
        };

        mockRepo.Setup(ls => ls.UpdateAsync(leaveStatusUpdate))
            .Callback((LeaveStatus updated) =>
            {
                var existing = leaveStatuses.FirstOrDefault();
                if (existing != null)
                {
                    existing.Name = updated.Name;
                    existing.Description = updated.Description;
                }
            });

        return mockRepo;
    }

    public static Mock<ILeaveStatusRepository> DeleteMock()
    {
        var leaveStatuses = LeaveStatusSeedData.GetList();
        var mockRepo = CreateBaseMock(leaveStatuses);

        mockRepo.Setup(ls => ls.DeleteAsync(It.IsAny<LeaveStatus>()))
            .Callback((LeaveStatus deleted) =>
            {
                var existing = leaveStatuses.FirstOrDefault(ls => ls.Id == deleted.Id);
                if (existing != null)
                    leaveStatuses.Remove(existing);
            }).Returns(Task.CompletedTask);

        return mockRepo;
    }
}