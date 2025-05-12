using Amazon.S3;
using Amazon.S3.Model;
using Application.Abstracts.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Application.Services;

public class S3Service : IS3Service
{
    private readonly string _bucketName;
    private readonly IAmazonS3 _amazonS3;

    public S3Service(IConfiguration configuration, IAmazonS3 amazonS3)
    {
        _bucketName = configuration["AWS:BucketName"]!;
        _amazonS3 = amazonS3;
    }

    public async Task<string> UploadFileAsync(IFormFile file, string? entityType, Guid entityId)
    {
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

        var key = $"{entityType}/{entityId}/{fileName}";

        await using var stream = file.OpenReadStream();

        var request = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = key,
            InputStream = stream,
            ContentType = file.ContentType,
            CannedACL = S3CannedACL.Private
        };
        
        await _amazonS3.PutObjectAsync(request);

        return key;
        //var returnUrl = $"https://{_bucketName}.s3.amazonaws.com/{key}";
        
        //return returnUrl;
    }

    public string GeneratePreSignedUrl(string key, TimeSpan validFor)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = _bucketName,
            Key = key,
            Expires = DateTime.UtcNow.Add(validFor),
            Verb = HttpVerb.GET
        };

        var url = _amazonS3.GetPreSignedURL(request);
        return url;
    }

    public async Task DeleteFileAsync(string key)
    {
        await _amazonS3.DeleteObjectAsync(_bucketName, key);
    }

    public async Task<Stream> GetFileAsync(string key)
    {
        var response = await _amazonS3.GetObjectAsync(_bucketName, key);
        return response.ResponseStream;
    }
}