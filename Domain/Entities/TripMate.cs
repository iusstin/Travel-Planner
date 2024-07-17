using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class TripMate : BaseEntity
{
    public long TripId { get; set; }
    [ForeignKey("TripId")]
    public virtual Trip Trip { get; set; }

    public string UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual User Mate { get; set; }

    public CapabilityType CapabilityType { get; set; }
}
