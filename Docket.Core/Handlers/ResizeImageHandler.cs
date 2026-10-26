using System;

namespace Docket.Core.Handlers;

public class ResizeImageHandler<T> : HandlerBase<T>, IHandler
{
    public ResizeImageHandler(string payload) : base(payload) 
    {}

    public override Task Execute()
    {
        PrintData();
        throw new NotImplementedException();
    }
}
