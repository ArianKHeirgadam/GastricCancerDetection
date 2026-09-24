using GastricCancerDetection.Domain.Entities;
using GastricCancerDetection.Infrastructure.Persistence;
using GastricCancerDetection.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace GastricCancerDetection.Api.Controllers;

[ApiController][Route("api/genes")]
public class GenesController : ControllerBase{
 private readonly GastricCancerDbContext _db; public GenesController(GastricCancerDbContext db)=>_db=db;
 [HttpGet]public async Task<IActionResult>Get([FromQuery]string? search=null){var q=_db.Genes.AsNoTracking();if(!string.IsNullOrWhiteSpace(search))q=q.Where(x=>x.GeneSymbol.Contains(search));return Ok(await q.OrderBy(x=>x.GeneSymbol).ToListAsync());}
 [HttpGet("{id:int}")]public async Task<IActionResult>Get(int id){var x=await _db.Genes.FindAsync(id);return x is null?NotFound():Ok(x);}
 [HttpPost]public async Task<IActionResult>Create(Gene x){x.GeneId=0;_db.Genes.Add(x);await _db.SaveChangesAsync();return CreatedAtAction(nameof(Get),new{id=x.GeneId},x);}
 [HttpPut("{id:int}")]public async Task<IActionResult>Update(int id,Gene input){var x=await _db.Genes.FindAsync(id);if(x is null)return NotFound();input.GeneId=id;_db.Entry(x).CurrentValues.SetValues(input);await _db.SaveChangesAsync();return Ok(x);}
 [HttpDelete("{id:int}")]public async Task<IActionResult>Delete(int id){var x=await _db.Genes.FindAsync(id);if(x is null)return NotFound();_db.Genes.Remove(x);await _db.SaveChangesAsync();return NoContent();}
}
