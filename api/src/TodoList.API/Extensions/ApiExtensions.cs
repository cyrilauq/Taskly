using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TodoList.API.Extensions
{
    public static class ApiExtensions
    {
        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CORS", p => p.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
            });

            return services;
        }

        public static IServiceCollection AddJwtAuthorization(this IServiceCollection services)
        {
            services.AddAuthentication(x =>
                 {
                     x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                     x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                     x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                 })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("3747D5F6-6420-4C9F-B140-39A54E77C3343747D5F6-6420-4C9F-B140-39A54E77C3343747D5F6-6420-4C9F-B140-39A54E77C334")),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            services.AddAuthorization();
            return services;
        }
    }
}
