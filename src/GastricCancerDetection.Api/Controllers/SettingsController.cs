using GastricCancerDetection.Domain.Entities;
using GastricCancerDetection.Infrastructure.Persistence;
using GastricCancerDetection.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace GastricCancerDetection.Api.Controllers;

[ApiController][Route("api/settings")]
public class SettingsController : ControllerBase{
 private readonly GastricCancerDbContext _db;public SettingsController(GastricCancerDbContext db)=>_db=db;
 [HttpGet]public async Task<IActionResult>Get()=>Ok(await _db.SystemSettings.AsNoTracking().ToListAsync());
 [HttpGet("{key}")]public async Task<IActionResult>Get(string key){var x=await _db.SystemSettings.FindAsync(key);return x is null?NotFound():Ok(x);}
 [HttpPut("{key}")]public async Task<IActionResult>Put(string key,[FromBody]SettingRequest req){var x=await _db.SystemSettings.FindAsync(key);if(x is null){x=new SystemSetting{SettingKey=key,SettingValue=req.Value,Description=req.Description,UpdatedAt=DateTime.UtcNow};_db.SystemSettings.Add(x);}else{x.SettingValue=req.Value;x.Description=req.Description;x.UpdatedAt=DateTime.UtcNow;}await _db.SaveChangesAsync();return Ok(x);}
}
public record SettingRequest(string Value,string? Description);
