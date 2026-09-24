using GastricCancerDetection.Domain.Entities;
using GastricCancerDetection.Infrastructure.Persistence;
using GastricCancerDetection.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace GastricCancerDetection.Api.Controllers;

[ApiController][Route("api/reports")]
public class ReportsController : ControllerBase{
 private readonly GastricCancerDbContext _db;public ReportsController(GastricCancerDbContext db)=>_db=db;
 [HttpGet]public async Task<IActionResult>Get(){return Ok(await _db.GeneratedReports.AsNoTracking().OrderByDescending(x=>x.ReportId).Take(100).ToListAsync());}
 [HttpGet("{id:int}/download")]public async Task<IActionResult>Download(int id){var r=await _db.GeneratedReports.FindAsync(id);if(r is null||string.IsNullOrWhiteSpace(r.FilePath)||!System.IO.File.Exists(r.FilePath))return NotFound("Report file not found.");var bytes=await System.IO.File.ReadAllBytesAsync(r.FilePath);var content=r.ReportFormat.Equals("PDF",StringComparison.OrdinalIgnoreCase)?"application/pdf":"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";return File(bytes,content,Path.GetFileName(r.FilePath));}
}
