using GastricCancerDetection.Domain.Entities;
using GastricCancerDetection.Infrastructure.Persistence;
using GastricCancerDetection.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace GastricCancerDetection.Api.Controllers;

[ApiController][Route("api/audit-logs")]
public class AuditLogsController : ControllerBase{
 private readonly GastricCancerDbContext _db;public AuditLogsController(GastricCancerDbContext db)=>_db=db;
 [HttpGet]public async Task<IActionResult>Get([FromQuery]string? entityType=null,[FromQuery]int page=1){page=Math.Max(page,1);var q=_db.AuditLogs.AsNoTracking();if(entityType!=null)q=q.Where(x=>x.EntityType==entityType);return Ok(await q.OrderByDescending(x=>x.AuditId).Skip((page-1)*100).Take(100).ToListAsync());}
}
