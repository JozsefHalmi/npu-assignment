using Creations.Application.Common.Models;
using Creations.Application.Reviews.Commands.CreateReview;
using Creations.Application.Reviews.GetReviews;
using Creations.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Reviews.Application.Reviews.Queries.GetReviews;

namespace Reviews.WebApi.Controllers;
public class ReviewsController : ApiControllerBase
{
    [HttpGet]
    public async Task<PaginatedList<ReviewDto>> Get([FromQuery] GetReviewsWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task Post(CreateReviewCommand command)
    {
        await Mediator.Send(command);
    }
}
