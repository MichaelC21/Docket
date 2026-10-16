using System;
using Docket.Persistence.Context;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;

namespace Docket.Tests.Fixtures;

 public class DbFixture : IDisposable
    {
        private string ConnectionString { get; }

        public DbFixture()
        {
            Env.TraversePath().Load();
            ConnectionString = Environment.GetEnvironmentVariable("DOCKET_TEST_CONNECTION_STRING")!;

            if (String.IsNullOrEmpty(ConnectionString))
            {
                throw new InvalidOperationException("Test connection string is not set");
            }

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