using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace API.Extensions
{
    public static class ClaimsExtensions
    {
        public static string GetEmail(this ClaimsPrincipal user)
        {
            // System.Console.WriteLine("user.Claims.FirstOrDefault().Value");
            // System.Console.WriteLine(user.);
            return user.Claims.Where(c => c.Type == JwtRegisteredClaimNames.Name ).FirstOrDefault()?.Value;
        }
        public static int GetId(this ClaimsPrincipal user)
        {
            return int.Parse(user.Claims.FirstOrDefault().Value);
        }
        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return bool.Parse(user.FindFirst("isadmin")?.Value);
        }
    }
}