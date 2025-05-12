using Amazon.S3;

namespace Api.Extensions;

public static class AwsServiceExtensions
{
    public static void AddAwsServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDefaultAWSOptions(configuration.GetAWSOptions("AWS"));
        services.AddAWSService<IAmazonS3>();
    }
}
