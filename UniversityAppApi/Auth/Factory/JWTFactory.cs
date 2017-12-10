using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using UniversityAppApi.Auth.Helpers;
using UniversityAppApi.Auth.Models;

namespace UniversityAppApi.Auth.Factory
{
    public class JWTFactory : IJWTFactory
    {
        private JWTIssuerOptions _jwtOptions;
        private UserManager<UniversityUserModel> _userManager;

        public JWTFactory(UserManager<UniversityUserModel> userManager, IOptions<JWTIssuerOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
            _userManager = userManager;
        }

        public async Task<ClaimsIdentity> GenerateClaimsIdentity(UniversityUserModel user)
        {
            return new ClaimsIdentity(new GenericIdentity(user.UserName, "Token"), new[]
            {
                new Claim("id", user.Id),
                new Claim("roles", (await _userManager.GetRolesAsync(user)).LastOrDefault())
            });
        }

        public async Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity, IList<string> roles)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JTIGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                identity.FindFirst("roles"),
                identity.FindFirst("id")
            };

            var jwt = new JwtSecurityToken(
                    issuer: _jwtOptions.Issuer,
                    audience: _jwtOptions.Audience,
                    claims: claims,
                    notBefore: _jwtOptions.NotBefore,
                    expires: _jwtOptions.Expiration,
                    signingCredentials: _jwtOptions.SignInCredentials
                );

            var encodedJWT = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJWT;
        }

        #region Private Helpers

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        #endregion
    }
}
