using Microsoft.AspNetCore.Mvc;

namespace HR_Management.Api.Controllers;

[Route("reset-password")]
public class ResetPasswordController : Controller
{
    [HttpGet]
    public IActionResult Index(string token)
    {
        ViewBag.Token = token;
        return View();
    }
}