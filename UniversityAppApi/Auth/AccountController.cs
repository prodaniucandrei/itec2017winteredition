using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UniversityAppApi.Auth.Factory;
using UniversityAppApi.Auth.Helpers;
using UniversityAppApi.Auth.Models;
using UniversityAppApi.Auth.ViewModels;
using UniversityAppApi.Repositories;

namespace UniversityAppApi.Auth
{
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private UserManager<UniversityUserModel> _userManager;
        private IJWTFactory _jwtFactory;
        private JWTIssuerOptions _jwtOptions;
        private BLLUnitOfWork _bll;
        private IConfigurationRoot _config;

        public AccountController(UserManager<UniversityUserModel> userManager, IJWTFactory jwtFactory,
            IOptions<JWTIssuerOptions> jwtOptions, BLLUnitOfWork bll, IConfigurationRoot config)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
            _bll = bll;
            _config = config;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(vm.Username);
            if (user != null)
            {
                var identity = await GetClaimsIdentity(vm.Username, vm.Password, user);
                if (identity == null)
                {
                    return BadRequest("Email sau parolă invalidă.");
                }

                var roles = await _userManager.GetRolesAsync(user);

                var response = new
                {
                    id = user.Id,
                    email = vm.Username,
                    auth_token = await _jwtFactory.GenerateEncodedToken(vm.Username, identity, roles),
                    expires_in = (int)_jwtOptions.ValidFor.TotalSeconds,
                    roles = string.Join(",", roles)
                };

                var resultJson = JsonConvert.SerializeObject(response);
                return Ok(resultJson);
            }
            return BadRequest("Email sau parolă invalidă");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await UserExist(vm))
            {
                return BadRequest("Email-ul este deja folosit.");
            }

            var newUser = new UniversityUserModel
            {
                UserName = vm.Username,
                Email = vm.Username,
                EmailConfirmed = true
            };

            var resultUser = await _userManager.CreateAsync(newUser, vm.Password);
            if (!resultUser.Succeeded)
            {
                return BadRequest(resultUser.Errors);
            }
            var resultRole = await _userManager.AddToRoleAsync(newUser, "User");
            if (!resultRole.Succeeded)
            {
                return BadRequest(resultRole.Errors);
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

            return Ok(vm);
        }

        #region Private Helpers

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password, UniversityUserModel user)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                if (user != null)
                {
                    if (await _userManager.CheckPasswordAsync(user, password))
                    {
                        return await Task.FromResult(await _jwtFactory.GenerateClaimsIdentity(user));
                    }
                }
            }

            return await Task.FromResult<ClaimsIdentity>(null);
        }

        private async Task<bool> UserExist(RegisterViewModel vm)
        {
            var user = await _userManager.FindByEmailAsync(vm.Username);
            return (user != null);
        }

        #endregion
    }
}
