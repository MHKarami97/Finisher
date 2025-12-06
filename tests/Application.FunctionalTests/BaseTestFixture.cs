using System.Diagnostics.CodeAnalysis;

namespace Finisher.Application.FunctionalTests;

using static Testing;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[TestFixture]
internal abstract class BaseTestFixture
{
    [SetUp]
    public async Task TestSetUp()
    {
        await ResetState();
    }
}
