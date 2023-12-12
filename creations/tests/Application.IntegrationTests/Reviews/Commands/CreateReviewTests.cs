using Creations.Application.Common.Exceptions;
using Creations.Application.Reviews.Commands.CreateReview;
using Creations.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

using static Creations.Application.IntegrationTests.Testing;

namespace Creations.Application.IntegrationTests.Reviews.Commands;
public class CreateReviewTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateReviewCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldThrowWhenNotUnique()
    {
        var customer = await FindAsync<Customer>(999);
        var creation = await FindAsync<Creation>(1001);

        await FluentActions.Invoking(async () => {
            await SendAsync(new CreateReviewCommand
            {
                CreationId = creation.Id,
                CustomerId = customer.Id,
                CreativityScore = 5,
                UniquenessScore = 9,
                Text = "Some text"
            });

            await SendAsync(new CreateReviewCommand
            {
                CreationId = creation.Id,
                CustomerId = customer.Id,
                CreativityScore = 5,
                UniquenessScore = 8,
                Text = "Some text 222"
            });
        }).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateReview()
    {
        var customer = await FindAsync<Customer>(999);
        var creation = await FindAsync<Creation>(1001);

        var itemId = await SendAsync(new CreateReviewCommand
        {
            CreationId = creation.Id,
            CustomerId = customer.Id,
            CreativityScore = 5,
            UniquenessScore = 9,
            Text = "Some text"
        });

        var item = await FindAsync<Review>(itemId);

        item.Should().NotBeNull();
        item.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}

