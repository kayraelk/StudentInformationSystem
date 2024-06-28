using Microsoft.AspNetCore.Mvc;
using StudentInformationSystem.Authorization;

namespace StudentInformationSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly PasswordCheckService _passwordCheckService;

        public AccountController(PasswordCheckService passwordCheckService)
        {
            _passwordCheckService = passwordCheckService;
        }

        [HttpGet]
        public async Task<IActionResult> VerifyPassword(string email, string password)
        {
            bool isPasswordCorrect = await _passwordCheckService.CheckPasswordAsync(email, password);

            if (isPasswordCorrect)
            {
                return Ok("Password is correct");
            }
            else
            {
                return BadRequest("Password is incorrect");
            }
        }
    }

}
