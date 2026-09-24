using GastricCancerDetection.Domain.Entities;
using GastricCancerDetection.Infrastructure.Persistence;
using GastricCancerDetection.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace GastricCancerDetection.Api.Controllers;

[ApiController][Route("api/health")]
public class HealthController : ControllerBase{
 private readonly GastricCancerDbContext _db; public HealthController(GastricCancerDbContext db)=>_db=db;
 [HttpGet]public async Task<IActionResult>Get(){var can=await _db.Database.CanConnectAsync();return can?Ok(new{status="Healthy",database="Connected",utc=DateTime.UtcNow}):StatusCode(503,new{status="Unhealthy"});}
}
