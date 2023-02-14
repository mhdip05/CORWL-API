using System.Security.Claims;

namespace NMS_API_N.Extension
{
    public static class ClaimPrincipleExtension
    {
        public static string GetUserName(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name)!.Value;
        }

        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        }

        public static string GetEmployeeId(this ClaimsPrincipal emp)
        {
            return emp.FindFirst(ClaimTypes.Sid)!.Value;
        }

    }
}
