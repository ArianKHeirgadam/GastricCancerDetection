using GastricCancerDetection.Domain.Entities;
using GastricCancerDetection.Infrastructure.Persistence;
using GastricCancerDetection.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace GastricCancerDetection.Api.Controllers;

[ApiController][Route("api/subjects")]
public class SubjectsController : ControllerBase
{
    private readonly GastricCancerDbContext _db;
    public SubjectsController(GastricCancerDbContext db)=>_db=db;

    [HttpGet] public async Task<IActionResult> Get([FromQuery]int page=1,[FromQuery]int pageSize=50,[FromQuery]bool? healthy=null){
        page=Math.Max(1,page); pageSize=Math.Clamp(pageSize,1,200);
        var q=_db.Subjects.AsNoTracking().Where(x=>!x.IsDeleted);
        if(healthy.HasValue) q=q.Where(x=>x.IsHealthy==healthy.Value);
        var total=await q.CountAsync();
        var items=await q.OrderByDescending(x=>x.SubjectId).Skip((page-1)*pageSize).Take(pageSize).ToListAsync();
        return Ok(new {items,page,pageSize,total});
    }
    [HttpGet("{id:int}")] public async Task<IActionResult> Get(int id){var x=await _db.Subjects.AsNoTracking().FirstOrDefaultAsync(x=>x.SubjectId==id&&!x.IsDeleted);return x is null?NotFound():Ok(x);}
    [HttpPost] public async Task<IActionResult> Create(Subject x){x.SubjectId=0;x.IsDeleted=false;_db.Subjects.Add(x);await _db.SaveChangesAsync();return CreatedAtAction(nameof(Get),new{id=x.SubjectId},x);}
    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id,Subject input){var x=await _db.Subjects.FindAsync(id);if(x is null||x.IsDeleted)return NotFound();input.SubjectId=id;input.ValidFrom=x.ValidFrom;input.ValidTo=x.ValidTo;_db.Entry(x).CurrentValues.SetValues(input);await _db.SaveChangesAsync();return Ok(x);}
    [HttpDelete("{id:int}")] public async Task<IActionResult> Delete(int id){var x=await _db.Subjects.FindAsync(id);if(x is null)return NotFound();x.IsDeleted=true;await _db.SaveChangesAsync();return NoContent();}
}
