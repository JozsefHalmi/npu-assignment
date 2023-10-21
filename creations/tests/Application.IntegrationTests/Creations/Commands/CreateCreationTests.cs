using Creations.Application.Common.Exceptions;
using Creations.Application.Creations.Commands.CreateCreation;
using Creations.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

using static Creations.Application.IntegrationTests.Testing;

namespace Creations.Application.IntegrationTests.Creations.Commands;
public class CreateCreationTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateCreationCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateCreation()
    {
        var customer = await FindAsync<Customer>(999);
        var brick = await FindAsync<Brick>(1);

        var itemId = await SendAsync(new CreateCreationCommand
        {
            Title = "My creation",
            CustomerId = customer.Id,
            BrickCodes = new List<string>() { brick.Code },
            Description = "Some description",
            ImagePath = "img-path",
            ThumbnailPath = "thumbnail-path"
        });

        var item = await FindAsync<Creation>(itemId);

        item.Should().NotBeNull();
        item.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}

