using Docket.Core.Entities;
using Docket.Tests.Fixtures;
using FluentAssertions;

namespace Docket.Tests.Integration
{
   

    public class JobMappingIntegrationTests : IClassFixture<DbFixture>
    {
        private readonly DbFixture _db;

        public JobMappingIntegrationTests(DbFixture db)
        {
            _db = db;
        }

        [Fact]
        public void TestJobMapping()
        {
            // Arrange
            using var context = _db.CreateContext();
            var job = new Job("TestHandler", "{\"message\":\"hello\"}");

            // Act
            context.Jobs.Add(job);
            context.SaveChanges();

            // Assert
            using var context2 = _db.CreateContext();

            Job? savedJob = context2.Jobs.FirstOrDefault(j => j.JobID == job.JobID);

            savedJob.Should().NotBeNull();
            savedJob.JobID.Should().Be(job.JobID);
            savedJob.Handler.Should().Be(job.Handler);
            savedJob.Payload.Should().Be(job.Payload);
            savedJob.Status.Should().Be(job.Status);
            savedJob.Attempted.Should().Be(job.Attempted);
            savedJob.MaxAttempts.Should().Be(job.MaxAttempts);
            savedJob.NextRunUTC.Should().Be(job.NextRunUTC);
            savedJob.CreatedAtUTC.Should().Be(job.CreatedAtUTC);

        }
    }
}
