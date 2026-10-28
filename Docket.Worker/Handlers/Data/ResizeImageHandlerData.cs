using System;
using System.Runtime.CompilerServices;

namespace Docket.Worker.Handlers.Data;

public class ResizeImageHandlerData
{
    public int ImageID {get; set;}
    public int Width {get; set;}
    public int Height {get; set;}
}
