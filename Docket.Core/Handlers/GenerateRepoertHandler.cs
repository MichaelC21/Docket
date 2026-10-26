using System;

namespace Docket.Core.Handlers;

public class GenerateRepoertHandler<T> : HandlerBase<T>, IHandler
{
    public GenerateRepoertHandler(string payload) : base(payload)
    {}
    
    public override Task Execute()
    {
        PrintData();
        throw new NotImplementedException();
    }
}
