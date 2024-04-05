using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManger;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManger, ITokenRepository tokenRepository)
        {
            this.userManger = userManger;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            //Check email
            var identityUser = await userManger.FindByEmailAsync(request.Email);

            if (identityUser is not null)
            {
                //Check password
               var checkPasswordResult =  await userManger.CheckPasswordAsync(identityUser, request.Password);

               if (checkPasswordResult)
               {
                   var roles = await userManger.GetRolesAsync(identityUser);

                   //Create a token and response
                   var jwtToken = tokenRepository.CreateJwtToken(identityUser, roles.ToList());

                   var response = new LoginResponseDto(request.Email, jwtToken, roles.ToList());

                   return Ok(response);

               }
            }

            ModelState.AddModelError("", "Email or Password Incorrect");

            return ValidationProblem(ModelState);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequestDto request)
        {
            //Create the IdentityUser

            var user = new IdentityUser
            {
                UserName = request.Email?.Trim(),
                Email = request.Email?.Trim()
            };

            //Create user
            var identityResult = await userManger.CreateAsync(user, request.Password);

            if (identityResult.Succeeded)
            {
                //Add Role to user (reader)
                identityResult = await userManger.AddToRoleAsync(user, "Reader");

                if (identityResult.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    if (identityResult.Errors.Any())
                    {
                        foreach (var error in identityResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }
            else
            {
                if (identityResult.Errors.Any())
                {
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return ValidationProblem(ModelState);
        }
    }
}
