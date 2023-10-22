using Creations.Application.Creations.Commands.CreateCreation;
using Creations.Application.Creations.Queries.GetCreations;
using Microsoft.AspNetCore.Mvc;

namespace Creations.WebApi.Controllers;
public class CreationsController : ApiControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<Creation>> Get(GetCreationsQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task Post(CreateCreationCommand command)
    {
        await Mediator.Send(command);
    }
}
