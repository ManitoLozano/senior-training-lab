using Sales.Api.Middlewares;

namespace Sales.Api.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseApiMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<GlobalExceptionMiddleware>();

        return app;
    }
}