using Microsoft.AspNetCore.Mvc;
using UniversityAllExpelledExecutorContracts.BindingModels;
using UniversityAllExpelledExecutorContracts.BusinessLogicContracts;
using UniversityAllExpelledExecutorContracts.ViewModels;
using UniversityAllExpelledSuretyContracts.BusinessLogicContracts;
using UniversityAllExpelledSuretyContracts.ViewModels;

namespace UniversityAlExpelledRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IStudentLogic _student;
        private readonly IStatementLogic _statement;
        private readonly ITestingLogic _testing;
        private readonly ILessonLogic _lesson;

        public MainController(IStudentLogic student, IStatementLogic statement, ITestingLogic testing,
            ILessonLogic lesson)
        {
            _student = student;
            _statement = statement;
            _testing = testing;
            _lesson = lesson;
        }
        //-------------------------------------- Full lists ------------------------------------------------------

        [HttpGet]
        public List<LessonViewModel> GetAllLessons() => _lesson.Read(null);

        //------------------------------------ Filtered Lists ----------------------------------------------------

        [HttpGet]
        public List<StudentViewModel> GetStudents(int teacherId) => _student.Read(new StudentBindingModel
        { TeacherId = teacherId });
        [HttpGet]
        public List<StatementViewModel> GetStatements(int teacherId) => _statement.Read(new StatementBindingModel
        { TeacherId = teacherId });
        [HttpGet]
        public List<TestingViewModel> GetTestings(int teacherId) => _testing.Read(new TestingBindingModel
        { TeacherId = teacherId });

        //--------------------------------------- Element -----------------------------------------------------

        [HttpGet]
        public StudentViewModel GetStudent(int studentId) => _student.Read(new StudentBindingModel
        { Id = studentId })?[0];
        [HttpGet]
        public StatementViewModel GetStatement(int statementId) => _statement.Read(new StatementBindingModel
        { Id = statementId })?[0];
        [HttpGet]
        public TestingViewModel GetTesting(int testingId) => _testing.Read(new TestingBindingModel
        { Id = testingId })?[0];

        //--------------------------------------- CU -----------------------------------------------------

        [HttpPost]
        public void CreateOrUpdateStudent(StudentBindingModel model) => _student.CreateOrUpdate(model);
        [HttpPost]
        public void CreateOrUpdateStatement(StatementBindingModel model) => _statement.CreateOrUpdate(model);
        //-------------- Put --------------
        [HttpPost]
        public void AddStudentToStatement(StudentStatementBindingModel model) => _statement.AddStudent(model);
        [HttpPost]
        public void DeleteStudentFromStatement(StudentStatementBindingModel model) => _statement.DeleteStudent(model);
        //---------------------------------
        [HttpPost]
        public void CreateOrUpdateTesting(TestingBindingModel model) => _testing.CreateOrUpdate(model);
        //-------------- Put --------------
        [HttpPost]
        public void AddStudentToTesting(StudentTestingBindingModel model) => _testing.AddStudent(model);
        [HttpPost]
        public void DeleteStudentFromTesting(StudentTestingBindingModel model) => _testing.DeleteStudent(model);
        //---------------------------------

        //--------------------------------------- D -----------------------------------------------------

        [HttpPost]
        public void DeleteStudent(StudentBindingModel model) => _student.Delete(model);
        [HttpPost]
        public void DeleteStatement(StatementBindingModel model) => _statement.Delete(model);
        [HttpPost]
        public void DeleteTesting(TestingBindingModel model) => _testing.Delete(model);
    }
}
