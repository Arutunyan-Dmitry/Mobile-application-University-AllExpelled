using Microsoft.AspNetCore.Mvc;
using UniversityAllExpelledExecutorContracts.BindingModels;
using UniversityAllExpelledExecutorContracts.BusinessLogicContracts;
using UniversityAllExpelledExecutorContracts.ViewModels;

namespace UniversityAllExpelledExecutorRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherLogic _logic;
        public TeacherController(ITeacherLogic logic)
        {
            _logic = logic;
        }
        [HttpGet]
        public TeacherViewModel Login(string login, string password)
        {
            var list = _logic.Read(new TeacherBindingModel
            {
                Email = login,
                Password = password
            });
            return (list != null && list.Count > 0) ? list[0] : null;
        }
        [HttpPost]
        public void Register(TeacherBindingModel model) =>
        _logic.CreateOrUpdate(model);
    }
}
