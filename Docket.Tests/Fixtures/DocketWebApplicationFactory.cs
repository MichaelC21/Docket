using System;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Docket.Tests.Fixtures;

public class DocketWebApplicationFactory : WebApplicationFactory<Program>
{
    public DocketWebApplicationFactory()
    {
        Environment.SetEnvironmentVariable(
            "DOCKET_CONNECTION_STRING",
            Environment.GetEnvironmentVariable("DOCKET_TEST_CONNECTION_STRING"));
    }
}
