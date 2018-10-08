using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Northwind.API.CustomMiddlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            string authHeader = context.Request.Headers["Authorization"];
            // basic hidayat:12345
            // 012345
            if (authHeader != null && authHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
            {
                var token = authHeader.Substring(6).Trim();
                //Encoding to normal string
                var credentialString = token;
                var credential = credentialString.Split(':');

                //DB check here
                if (credential[0] == "hidayat" && credential[1] == "12345")
                {
                    //Principals
                    var claims = new[]
                    {
                        new Claim("name", credential[0]),
                        // Bring from the DB
                        new Claim(ClaimTypes.Role, "Admin")
                    };

                    var identity = new ClaimsIdentity(claims, "basic");
                    context.User = new ClaimsPrincipal(identity);
                }
            }
            else
            {
                context.Response.StatusCode = 401;
            }

            // Pass to next middleware
            await _next(context);
        }
    }
}
