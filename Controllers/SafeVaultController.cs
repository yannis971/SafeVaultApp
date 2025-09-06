
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class SafeVaultController : Controller
{
    [HttpGet("/dashboard")]
    public IActionResult Dashboard()
    {
        return View();
    }
}
