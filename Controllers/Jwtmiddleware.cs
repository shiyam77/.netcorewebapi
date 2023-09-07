using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;

namespace WebApidotnetcore.Controllers
{
    public class Jwtmiddleware
    {
        private readonly RequestDelegate _next;

        public Jwtmiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check if the request contains an Authorization header
            if (context.Request.Headers.ContainsKey("Authorization"))
            {
                var tokenString = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                // Decode and log the token
                LogDecodedToken(tokenString);
            }

            // Call the next middleware in the pipeline
            await _next(context);
        }

        private void LogDecodedToken(string tokenString)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(tokenString);

            // Access claims
            var name = token.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
            var role = token.Claims.FirstOrDefault(c => c.Type == "role")?.Value;

            // Log the decoded token information
            // You can replace this with your logging mechanism
            Console.WriteLine($"Decoded token - Name: {name}, Role: {role}");
        }
    }
}
