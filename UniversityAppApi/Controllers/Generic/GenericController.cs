using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UniversityAppApi.Auth.Models;
using UniversityAppApi.Models;
using UniversityAppApi.Repositories;
using UniversityAppApi.Repositories.Generic;
using UniversityAppApi.ViewModels;

namespace UniversityAppApi.Controllers.Generic
{
    [Route("api/[controller]/[action]")]
    public abstract class GenericController<TM, TVm> : Controller, IGenericController<TM, TVm>
        where TM : BaseModel where TVm : BaseViewModel
    {
        protected BLLUnitOfWork Bll;
        protected UserManager<UniversityUserModel> UserManager;
        protected ClaimsPrincipal ClaimsPrincipal;

        protected GenericController(BLLUnitOfWork bll, UserManager<UniversityUserModel> userManager)
        {
            Bll = bll;
            UserManager = userManager;
        }

        protected abstract GenericRepository<TM> GetRepository(BLLUnitOfWork bll);

        private UniversityUserModel _currentUser = null;
        protected UniversityUserModel CurrentUser => _currentUser ?? (_currentUser = GetCurrentUser().Result);


        [HttpGet]
        public virtual async Task<IActionResult> GetAll()
        {
            var resultModel = await GetRepository(Bll).GetAllAsync();
            var resultViewModel = Mapper.Map<List<TVm>>(resultModel);
            return Ok(resultViewModel);
        }


        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(int id)
        {
            var resultModel = await GetRepository(Bll).GetAsync(id);
            if (resultModel == null)
                return NotFound();
            var resultViewModel = Mapper.Map<TVm>(resultModel);
            return Ok(resultViewModel);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Add([FromBody] TVm vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var model = Mapper.Map<TM>(vm);
            model = await GetRepository(Bll).InsertAsync(model);
            var resultViewModel = Mapper.Map<TVm>(model);
            return Ok(resultViewModel);
        }


        [HttpPut]
        public virtual async Task<IActionResult> Update([FromBody] TVm vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var model = Mapper.Map<TM>(vm);
            model = await GetRepository(Bll).UpdateAsync(model);
            if (model == null)
                return NotFound();
            var resultViewModel = Mapper.Map<TVm>(model);
            return Ok(resultViewModel);
        }


        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var model = await GetRepository(Bll).GetAsync(id);
            if (model != null)
            {
                await GetRepository(Bll).DeleteAsync(model);
                return Ok();
            }
            return NotFound();
        }

        private async Task<UniversityUserModel> GetCurrentUser()
        {
            try
            {
                if (User.Identity is ClaimsIdentity identity)
                {
                    var userName = !identity.IsAuthenticated
                        ? null
                        : identity.Claims.FirstOrDefault(claim => claim.Type.Contains("nameidentifier"))?.Value;
                    return await UserManager.FindByEmailAsync(userName);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }
    }
}
