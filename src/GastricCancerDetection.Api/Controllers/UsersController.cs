using GastricCancerDetection.Domain.Entities;
using GastricCancerDetection.Infrastructure.Persistence;
using GastricCancerDetection.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace GastricCancerDetection.Api.Controllers;

[ApiController][Route("api/users")]
public class UsersController : ControllerBase{
 private readonly GastricCancerDbContext _db;public UsersController(GastricCancerDbContext db)=>_db=db;
 [HttpGet]public async Task<IActionResult>Get(){return Ok(await _db.Users.AsNoTracking().Select(x=>new{x.UserId,x.FullName,x.Email,x.RoleId,x.IsActive,x.CreatedAt,x.LastLoginAt}).ToListAsync());}
 [HttpGet("{id:int}")]public async Task<IActionResult>Get(int id){var x=await _db.Users.AsNoTracking().Where(x=>x.UserId==id).Select(x=>new{x.UserId,x.FullName,x.Email,x.RoleId,x.IsActive,x.CreatedAt,x.LastLoginAt}).FirstOrDefaultAsync();return x is null?NotFound():Ok(x);}
 [HttpPut("{id:int}/status")]public async Task<IActionResult>Status(int id,[FromBody]bool active){var x=await _db.Users.FindAsync(id);if(x is null)return NotFound();x.IsActive=active;await _db.SaveChangesAsync();return Ok(new{x.UserId,x.IsActive});}
}
