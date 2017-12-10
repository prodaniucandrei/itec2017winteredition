using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UniversityAppApi.Auth.Models;

namespace UniversityAppApi.Auth.Factory
{
    public interface IJWTFactory
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity, IList<string> roles);

        Task<ClaimsIdentity> GenerateClaimsIdentity(UniversityUserModel user);
    }
}
