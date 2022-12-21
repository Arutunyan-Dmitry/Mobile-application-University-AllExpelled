using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UniversityAllExpelledExecutorContracts.BusinessLogicContracts;
using UniversityAllExpelledExecutorContracts.ViewModels;

namespace UniversityAllExpelledExecutorRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : Controller
    {
        private readonly ITeacherLogic _teacher;

        public TestController(ITeacherLogic teacher)
        {
            _teacher = teacher;
        }
        [HttpGet]
        public List<TeacherViewModel> GetAllTeachers() => _teacher.Read(null);
    }
}
