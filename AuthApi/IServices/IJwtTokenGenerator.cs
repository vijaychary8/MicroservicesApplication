using AuthApi.Models;

namespace AuthApi.IServices
{
    public interface IJwtTokenGenerator
    {
        public string GenerateToken(ApplicationUser applicationUser);
    }
}
