using System;

namespace Docket.Web.Data;

public record EnqueueRequest(string handler, string payload);
