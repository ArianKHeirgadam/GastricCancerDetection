using GastricCancerDetection.Domain.Entities;
using GastricCancerDetection.Infrastructure.Persistence;
using GastricCancerDetection.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace GastricCancerDetection.Api.Controllers;

[ApiController][Route("api/samples")]
public class SamplesController : ControllerBase{
 private readonly GastricCancerDbContext _db; public SamplesController(GastricCancerDbContext db)=>_db=db;
 [HttpGet] public async Task<IActionResult> Get([FromQuery]int? subjectId=null){var q=_db.Samples.AsNoTracking();if(subjectId.HasValue)q=q.Where(x=>x.SubjectId==subjectId);return Ok(await q.OrderByDescending(x=>x.SampleId).ToListAsync());}
 [HttpGet("{id:int}")]public async Task<IActionResult> Get(int id){var x=await _db.Samples.FindAsync(id);return x is null?NotFound():Ok(x);}
 [HttpPost]public async Task<IActionResult>Create(Sample x){x.SampleId=0;_db.Samples.Add(x);await _db.SaveChangesAsync();return CreatedAtAction(nameof(Get),new{id=x.SampleId},x);}
 [HttpPut("{id:int}")]public async Task<IActionResult>Update(int id,Sample input){var x=await _db.Samples.FindAsync(id);if(x is null)return NotFound();input.SampleId=id;_db.Entry(x).CurrentValues.SetValues(input);await _db.SaveChangesAsync();return Ok(x);}
 [HttpDelete("{id:int}")]public async Task<IActionResult>Delete(int id){var x=await _db.Samples.FindAsync(id);if(x is null)return NotFound();_db.Samples.Remove(x);await _db.SaveChangesAsync();return NoContent();}
}
