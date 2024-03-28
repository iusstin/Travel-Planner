namespace Domain;

public abstract class IBaseEntity
{
    public long Id { get; set; }
    public DateTime CreateDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime LastModifiedDate { get; set; }
    public string ModifiedBy { get; set; }

    public DateTime DeleteDate { get; set; }
    public bool IsDeleted { get; set; }
}
