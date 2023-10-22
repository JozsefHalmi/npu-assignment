using AutoMapper;
using Creations.Application.Common.Mappings;
using Creations.Domain.Entities;

namespace Creations.Application.Creations.Queries.GetCreations;

public class CreationDto : IMapFrom<Creation>
{
    public DateTime Created { get; init; }
    public string? CreatedBy { get; set; }
    public double? UniquenessScore { get; set; }
    public double? CreativityScore { get; set; }
    public string? ThumbnailPath { get; set; }
    public string? ImagePath { get; set; }
    public string? Description { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Creation, CreationDto>()
            .ForMember(d => d.CreatedBy, opt => opt.MapFrom(s => $"{s.Customer.FirstName} {s.Customer.LastName}"))
            .ForMember(d => d.UniquenessScore, opt => opt.Ignore())
            .ForMember(d => d.CreativityScore, opt => opt.Ignore());
    }
}
