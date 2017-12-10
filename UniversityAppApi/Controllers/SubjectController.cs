using AutoMapper;
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
    [Authorize]
    public class SubjectController : GenericController<SubjectModel, SubjectViewModel>
    {
        public SubjectController(BLLUnitOfWork bll, UserManager<UniversityUserModel> userManager) : base(bll, userManager)
        {
        }

        protected override GenericRepository<SubjectModel> GetRepository(BLLUnitOfWork bll)
        {
            return bll.SubjectRepository;
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetAll(string id)
        {
            var student = (await Bll.StudentRepository.GetAllAsync()).FirstOrDefault(a => a.UserId == id);

            if (student != null)
            {
                var resultModel = await Bll.SubjectRepository.GetAllAsync(student.Id, true);
                var resultViewModel = Mapper.Map<List<SubjectViewModel>>(resultModel);
                return Ok(resultViewModel);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetAllUnconfirmed(string id)
        {
            var student = (await Bll.StudentRepository.GetAllAsync()).FirstOrDefault(a => a.UserId == id);

            if (student != null)
            {
                var resultModel = await Bll.SubjectRepository.GetAllAsync(student.Id, false);
                var resultViewModel = Mapper.Map<List<SubjectViewModel>>(resultModel);
                return Ok(resultViewModel);
            }
            return NotFound();
        }
    }
}
