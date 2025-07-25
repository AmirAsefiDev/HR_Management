namespace HR_Management.Domain;

public class LeaveType : BaseDomainEntity
{
    public string Name { get; set; }
    public int DefaultDay { get; set; }
}