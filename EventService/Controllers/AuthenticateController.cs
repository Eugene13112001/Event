using EventService.Options;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static IdentityServer4.Models.IdentityResources;

namespace EventService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthenticateController : ControllerBase
    {

        public IdentityServerOptions IdentityServerOptions { get; }

        public AuthenticateController(IOptions<IdentityServerOptions> identityServerOptions)
        {
            IdentityServerOptions = identityServerOptions.Value;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("login/{userName}&{password}")]
        public async Task<IActionResult> Login([FromRoute] string userName, string password)
        {
            if (password is null)
                throw new ArgumentNullException(nameof(password));

            if (userName is null)
                throw new ArgumentNullException(nameof(userName));

            var httpClient = new HttpClient();
            TokenResponse tokenResponse = await httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = $"{IdentityServerOptions.Address}connect/token",
                ClientId = IdentityServerOptions.ClientId,
                ClientSecret = IdentityServerOptions.ClientSecret,
                Scope = IdentityServerOptions.Scope,
                UserName = userName,
                Password = password
            });

            if (!tokenResponse.IsError)
            {
                Response.Headers.Add("x-access-token", tokenResponse.AccessToken);
                return Ok();
            }
            else
            {
                return BadRequest($"Failed to login with error:\n{tokenResponse.Error}\n{tokenResponse.ErrorDescription}");
            }
        }

        [HttpGet]
        [Route("stub/authstub")]
        public IActionResult CheckAuthenticate()
        {
            return Ok("Вы успешно попали в закрытую область сервиса.");
        }
    }
}
