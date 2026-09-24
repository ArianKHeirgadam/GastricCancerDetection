using GastricCancerDetection.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace GastricCancerDetection.Api.Controllers;
[ApiController][Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly GastricCancerDbContext _db; private readonly IConfiguration _cfg;
    public AuthController(GastricCancerDbContext db,IConfiguration cfg){_db=db;_cfg=cfg;}

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest req)
    {
        var user=await _db.Users.Include(x=>x.Role).SingleOrDefaultAsync(x=>x.Email==req.Email&&x.IsActive);
        if(user is null || !Verify(req.Password,user.PasswordHash)) return Unauthorized(new{message="Invalid credentials."});
        user.LastLoginAt=DateTime.UtcNow; await _db.SaveChangesAsync();
        var claims=new[]{new Claim(JwtRegisteredClaimNames.Sub,user.UserId.ToString()),new Claim(ClaimTypes.Name,user.FullName),new Claim(ClaimTypes.Email,user.Email),new Claim(ClaimTypes.Role,user.Role?.RoleName??"Viewer")};
        var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_cfg["Jwt:Key"]!));
        var token=new JwtSecurityToken(claims:claims,expires:DateTime.UtcNow.AddHours(8),signingCredentials:new SigningCredentials(key,SecurityAlgorithms.HmacSha256));
        return Ok(new{token=new JwtSecurityTokenHandler().WriteToken(token),expires=token.ValidTo,userId=user.UserId,role=user.Role?.RoleName});
    }
    static bool Verify(string password,string stored){try{var parts=stored.Split('.',2);if(parts.Length!=2)return false;var salt=Convert.FromBase64String(parts[0]);var hash=Convert.FromBase64String(parts[1]);var test=Rfc2898DeriveBytes.Pbkdf2(password,salt,120000,HashAlgorithmName.SHA256,32);return CryptographicOperations.FixedTimeEquals(test,hash);}catch{return false;}}
}
public record LoginRequest(string Email,string Password);
