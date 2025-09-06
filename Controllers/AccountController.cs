
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet("/register")]
    public IActionResult Register() => View();

    [HttpPost("/register")]
    public async Task<IActionResult> Register(string username, string email, string password)
    {
        var user = new IdentityUser { UserName = username, Email = email };
        var result = await _userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return Redirect("/dashboard");
        }
        return BadRequest("Registration failed");
    }

    [HttpGet("/login")]
    public IActionResult Login() => View();

    [HttpPost("/login")]
    public async Task<IActionResult> Login(string username, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(username, password, false, false);
        if (result.Succeeded)
            return Redirect("/dashboard");
        return Unauthorized("Login failed");
    }
}
