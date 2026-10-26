using System;
using Docket.Core.Entities;
using Docket.Core.Enums;
using Docket.Core.Handlers;
using Docket.Core.Handlers.Data;
using Docket.Core.Services;
using Docket.Core.Services.Interfaces;
using FluentAssertions;

namespace Docket.Tests;

public class HandlerRegisterServiceTests
{
    private readonly IHandlerRegisterService _handlerRegisterService = new HandlerRegisterService();


    [Theory]
    [InlineData(Handler.SendEmailHandler, """{"To":"a@b.com","Subject":"hi"}""", typeof(SendEmailHandler<SendEmailHandlerData>))]
    [InlineData(Handler.GenerateReportHandler, """{"ReportID":1,"Format":"pdf"}""", typeof(GenerateRepoertHandler<GenerateRepoertHandlerData>))]
    [InlineData(Handler.CleanupTempFilesHandler, """{"OlderThanDays":7}""", typeof(CleanupTempFileHandler<CleanupTempFileHandlerData>))]
    [InlineData(Handler.ResizeImageHandler, """{"ImageID":1,"Width":10,"Height":10}""", typeof(ResizeImageHandler<ResizeImageHandlerData>))]
    public void TestHandlerRegisterService_GetHandler_ReturnsIHandler(Handler handlerType, string payload, Type expectedType)
    {
        var handler = _handlerRegisterService.GetHandler(handlerType, payload);

        handler.Should().BeOfType(expectedType);
    }

    [Fact]
    public void TestHandlerRegisterService_GetHandlerMissingHandler_ReturnsNull()
    {
        var handler = _handlerRegisterService.GetHandler((Handler)100, """{"Hello": "World"}""");

        handler.Should().BeNull();

    }
}
