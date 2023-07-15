using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PracticeWebApi.Model.DTO;
using PracticeWebApi.Repository;

namespace PracticeWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly ITokenRepository tokenRepo;  
		public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
		{
			_userManager= userManager;
			tokenRepo= tokenRepository;
		}
		[HttpPost]
		[Route("Register")]

		public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
		{
			var identityUser = new IdentityUser
			{
				UserName = registerDto.UserName,
				Email = registerDto.UserName
			};
			var identityResult = await _userManager.CreateAsync(identityUser, registerDto.Password);

			if (identityResult.Succeeded)
			{
				if (registerDto.Roles != null && registerDto.Roles.Any())
				{
					identityResult = await _userManager.AddToRolesAsync(identityUser,registerDto.Roles);

					if(identityResult.Succeeded)
					{
						return Ok("User was registered. Please Login in");
					}
				}
			}

			return BadRequest("something went wrong");
		}

		[HttpPost]
		[Route("Login")]
		public async Task<IActionResult> Login([FromBody] LoginDto loginDto )
		{
			var user = await _userManager.FindByEmailAsync(loginDto.UserName);

			if(user != null) 
			{
				var checkPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);

				if (checkPassword)
				{
					var roles = await _userManager.GetRolesAsync(user);

					if (roles != null)
					{
						var token = tokenRepo.CreatJwtToken(user, roles.ToList());
						var response = new TokenDto
						{
							JwtToken = token
						};
						return Ok(response);
					};
				}
			}

			return BadRequest("username or password incorrect");
		}
	}
}
