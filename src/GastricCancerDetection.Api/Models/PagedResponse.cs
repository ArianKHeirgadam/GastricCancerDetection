namespace GastricCancerDetection.Api.Models;
public record PagedResponse<T>(IReadOnlyList<T> Items, int Page, int PageSize, int Total);
