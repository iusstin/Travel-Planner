namespace Domain;

public interface IBaseEntity
{
    long Id { get; set; }
    DateTime CreateDate { get; set; }
    string CreatedBy { get; set; }
    DateTime LastModifiedDate { get; set; }
    string ModifiedBy { get; set; }
}
