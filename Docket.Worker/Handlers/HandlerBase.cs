using System;
using Newtonsoft.Json;

namespace Docket.Worker.Handlers;

public abstract class HandlerBase<T> : IHandler
{
    protected T? Data;
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
