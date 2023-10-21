
namespace Creations.Domain.Entities;
public class Brick : BaseEntity
{
    public string Code { get; set; }
    public string Name { get; set; }
    public List<Creation> Creations { get; set; }
}
