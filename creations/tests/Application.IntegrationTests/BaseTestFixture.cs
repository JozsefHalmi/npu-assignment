using NUnit.Framework;

using static Creations.Application.IntegrationTests.Testing;

namespace Creations.Application.IntegrationTests;
[TestFixture]
public abstract class BaseTestFixture
{
    [SetUp]
    public async Task TestSetUp()
    {
        await ResetState();
    }
}
