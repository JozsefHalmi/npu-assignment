using Creations.Application.Reviews.Commands.CreateReview;
using Creations.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Reviews.WebApi.Controllers;
public class ReviewsController : ApiControllerBase
{
    //[HttpGet]
    //public async Task<IEnumerable<Review>> Get(GetReviewsQuery query)
    //{
    //    return await Mediator.Send(query);
    //}

    [HttpPost]
    public async Task Post(CreateReviewCommand command)
    {
        await Mediator.Send(command);
    }
}
