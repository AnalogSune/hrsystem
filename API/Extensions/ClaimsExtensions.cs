using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace API.Extensions
{
    public static class ClaimsExtensions
    {
        public static string GetEmail(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name)?.Value;
        }
        public static int GetId(this ClaimsPrincipal user)
        {
            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return bool.Parse(user.FindFirst("isadmin")?.Value);
        }
    }
}