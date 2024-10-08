using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repository
{
    public interface ITokenRespository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
