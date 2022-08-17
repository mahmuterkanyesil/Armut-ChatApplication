using Microsoft.Extensions.DependencyInjection;

namespace ChatApplication.Core.Middleware;

public static class ServiceTool
{
    #region Property
    public static IServiceProvider ServiceProvider { get; set; }
    #endregion

    #region Create
    public static IServiceCollection Create(IServiceCollection services)
    {
        ServiceProvider = services.BuildServiceProvider();
        return services;
    }
    #endregion
}