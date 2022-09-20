using Microsoft.AspNetCore.Builder;

namespace Microsoft.Streamye.Commons.Exceptions.GlobalExceptions.Extensions
{
    public static class SystemExceptionExtension
    {
        public static IApplicationBuilder AddSystemException(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<SystemExceptionHandlerMiddleware>();
            return builder;
        }
    }
}