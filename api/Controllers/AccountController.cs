using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Interfaces;
using api.Models;
using api.Repositories;
using api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITokenService _tokenService;
        public AccountController(IAccountRepository accountRepository, ITokenService tokenService)
        {
            _accountRepository = accountRepository;
            _tokenService = tokenService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var loginResult = await _accountRepository.LoginAsync(loginDto);

            if (loginResult.IsSuccess)
            {
                return Ok(new NewUserDto
                {
                    Email = loginResult.User.Email,
                    Token = _tokenService.CreateToken(loginResult.User),
                    UserName = loginResult.User.UserName
                });
            }

            if (loginResult.StatusCode == 401) return Unauthorized(loginResult.ErrorMessage);
            return NotFound(loginResult.ErrorMessage);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var registerResult = await _accountRepository.RegisterAsync(registerDto);
                if (registerResult.IsSuccess)
                {
                    return Ok(registerResult.User);
                }
                return StatusCode(registerResult.StatusCode, registerResult.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

    }
}