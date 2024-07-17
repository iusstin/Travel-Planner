namespace Domain.Common;

public abstract class BaseEntity
{
    public DateTime CreateDate { get; set; }
    public string CreatedBy { get; set; } = default!;
    public DateTime? LastModifiedDate { get; set; }
    public string? ModifiedBy { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeleteDate { get; set; }
    public string? DeletedBy { get; set; }
}
