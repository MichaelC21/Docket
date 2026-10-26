using System;
using Docket.Core.Services;
using Docket.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Docket.Core;

public static class DocketCoreLayer
{
    public static void AddCoreLayer(this IServiceCollection services)
    {
        services.AddSingleton<IHandlerService, HandlerService>();
        services.AddSingleton<IHandlerRegisterService, HandlerRegisterService>();
    }
}
