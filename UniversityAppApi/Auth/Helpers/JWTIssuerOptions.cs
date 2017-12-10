using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAppApi.Auth.Helpers
{
    public class JWTIssuerOptions
    {
        public string Issuer { get; set; }
        public string Subject { get; set; }
        public string Audience { get; set; }
        public DateTime Expiration => IssuedAt.Add(ValidFor);
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
        public DateTime NotBefore { get; set; } = DateTime.UtcNow;
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(360);

        public Func<Task<string>> JTIGenerator => () => Task.FromResult(Guid.NewGuid().ToString());

        public SigningCredentials SignInCredentials { get; set; }
    }
}
