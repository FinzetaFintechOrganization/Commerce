using System.Threading.Tasks;
using IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static IdentityServer4.IdentityServerConstants;

[Authorize(LocalApi.PolicyName)]
[Route("api/[controller]/[action]")]
[ApiController]

public class RegisterController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public RegisterController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> UserRegister(UserRegisterDto userRegisterDto)
    {
        var values = new ApplicationUser{
            UserName = userRegisterDto.UserName,
            Email = userRegisterDto.Email,
            Name = userRegisterDto.Name,
            Surname = userRegisterDto.Surname,
        };

        var result = await _userManager.CreateAsync(values,userRegisterDto.Password);
        if (result.Succeeded)
        { 
            return Ok("Kullanıcı başarıyla eklendi.");
        }
        else {
            return Ok("Hata oluştu tekrar deneyiniz.");
        }
    }
}