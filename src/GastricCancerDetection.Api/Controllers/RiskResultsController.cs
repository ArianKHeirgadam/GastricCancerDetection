using GastricCancerDetection.Domain.Entities;
using GastricCancerDetection.Infrastructure.Persistence;
using GastricCancerDetection.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace GastricCancerDetection.Api.Controllers;

[ApiController][Route("api/risk-results")]
public class RiskResultsController : ControllerBase{
 private readonly GastricCancerDbContext _db;public RiskResultsController(GastricCancerDbContext db)=>_db=db;
 [HttpGet]public async Task<IActionResult>Get([FromQuery]int? subjectId=null,[FromQuery]int? runId=null){var q=_db.SubjectRiskResults.AsNoTracking();if(subjectId.HasValue)q=q.Where(x=>x.SubjectId==subjectId);if(runId.HasValue)q=q.Where(x=>x.RunId==runId);return Ok(await q.OrderByDescending(x=>x.ResultId).Take(500).ToListAsync());}
 [HttpGet("subject/{subjectId:int}/latest")]public async Task<IActionResult>Latest(int subjectId){var x=await _db.SubjectRiskResults.AsNoTracking().Where(x=>x.SubjectId==subjectId).OrderByDescending(x=>x.RunId).FirstOrDefaultAsync();return x is null?NotFound():Ok(x);}
}
