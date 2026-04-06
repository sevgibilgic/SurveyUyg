using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SurveyUyg.API.DTOs;
using SurveyUyg.API.Models;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IConfiguration _configuration;

    public UserController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }
    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var user = new AppUser
        {
            UserName = dto.UserName,
            Email = dto.Email,
            FullName = dto.FullName,
            UserRole = "Member"
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (result.Succeeded)
        {
            if (!await _roleManager.RoleExistsAsync("Member"))
            {
                await _roleManager.CreateAsync(new AppRole { Name = "Member" });
            }
            await _userManager.AddToRoleAsync(user, "Member");

            return Ok(new { message = "Kayıt Başarılı." });
        }

        return BadRequest(result.Errors);
    }
    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.UserName);

        if (user != null && await _userManager.CheckPasswordAsync(user, dto.Password))
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                userName = user.UserName
            });
        }

        return Unauthorized(new { message = "Giriş Başarısız!" });
    }

    [Authorize]
    [HttpPut("UpdateProfile")]
    public async Task<IActionResult> UpdateProfile([FromForm] UserUpdateDto dto)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null) return NotFound("Kullanıcı bulunamadı.");

        user.Email = dto.Email;
        user.UserName = dto.UserName;

        if (!string.IsNullOrEmpty(dto.NewPassword))
        {
            if (string.IsNullOrEmpty(dto.CurrentPassword))
            {
                return BadRequest("Şifre değiştirmek için mevcut şifrenizi girmelisiniz.");
            }

            var passResult = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
            if (!passResult.Succeeded)
            {
                return BadRequest(passResult.Errors);
            }
        }

        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            return Ok(new { message = "Profil başarıyla güncellendi." });
        }

        return BadRequest(result.Errors);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("DeleteUser/{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound("Kullanıcı bulunamadı.");

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded) return Ok(new { message = "Kullanıcı silindi." });

        return BadRequest(result.Errors);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("GetAllUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = _userManager.Users.Select(u => new {
            u.Id,
            u.UserName,
            u.FullName,
            u.Email
        }).ToList();

        return Ok(users);
    }

    
}