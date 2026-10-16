using System;
using System.Net.Http.Json;
using Docket.Core.Entities;
using Docket.Tests.Fixtures;
using Docket.Web.Data;
using FluentAssertions;

namespace Docket.Tests.Integration;

public class EnqueueJobTests : IClassFixture<DbFixture>, IClassFixture<DocketWebApplicationFactory>
{
    private readonly DbFixture _db;
    private readonly HttpClient _client;

    public EnqueueJobTests(DbFixture db, DocketWebApplicationFactory factory)
    {
        _db = db;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task TestEnqueueEndpointPost_ReturnsString()
    {
        // Arrange
        var request = new EnqueueRequest("TestHandler", "{\"message\":\"hello\"}");

        // Act
        var response = await _client.PostAsJsonAsync("/api/enqueue", request);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var jobIdText = (await response.Content.ReadAsStringAsync()).Trim('"');
        Guid.TryParse(jobIdText, out var jobId).Should().BeTrue();

        using var context = _db.CreateContext();
        var savedJob = context.Jobs.FirstOrDefault(j => j.JobID == jobId);

        savedJob.Should().NotBeNull();
        savedJob!.Handler.Should().Be(request.handler);
        savedJob.Payload.Should().Be(request.payload);
    }

    [Fact]
    public async Task TestEnqueueEndpointPost_MissingHandler_ReturnsBadRequest()
    {
        // Arrange
        var request = new EnqueueRequest("", "{\"message\":\"hello\"}");

        // Act
        var response = await _client.PostAsJsonAsync("/api/enqueue", request);

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
    }
}
