namespace Creations.Application.Creations.Queries.GetCreations;

public class Creation
{
    public DateTime CreatedDate { get; init; }
    public string? CreatedBy { get; set; }
    public float UniquenessScore { get; set; }
    public float CreativityScore { get; set; }
    public string? ThumbnailPath { get; set; }
    public string? ImagePath { get; set; }
    public string? Description { get; set; }
}
