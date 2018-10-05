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

            // basic Hidayat:1234
            // Check the auth Header
            // There might be more than one authentications
            if (authHeader !=null && authHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
            {
                // Username token
                var token = authHeader.Substring(6).Trim();
                // Convert to normal string
                var credentialString = Encoding.UTF8.GetString
                    (Convert.FromBase64String(token));
                // Splitting the password from username
                var credentials = credentialString.Split(':');

                // Connecting to DB to check if the password and username is avaliable or not
                // at top we have array of creditial with credintital[0]-username and credintial[1] password
                if (credentials[0]=="hidayat" && credentials[1]=="1234")
                {
                    // Identity principal
                    // claim: Type 
                    var claims = new[]
                    {
                        new Claim("name", credentials[0]),
                        // Role 
                        // It is good if you get it from the DB
                        //new Claim("name", credentials[0]),
                        new Claim(ClaimTypes.Role, "Admin")


                    };
                    // Creat Identity
                    // ClaimsIdentity wants claims and AuthenticationType
                    var identity = new ClaimsIdentity(claims, "basic");

                    context.User = new ClaimsPrincipal(identity);

                }
            }

            // If there is no authHeader
            else
            {
                // You need to authenticated to request 
                context.Response.StatusCode = 401;
            }

            // Pass to next middleware
            await _next(context);
        }
    }
}
