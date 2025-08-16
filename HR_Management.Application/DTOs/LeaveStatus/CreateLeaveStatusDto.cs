namespace HR_Management.Application.DTOs.LeaveStatus;

public class CreateLeaveStatusDto : ILeaveStatusDto
{
    public string Name { get; set; }
    public string Description { get; set; }
}