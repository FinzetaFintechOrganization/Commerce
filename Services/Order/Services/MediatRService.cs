public static class MediatRService
{
    public static void AddAppService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MediatRService).Assembly));
    }
}