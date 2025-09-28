namespace HR_Management.Domain;

public abstract class BaseDomainEntity
{
    public int Id { get; set; }
    public DateTime? DateCreated { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    public string? LastModifiedBy { get; set; }
}