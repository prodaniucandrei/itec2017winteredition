using AutoMapper;
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
    public class PrezentaController : GenericController<PrezentaModel, PrezentaViewModel>
    {
        public PrezentaController(BLLUnitOfWork bll, UserManager<UniversityUserModel> userManager) : base(bll, userManager)
        {
        }

        protected override GenericRepository<PrezentaModel> GetRepository(BLLUnitOfWork bll)
        {
            return bll.PrezentaRepository;
        }
    }
}
