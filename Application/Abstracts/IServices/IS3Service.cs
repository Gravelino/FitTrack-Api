using Microsoft.AspNetCore.Http;

namespace Application.Abstracts.IServices;

public interface IS3Service
{
    Task<string> UploadFileAsync(IFormFile file, string? entityType, Guid entityId);
    string GeneratePreSignedUrl(string key, TimeSpan validFor);
    Task DeleteFileAsync(string key);
    Task<Stream> GetFileAsync(string key);
}