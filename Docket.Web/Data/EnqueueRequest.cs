using System;

namespace Docket.Web.Data;

public record EnqueueRequest(string Handler, string Payload);
