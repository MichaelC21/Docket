using System;

namespace Docket.Core.Handlers;

public class SendEmailHandler<T> : HandlerBase<T>, IHandler
{
    public SendEmailHandler(string payload) : base(payload)
    {}

    public override Task Execute()
    {
        PrintData();
        throw new NotImplementedException();
    }
}
