using GastricCancerDetection.Domain.Entities;
using GastricCancerDetection.Infrastructure.Persistence;
using GastricCancerDetection.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace GastricCancerDetection.Api.Controllers;

[ApiController]
[Route("api/genome-files")]
public class GenomeFilesController : ControllerBase
{
    private readonly GastricCancerDbContext _db;
    private readonly IConfiguration _cfg;

    public GenomeFilesController(
        GastricCancerDbContext db,
        IConfiguration cfg)
    {
        _db = db;
        _cfg = cfg;
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] int? sampleId = null,
        [FromQuery] byte? statusId = null)
    {
        var q = _db.GenomeFiles.AsNoTracking();

        if (sampleId.HasValue)
            q = q.Where(x => x.SampleId == sampleId);

        if (statusId.HasValue)
            q = q.Where(x => x.StatusId == statusId);

        return Ok(
            await q
                .OrderByDescending(x => x.FileId)
                .ToListAsync()
        );
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var x = await _db.GenomeFiles.FindAsync(id);

        return x is null
            ? NotFound()
            : Ok(x);
    }

    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Upload(
        [FromForm] GenomeFileUploadRequest request)
    {
        if (request.File == null || request.File.Length == 0)
            return BadRequest("Empty file.");

        var allowed = new[]
        {
            ".vcf",
            ".gz",
            ".bam",
            ".cram",
            ".fastq"
        };

        var ext = Path
            .GetExtension(request.File.FileName)
            .ToLowerInvariant();

        if (!allowed.Contains(ext))
            return BadRequest("Unsupported genomic file.");

        var dir = _cfg["Storage:GenomeFilesPath"]
                  ?? "storage/genomes";

        Directory.CreateDirectory(dir);

        var safe = Guid.NewGuid() + ext;

        var path = Path.Combine(dir, safe);

        await using var s = System.IO.File.Create(path);

        await request.File.CopyToAsync(s);

        var format =
            ext.Trim('.').ToUpperInvariant() == "GZ"
                ? "VCF"
                : ext.Trim('.').ToUpperInvariant();

        var x = new GenomeFile
        {
            SampleId = request.SampleId,
            BuildId = request.BuildId,
            FileName = request.File.FileName,
            FilePath = path,
            FileFormat = format,
            FileSizeBytes = request.File.Length,
            UploadedAt = DateTime.UtcNow,
            StatusId = 1,
            IsEncryptedAtRest = true
        };

        _db.GenomeFiles.Add(x);

        await _db.SaveChangesAsync();

        return CreatedAtAction(
            nameof(Get),
            new { id = x.FileId },
            x
        );
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var x = await _db.GenomeFiles.FindAsync(id);

        if (x is null)
            return NotFound();

        _db.GenomeFiles.Remove(x);

        await _db.SaveChangesAsync();

        return NoContent();
    }
}