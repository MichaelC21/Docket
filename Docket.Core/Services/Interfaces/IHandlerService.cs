using System;
using Docket.Core.Models;

namespace Docket.Core.Services.Interfaces;

public interface IHandlerService
{
    Task<HandlerResult> Handle(string handler, string payload);
}
