using AuthApi.Model;

namespace AuthApi.Service
{
    public interface ITokengenerate
    {
        string TokenGenerator(ApplicationUser applicationUser , IEnumerable<string> Roles);
    }
}
