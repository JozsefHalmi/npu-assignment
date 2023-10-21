namespace Creations.Domain.Entities;
public class Creation : BaseAuditableEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ThumbnailPath { get; set; }
    public string ImagePath { get; set; }
    public Customer Customer { get; set; }
    public IEnumerable<Brick> Bricks { get; set; }
}
