using SampleJWT.Dto;

namespace SampleJWT.Security
{
    public interface IAuthenticationProvider
    {
        bool Login(LoginDto information, out string token, out string message);
        bool Refresh(string existingToken, out string newToken, out string message);
    }
}