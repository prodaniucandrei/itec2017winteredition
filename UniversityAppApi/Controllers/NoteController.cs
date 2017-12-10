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
    public class NoteController: GenericController<NoteModel, NoteViewModel>
    {
        public NoteController(BLLUnitOfWork bll, UserManager<UniversityUserModel> userManager) : base(bll, userManager)
        {
        }

        protected override GenericRepository<NoteModel> GetRepository(BLLUnitOfWork bll)
        {
            return bll.NoteRepository;
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetAllBySubject(int id)
        {
            var resultModel = await Bll.NoteRepository.GetAllBySubject(id);
            if (resultModel == null)
                return NotFound();
            var resultViewModel = Mapper.Map<List<NoteViewModel>>(resultModel);
            return Ok(resultViewModel);
        }
    }
}
