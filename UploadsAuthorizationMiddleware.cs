using System.Diagnostics;

namespace MuafiyetProjesi2024
{
    public class UploadsAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public UploadsAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.ToString().ToLower();
            Debug.WriteLine(path);
            if (path.StartsWith("/uploads")) //gelen istek /uploads ile başlıyorsa
            {
                var userMail = context.Session.GetString("UserMail");
                var userRole = context.Session.GetString("KullaniciYetki");
                Debug.WriteLine(userRole);

                if (string.IsNullOrEmpty(userMail) || userRole != "1") // 1 admin rolüydü ve ondan başka rolü reddedeceğiz
                {
                    context.Response.StatusCode = 403;
                    await context.Response.WriteAsync("Access Denied");
                    return;
                }
            }

            await _next(context);
        }
    }

    public static class UploadsAuthorizationMiddlewareExtensions
    {
        public static IApplicationBuilder UseUploadsAuthorization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UploadsAuthorizationMiddleware>();
        }
    }
}

