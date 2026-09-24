using GastricCancerDetection.Domain.Entities;
using GastricCancerDetection.Infrastructure.Persistence;
using GastricCancerDetection.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace GastricCancerDetection.Api.Controllers;

[ApiController][Route("api/gene-panels")]
public class GenePanelsController : ControllerBase{
 private readonly GastricCancerDbContext _db;public GenePanelsController(GastricCancerDbContext db)=>_db=db;
 [HttpGet]public async Task<IActionResult>Get([FromQuery]bool active=true){return Ok(await _db.GenePanels.AsNoTracking().Where(x=>!active||x.IsActive).OrderByDescending(x=>x.PanelId).ToListAsync());}
 [HttpGet("{id:int}")]public async Task<IActionResult>Get(int id){var x=await _db.GenePanels.FindAsync(id);return x is null?NotFound():Ok(x);}
 [HttpPost]public async Task<IActionResult>Create(GenePanel x){x.PanelId=0;_db.GenePanels.Add(x);await _db.SaveChangesAsync();return CreatedAtAction(nameof(Get),new{id=x.PanelId},x);}
 [HttpPut("{id:int}")]public async Task<IActionResult>Update(int id,GenePanel input){var x=await _db.GenePanels.FindAsync(id);if(x is null)return NotFound();input.PanelId=id;_db.Entry(x).CurrentValues.SetValues(input);await _db.SaveChangesAsync();return Ok(x);}
}
