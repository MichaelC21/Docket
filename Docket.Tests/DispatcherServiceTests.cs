using Docket.Core.Models;
using Docket.Tests.Fakes;
using Docket.Worker.Services;
using FluentAssertions;

namespace Docket.Tests;

public class DispatcherServiceTests
{
    [Fact]
    public async Task DispatchJob_DelegatesToHandlerServiceAndReturnsItsOutcome()
    {
        var expected = new HandlerResult(true, DateTime.Now, "ok");
        var fakeHandlerService = new FakeHandlerService(expected);
        var sut = new DispatcherService(fakeHandlerService);

        var result = await sut.DispatchJob("SendEmailHandler", "{\"To\":\"a@b.com\"}");

        fakeHandlerService.ReceivedHandler.Should().Be("SendEmailHandler");
        fakeHandlerService.ReceivedPayload.Should().Be("{\"To\":\"a@b.com\"}");
        result.Should().BeSameAs(expected);
    }
}
