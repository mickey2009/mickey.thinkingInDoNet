using Mickey.Core.ComponentModel;
using System.Security.Claims;

namespace Mickey.Web.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetClaimTypeValue(this ClaimsPrincipal principal, string type)
        {
            Requires.NotNull(principal, "principal");
            Claim claim1 = principal.FindFirst(type);
            if (claim1 == null)
            {
                throw new ClaimNotFoundException("无法找到指定的[" + type + "]类型");
            }
            return claim1.Value;
        }
    }
}
