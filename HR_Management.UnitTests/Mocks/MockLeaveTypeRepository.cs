using HR_Management.Application.Contracts.Persistence;
using HR_Management.Domain;
using HR_Management.UnitTests.Common.SeedData;
using MockQueryable;
using Moq;

namespace HR_Management.UnitTests.Mocks;

public static class MockLeaveTypeRepository
{
    private static Mock<ILeaveTypeRepository> CreateBaseMock(List<LeaveType> leaveTypes)
    {
        var mockRepo = new Mock<ILeaveTypeRepository>();
        mockRepo.Setup(lt => lt.GetAllAsync())
            .ReturnsAsync(leaveTypes);

        mockRepo.Setup(lt => lt.GetAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) => leaveTypes.FirstOrDefault(lt => lt.Id == id));

        return mockRepo;
    }

    public static Mock<ILeaveTypeRepository> GetLeaveTypeDetailsMock()
    {
        var leaveTypes = LeaveTypeSeedData.GetList();
        var mockRepo = CreateBaseMock(leaveTypes);

        mockRepo.Setup(lt => lt.ExistAsync(It.IsAny<int>())).ReturnsAsync(true);

        return mockRepo;
    }

    public static Mock<ILeaveTypeRepository> GetLeaveTypesListMock()
    {
        var leaveTypes = LeaveTypeSeedData.GetList();
        var mockQueryable = leaveTypes.BuildMock();

        var mockRepo = new Mock<ILeaveTypeRepository>();

        mockRepo.Setup(lt => lt.GetLeaveTypesWithDetails())
            .Returns(mockQueryable);

        return mockRepo;
    }

    public static Mock<ILeaveTypeRepository> GetLeaveTypeListSelectionMock()
    {
        var leaveTypes = LeaveTypeSeedData.GetList();
        var mockRepo = CreateBaseMock(leaveTypes);

        return mockRepo;
    }

    public static Mock<ILeaveTypeRepository> AddMock()
    {
        var leaveTypes = LeaveTypeSeedData.GetList();
        var mockRepo = CreateBaseMock(leaveTypes);

        mockRepo.Setup(lt => lt.AddAsync(It.IsAny<LeaveType>()))
            .ReturnsAsync((LeaveType leaveType) =>
            {
                leaveType.Id = 4;
                leaveTypes.Add(leaveType);
                return leaveType;
            });

        return mockRepo;
    }

    public static Mock<ILeaveTypeRepository> UpdateMock()
    {
        var leaveTypes = LeaveTypeSeedData.GetList();
        var mockRepo = CreateBaseMock(leaveTypes);
        var leaveTypeUpdate = new LeaveType
        {
            Id = 3,
            DefaultDay = 30,
            Name = "Great"
        };
        mockRepo.Setup(lt => lt.UpdateAsync(leaveTypeUpdate))
            .Callback((LeaveType updated) =>
            {
                var existing = leaveTypes.FirstOrDefault();
                if (existing != null)
                {
                    existing.Name = updated.Name;
                    existing.DefaultDay = updated.DefaultDay;
                }
            });

        return mockRepo;
    }

    public static Mock<ILeaveTypeRepository> DeleteMock()
    {
        var leaveTypes = LeaveTypeSeedData.GetList();
        var mockRepo = CreateBaseMock(leaveTypes);

        mockRepo.Setup(lt => lt.DeleteAsync(It.IsAny<LeaveType>()))
            .Callback((LeaveType deleted) =>
            {
                var existing = leaveTypes.FirstOrDefault(lt => lt.Id == deleted.Id);
                if (existing != null)
                    leaveTypes.Remove(existing);
            }).Returns(Task.CompletedTask);

        return mockRepo;
    }
}