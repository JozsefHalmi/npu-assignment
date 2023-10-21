
namespace Creations.Domain.Entities;
public class Review : BaseAuditableEntity
{
    public Customer Customer { get; set; }
    public Creation Creation { get; set; }
    public int CreativityScore { get; set; }
    public int UniquenessScore { get; set; }
    public string Text { get; set; }
}
