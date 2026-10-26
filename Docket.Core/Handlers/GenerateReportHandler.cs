using System;

namespace Docket.Core.Handlers;

public class GenerateReportHandler<T> : HandlerBase<T>, IHandler
{
    public GenerateReportHandler(string payload) : base(payload)
    {}
    
    public override Task Execute()
    {
        PrintData();
        throw new NotImplementedException();
    }
}
