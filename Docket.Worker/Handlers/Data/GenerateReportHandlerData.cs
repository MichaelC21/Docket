using System;

namespace Docket.Worker.Handlers.Data;

public class GenerateReportHandlerData
{
    public int ReportID {get; set;}
    public string Format {get; set;} = String.Empty; 
}
