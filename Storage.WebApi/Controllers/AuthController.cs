using Microsoft.AspNetCore.Mvc;
using Storage.BLL.DTO.User;
using Storage.BLL.Services.Interfaces;
using System.Net;

namespace Storage.WebApi.Controllers
{
	[ApiController]
	[Route("/api/auth")]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("login")]
		public async Task<ActionResult> Login([FromBody] UserLoginDto userDto)
		{
			try
			{
				var user = await _authService.Login(userDto);
				if (user == null)
				{
					return BadRequest();
				}

				return Ok(user);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
		}

		[HttpPost("register")]
		public async Task<ActionResult> Register([FromBody] UserRegisterDto userDto)
		{
			try
			{
				var user = await _authService.Register(userDto);
				if (user == null)
				{
					return BadRequest();
				}

				return Ok(user);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
		}
	}
}
