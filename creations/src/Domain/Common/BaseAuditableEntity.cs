namespace Creations.Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTime Created { get; set; }

    //TODO: Username handling
    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    //TODO: Username handling
    public string? LastModifiedBy { get; set; }
}
