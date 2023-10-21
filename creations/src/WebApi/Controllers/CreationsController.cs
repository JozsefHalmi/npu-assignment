using Creations.Application.Creations.Queries.GetCreations;
using Microsoft.AspNetCore.Mvc;

namespace Creations.WebApi.Controllers;
public class CreationsController : ApiControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<Creation>> Get(string brickCode)
    {
        return await Mediator.Send(new GetCreationsQuery(brickCode));
    }
}
