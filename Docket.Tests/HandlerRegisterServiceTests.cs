using System;
using Docket.Worker.Handlers;
using Docket.Worker.Handlers.Data;
using Docket.Worker.Services;
using Docket.Worker.Services.Interfaces;
using FluentAssertions;

namespace Docket.Tests;

public class HandlerRegisterServiceTests
{
    private readonly IHandlerRegisterService _handlerRegisterService = new HandlerRegisterService();


    [Theory]
    [InlineData("SendEmailHandler", """{"To":"a@b.com","Subject":"hi"}""", typeof(SendEmailHandler<SendEmailHandlerData>))]
    [InlineData("GenerateReportHandler", """{"ReportID":1,"Format":"pdf"}""", typeof(GenerateReportHandler<GenerateReportHandlerData>))]
    [InlineData("CleanupTempFileHandler", """{"OlderThanDays":7}""", typeof(CleanupTempFileHandler<CleanupTempFileHandlerData>))]
    [InlineData("ResizeImageHandler", """{"ImageID":1,"Width":10,"Height":10}""", typeof(ResizeImageHandler<ResizeImageHandlerData>))]
    public void TestHandlerRegisterService_GetHandler_ReturnsIHandler(string handlerName, string payload, Type expectedType)
    {
        var handler = _handlerRegisterService.GetHandler(handlerName, payload);

        handler.Should().BeOfType(expectedType);
    }

    [Fact]
    public void TestHandlerRegisterService_GetHandlerMissingHandler_ReturnsNull()
    {
        var handler = _handlerRegisterService.GetHandler("NotARealHandler", """{"Hello": "World"}""");

        handler.Should().BeNull();

    }
}
