using System;

namespace Docket.Worker.Handlers;

public class CleanupTempFileHandler<T> : HandlerBase<T>, IHandler
{
    public CleanupTempFileHandler(string payload) : base(payload)
    {}

    public override Task Execute()
    {
        PrintData();
        throw new NotImplementedException();
    }
}
