using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAppApi.Auth.Models;
using UniversityAppApi.Controllers.Generic;
using UniversityAppApi.Models;
using UniversityAppApi.Repositories;
using UniversityAppApi.Repositories.Generic;
using UniversityAppApi.ViewModels;

namespace UniversityAppApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ProfesorController : GenericController<ProfesorModel, ProfesorViewModel>
    {
        public ProfesorController(BLLUnitOfWork bll, UserManager<UniversityUserModel> userManager) : base(bll, userManager)
        {
        }

        protected override GenericRepository<ProfesorModel> GetRepository(BLLUnitOfWork bll)
        {
            return bll.ProfesorRepository;
        }
    }
}
