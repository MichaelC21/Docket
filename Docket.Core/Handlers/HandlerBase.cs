using System;
using Newtonsoft.Json;

namespace Docket.Core.Handlers;

public abstract class HandlerBase<T> : IHandler
{
    private T? Data;
    public abstract Task Execute();

    public HandlerBase(string payload)
    {
        Data = ParsePayload(payload);
    }
    private T? ParsePayload(string payload)
    {
        return JsonConvert.DeserializeObject<T>(payload);
    }

    public void PrintData()
    {
        Console.WriteLine(Data == null ? "No data" : JsonConvert.SerializeObject(Data));
    }
}
