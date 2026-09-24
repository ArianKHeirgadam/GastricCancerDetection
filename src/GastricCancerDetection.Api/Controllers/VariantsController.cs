using GastricCancerDetection.Domain.Entities;
using GastricCancerDetection.Infrastructure.Persistence;
using GastricCancerDetection.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace GastricCancerDetection.Api.Controllers;

[ApiController][Route("api/variants")]
public class VariantsController : ControllerBase{
 private readonly GastricCancerDbContext _db; public VariantsController(GastricCancerDbContext db)=>_db=db;
 [HttpGet]public async Task<IActionResult>Get([FromQuery]string? chromosome=null,[FromQuery]long? position=null,[FromQuery]int? geneId=null,[FromQuery]string? dbSnpId=null){var q=_db.VariantCatalog.AsNoTracking();if(chromosome!=null)q=q.Where(x=>x.Chromosome==chromosome);if(position.HasValue)q=q.Where(x=>x.Position==position);if(geneId.HasValue)q=q.Where(x=>x.GeneId==geneId);if(dbSnpId!=null)q=q.Where(x=>x.dbSNP_Id==dbSnpId);return Ok(await q.Take(500).ToListAsync());}
 [HttpGet("{id:long}")]public async Task<IActionResult>Get(long id){var x=await _db.VariantCatalog.FindAsync(id);return x is null?NotFound():Ok(x);}
 [HttpPost]public async Task<IActionResult>Create(VariantCatalog x){x.VariantCatalogId=0;_db.VariantCatalog.Add(x);await _db.SaveChangesAsync();return CreatedAtAction(nameof(Get),new{id=x.VariantCatalogId},x);}
 [HttpDelete("{id:long}")]public async Task<IActionResult>Delete(long id){var x=await _db.VariantCatalog.FindAsync(id);if(x is null)return NotFound();_db.VariantCatalog.Remove(x);await _db.SaveChangesAsync();return NoContent();}
}
