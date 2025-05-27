using HiFive.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HiFive.Presentation;

public class JwtService
{
	private readonly Jwt _jwtOptions;
	private readonly UserManager<ApplicationUser> _userManager;

	public JwtService(IOptions<Jwt> options, UserManager<ApplicationUser> userManager)
	{
		_jwtOptions = options.Value;
		_userManager = userManager;
	}

	public async Task<string> GenerateToken(ApplicationUser user)
	{
		var roles = await _userManager.GetRolesAsync(user);

		var claims = new List<Claim>
		{
			new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
			new Claim(JwtRegisteredClaimNames.Email, user.Email!),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
		};

		claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			issuer: _jwtOptions.Issuer,
			audience: _jwtOptions.Audience,
			claims: claims,
			expires: DateTime.Now.AddHours(2),
			signingCredentials: creds
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
