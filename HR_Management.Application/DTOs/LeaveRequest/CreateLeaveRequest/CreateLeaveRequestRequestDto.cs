namespace HR_Management.Application.DTOs.LeaveRequest.CreateLeaveRequest;

/// <summary>
///     this object using for receive data from client in API to transfer to CreateLeaveRequestCommand DTO
/// </summary>
public class CreateLeaveRequestRequestDto
{
    public DateTime DateRequested { get; set; }
    public string RequestComments { get; set; }
    public int UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int LeaveTypeId { get; set; }
}