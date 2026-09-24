using Microsoft.AspNetCore.Http;

namespace GastricCancerDetection.Api.Models;

public class GenomeFileUploadRequest
{
    public IFormFile File { get; set; } = default!;

    public int SampleId { get; set; }

    public int BuildId { get; set; }
}