namespace HR_Management.Domain;

public class LeaveStatus : BaseDomainEntity
{
    /// <summary>
    ///     /"Pending", "Approved", "Rejected", "Cancelled"
    /// </summary>
    public string Name { get; set; }

    public string? Description { get; set; }

    public ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();

    public ICollection<LeaveRequestStatusHistory> LeaveRequestStatusHistories { get; set; } =
        new List<LeaveRequestStatusHistory>();
}