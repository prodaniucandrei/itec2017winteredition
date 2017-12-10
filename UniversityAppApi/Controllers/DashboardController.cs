using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAppApi.Auth.Models;
using UniversityAppApi.Repositories;
using UniversityAppApi.ViewModels;

namespace UniversityAppApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class DashboardController: Controller
    {
        BLLUnitOfWork _bLLUnitOfWork;
        UserManager<UniversityUserModel> _userManager;

        public DashboardController(BLLUnitOfWork bll, UserManager<UniversityUserModel> userManager)
        {
            _bLLUnitOfWork = bll;
            _userManager = userManager; 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDashboard(string id)
        {
            var student = (await _bLLUnitOfWork.StudentRepository.GetAllAsync()).FirstOrDefault(a => a.UserId == id);

            if (student != null)
            {
                var dashbard = new DashboardViewModel();
                dashbard.Student = Mapper.Map<StudentViewModel>(student);

                var dateNumber = ConvertDaytoInt(DateTime.Now.DayOfWeek);
                var subjects = (await _bLLUnitOfWork.SubjectRepository.GetAllAsync()).Where(
                    a => a.Facultate == student.Facultate
                        && a.Serie == student.Sectie
                        && a.Date == dateNumber).OrderBy(a => a.StartTime);
                dashbard.Subjects = Mapper.Map<List<SubjectViewModel>>(subjects);

                return Ok(dashbard);
            }
            return NotFound();
        }

        private int ConvertDaytoInt(DayOfWeek d)
        {
            switch(d)
            {
                case DayOfWeek.Monday: return 1; break;
                case DayOfWeek.Tuesday: return 2; break;
                case DayOfWeek.Wednesday: return 3; break;
                case DayOfWeek.Thursday: return 4; break;
                case DayOfWeek.Friday: return 5; break;
                case DayOfWeek.Saturday: return 6; break;
                case DayOfWeek.Sunday: return 7; break;
            }
            return 0;
        }
    }
}
