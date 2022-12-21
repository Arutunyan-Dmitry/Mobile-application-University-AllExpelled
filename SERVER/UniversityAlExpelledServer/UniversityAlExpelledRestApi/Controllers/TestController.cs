using Microsoft.AspNetCore.Mvc;
using UniversityAllExpelledExecutorContracts.BusinessLogicContracts;
using UniversityAllExpelledExecutorContracts.ViewModels;

namespace UniversityAlExpelledRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : Controller
    {
        private readonly IStudentLogic _student;
        private readonly IStatementLogic _statement;
        private readonly ITestingLogic _testing;
        private readonly ITeacherLogic _teacher;

        public TestController(IStudentLogic student, IStatementLogic statement,
            ITestingLogic testing, ITeacherLogic teacher)
        {
            _student = student;
            _statement = statement;
            _testing = testing;
            _teacher = teacher;
        }
        [HttpGet]
        public List<StudentViewModel> GetAllStudents() => _student.Read(null);
        [HttpGet]
        public List<StatementViewModel> GetAllStatements() => _statement.Read(null);
        [HttpGet]
        public List<TestingViewModel> GetAllTestings() => _testing.Read(null);
        [HttpGet]
        public List<TeacherViewModel> GetAllTeachers() => _teacher.Read(null);
    }
}
