using AuthApi.IServices;
using AuthApi.Models;

namespace AuthApi.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtOptionsModel _jwtOptionsModel;

        public JwtTokenGenerator(JwtOptionsModel jwtOptionsModel)
        {
            _jwtOptionsModel = jwtOptionsModel;
        }

        public string GenerateToken(ApplicationUser applicationUser)
        {
            throw new NotImplementedException();
        }
    }
}
