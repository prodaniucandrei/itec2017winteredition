using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAppApi.Models;
using UniversityAppApi.ViewModels;

namespace UniversityAppApi.Controllers.Generic
{
    public interface IGenericController<TM, TVm> where TM : BaseModel where TVm : BaseViewModel
    {
        Task<IActionResult> GetAll();
        Task<IActionResult> GetById(int id);
        Task<IActionResult> Add([FromBody] TVm vm);
        Task<IActionResult> Update([FromBody] TVm vm);
        Task<IActionResult> Delete(int id);
    }
}
