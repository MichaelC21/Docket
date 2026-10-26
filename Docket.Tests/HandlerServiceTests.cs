using Docket.Core.Services;
using Docket.Tests.Fakes;
using FluentAssertions;

namespace Docket.Tests;

public class HandlerServiceTests
{
    [Fact]
    public async Task Handle_RegisteredHandlerSucceeds_ReturnsSuccessfulOutcome()
    {
        var fakeHandler = new FakeHandler();
        var sut = new HandlerService(new FakeHandlerRegisterService(fakeHandler));

        var result = await sut.Handle("SendEmailHandler", "{}");

        result.Success.Should().BeTrue();
        fakeHandler.Executed.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_UnknownHandlerName_ReturnsUnsuccessfulOutcome()
    {
        var sut = new HandlerService(new FakeHandlerRegisterService(new FakeHandler()));

        var result = await sut.Handle("NotARealHandler", "{}");

        result.Success.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_NoHandlerRegisteredForType_ReturnsUnsuccessfulOutcome()
    {
        var sut = new HandlerService(new FakeHandlerRegisterService(null));

        var result = await sut.Handle("SendEmailHandler", "{}");

        result.Success.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_HandlerThrows_ReturnsFailedOutcomeInsteadOfThrowing()
    {
        var fakeHandler = new FakeHandler(new InvalidOperationException("boom"));
        var sut = new HandlerService(new FakeHandlerRegisterService(fakeHandler));

        var result = await sut.Handle("SendEmailHandler", "{}");

        result.Success.Should().BeFalse();
        result.Message.Should().Contain("boom");
    }
}
