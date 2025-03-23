using Application.Auth;
using Application.Auth.Dto;
using Application.Courses.Services;
using Application.Modules.Services;
using Application.Tests.Services;
using Application.Users.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ServiceRegistration
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(x => configuration.GetSection("JwtSettings").Bind(x));
        
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICourseService, CourseService>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IModuleService, ModuleService>();
        services.AddScoped<ITestService, TestService>();
        
        services.AddAutoMapper(typeof(ServiceRegistration).Assembly);
        services.AddHttpContextAccessor();
        services.AddHttpClient();
    }
}