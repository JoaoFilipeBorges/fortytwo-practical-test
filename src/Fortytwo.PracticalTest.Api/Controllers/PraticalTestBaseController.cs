using Microsoft.AspNetCore.Mvc;

namespace Fortytwo.PracticalTest.Api.Controllers;

[ApiController]
public class PraticalTestBaseController : ControllerBase
{
    protected string GetUsername()
    {
        return User.Identity?.Name!;
    }
}