namespace HR_Management.Domain;

public class LeaveRequestStatusHistory : BaseDomainEntity
{
    public int LeaveRequestId { get; set; }
    public LeaveRequest LeaveRequest { get; set; }
    public int LeaveStatusId { get; set; }
    public LeaveStatus LeaveStatus { get; set; }
    public string Comment { get; set; }
    public int ChangedBy { get; set; }
    public User User { get; set; }
}