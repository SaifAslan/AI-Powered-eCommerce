using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class AccountRepository : IAccountRepository

    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountRepository(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }
        public async Task<LoginResult> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return LoginResult.Failure("Email not found!", 404);

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return LoginResult.Failure("Invalid credentials!", 401);

            return LoginResult.Success(user);
        }

        public async Task<RegisterResult> RegisterAsync(RegisterDto registerDto)
        {

            var appUser = new AppUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                DateOfBirth = registerDto.DateOfBirth,
                UpdatedAt = DateTime.Now.Date,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                PhoneNumber = registerDto.PhoneNumber
            };

            var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);
            if (createdUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                if (roleResult.Succeeded)
                {
                    return RegisterResult.Success(new NewUserDto
                    {
                        UserName = appUser.UserName,
                        Email = appUser.Email,
                        Token = _tokenService.CreateToken(appUser)
                    });
                }
                else
                {
                    return RegisterResult.Failure(roleResult.Errors, 500);

                }
            }
            else
            {
                return RegisterResult.Failure(createdUser.Errors, 500);
            }
        }
    }
}