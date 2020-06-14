using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using QuiZone.BusinessLogic.Utils.JwtAuth;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CM = QuiZone.API.Utils.Helpers.ConfigurationManager;

namespace QuiZone.API.Extensions
{
    public static class AuthenticationExtension
    {
        private const int keyLength = 25;
        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            var key = GenerateTokenKey(keyLength);
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = CM.GetAppSettingsValue("JwtIssuerOptions:Issuer");
                options.Audience = CM.GetAppSettingsValue("JwtIssuerOptions:Audience");
                options.AccessExpirationMins = int.Parse(CM.GetAppSettingsValue("JwtIssuerOptions:Lifetime"));
                options.RefreshExpirationMins = 60 * 24 * 7;

                options.SigningCredentials = new SigningCredentials(
                     signingKey,
                     SecurityAlgorithms.HmacSha256);
            });


            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = CM.GetAppSettingsValue("JwtIssuerOptions:Issuer"),

                ValidateAudience = true,
                ValidAudience = CM.GetAppSettingsValue("JwtIssuerOptions:Audience"),

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = CM.GetAppSettingsValue("JwtIssuerOptions:Issuer");
                configureOptions.TokenValidationParameters = tokenValidationParameters;

                configureOptions.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        if (!string.IsNullOrEmpty(accessToken))
                            context.Token = accessToken;

                        return Task.CompletedTask;
                    }
                };
            });


            services.AddScoped<IJwtFactory, JwtFactory>();
        }


        private static string GenerateTokenKey(int length)
        {
            var characterArray =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxy0123456789".ToCharArray();

            var bytes = new byte[length * 8];
            new RNGCryptoServiceProvider().GetBytes(bytes);
            var result = new char[length];
            for (int i = 0; i < length; i++)
            {
                ulong value = BitConverter.ToUInt64(bytes, i * 8);
                result[i] = characterArray[value % (uint)characterArray.Length];
            }

            return new string(result);
        }
    }
}