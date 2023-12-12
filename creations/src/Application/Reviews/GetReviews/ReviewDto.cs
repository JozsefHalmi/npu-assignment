using AutoMapper;
using Creations.Application.Common.Mappings;
using Creations.Domain.Entities;

namespace Creations.Application.Reviews.GetReviews;
public class ReviewDto : IMapFrom<Review>
{
    public CustomerDto Customer { get; set; }
    public int CreationId { get; set; }
    public int CreativityScore { get; set; }
    public int UniquenessScore { get; set; }
    public string Text { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Review, ReviewDto>()
            .ForMember(d => d.Customer, opt => opt.MapFrom(s => s.Customer))
            .ForMember(d => d.CreationId, opt => opt.MapFrom(s => s.Creation.Id));
    }

    public class CustomerDto : IMapFrom<Customer>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Customer, CustomerDto>();
        }
    }
}
