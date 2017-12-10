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
    public class StudentController : GenericController<StudentModel, StudentViewModel>
    {
        public StudentController(BLLUnitOfWork bll, UserManager<UniversityUserModel> userManager) : base(bll, userManager)
        {
        }

        protected override GenericRepository<StudentModel> GetRepository(BLLUnitOfWork bll)
        {
            return bll.StudentRepository;
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetAll(string id)
        {
            var student = (await Bll.StudentRepository.GetAllAsync()).FirstOrDefault(a => a.UserId == id);

            if (student != null)
            {
                var resultModel = await Bll.StudentRepository.GetAllAsync(student.Id);
                var resultViewModel = Mapper.Map<List<StudentViewModel>>(resultModel);
                return Ok(resultViewModel);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetAllBySubject(int id)
        {
            var studentList = new List<StudentModel>();
            var resultModel = await Bll.PrezentaRepository.GetAllBySubjectAsync(id);
            foreach(var stu in resultModel)
            {
                var student = await Bll.StudentRepository.GetAsync(stu.Student.Id);
                studentList.Add(student);
            }
            var resultViewModel = Mapper.Map<List<StudentViewModel>>(studentList);
            return Ok(resultViewModel);
        }
    }
}
