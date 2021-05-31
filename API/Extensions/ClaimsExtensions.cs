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
            int res = -1;
            int.TryParse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value, out res);
            return res;
        }
        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            bool res = false;
            bool.TryParse(user.FindFirst("isadmin")?.Value, out res);
            return res;
        }
    }
}