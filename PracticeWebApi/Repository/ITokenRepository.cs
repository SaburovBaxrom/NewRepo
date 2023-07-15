using Microsoft.AspNetCore.Identity;

namespace PracticeWebApi.Repository;

public interface ITokenRepository
{
	string CreatJwtToken(IdentityUser user, List<String> roles);
}
