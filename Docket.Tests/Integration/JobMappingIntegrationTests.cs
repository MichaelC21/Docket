using Docket.Core.Entities;
using Docket.Persistence.Context;
using DotNetEnv;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Docket.Tests.Integration
{
    public class DbFixture : IDisposable
    {
        private string ConnectionString { get; }

        public DbFixture()
        {
            Env.TraversePath().Load();
            ConnectionString = Environment.GetEnvironmentVariable("DOCKET_TEST_CONNECTION_STRING") ?? "";

            using var context = CreateContext();
            context.Database.Migrate();
        }

        public DocketDBContext CreateContext() =>
            new(new DbContextOptionsBuilder<DocketDBContext>()
                .UseSqlServer(ConnectionString)
                .Options);

        public void Dispose()
        {
            using var context = CreateContext();
            context.Jobs.RemoveRange(context.Jobs);
            context.SaveChanges();
        }
    }

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
            Job? savedJob = context.Jobs.FirstOrDefault(j => j.JobID == job.JobID);

            savedJob.Should().NotBeNull();
            savedJob.Handler.Should().Be("TestHandler");
            savedJob.Payload.Should().Be("{\"message\":\"hello\"}");
        }
    }
}
