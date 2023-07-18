using System.Security.Claims;
using Newtonsoft.Json;
using PL.Entities.User;

namespace PL.MVC.Extensions;

public static class ClaimsPrincipalExtension
{
    public static User? GetUserData(this ClaimsPrincipal claimsPrincipal)
    {
        var userData = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.UserData);
        return userData == null ? null : JsonConvert.DeserializeObject<User>(userData.Value);
    }
}