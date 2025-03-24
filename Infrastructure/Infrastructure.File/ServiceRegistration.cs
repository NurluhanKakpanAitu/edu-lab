using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Minio;
namespace Infrastructure.File;

public static class ServiceRegistration
{
    public static void AddFileInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MinioConfig>(x => configuration.GetSection("Minio").Bind(x));

        services.AddSingleton<IMinioClient>(
            serviceProvider =>
            {
                var minioConfig = serviceProvider.GetRequiredService<IOptions<MinioConfig>>().Value;
                
                var client = new MinioClient()
                    .WithEndpoint(minioConfig.Endpoint)
                    .WithCredentials(minioConfig.AccessKey, minioConfig.SecretKey);

                if (minioConfig.UseSSL)
                {
                    client = client.WithSSL();
                }

                return client.Build();
            });
        
        services.AddScoped<IMinioFileService, MinioFileService>();
    }
}