using Microsoft.AspNetCore.Mvc;
using UniversityAllExpelledExecutorBusinessLogic.MailWorker;
using UniversityAllExpelledExecutorContracts.BindingModels;
using UniversityAllExpelledExecutorContracts.BusinessLogicContracts;
using UniversityAllExpelledExecutorContracts.ViewModels;

namespace UniversityAlExpelledRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly IReportLogic _reportLogic;
        private readonly AbstractMailWorker _mailWorker;

        public ReportController(IReportLogic reportLogic, AbstractMailWorker mailWorker)
        {
            _reportLogic = reportLogic;
            _mailWorker = mailWorker;
        }

        [HttpGet]
        public List<ReportStudentDisciplineViewModel> GetStudentDisciplines(string students) => _reportLogic.GetStudentDisciplines(new ReportBindingModel
        {
            Parametrs = students
        });
        [HttpGet]
        public List<ReportStudentStatementTestingViewModel> GetStudentStatementTestings(string dates) => _reportLogic.GetStudentStatementTestings(new ReportBindingModel
        {
            Parametrs = dates
        });
        [HttpPost]
        public void SaveStudentDisciplinesToWordFile(ReportBindingModel model) => _reportLogic.SaveStudentDisciplinesToWordFile(model);
        [HttpPost]
        public void SaveStudentDisciplinesToExcelFile(ReportBindingModel model) => _reportLogic.SaveStudentDisciplinesToExcelFile(model);
        [HttpPost]
        public void SaveStudentStatementTestingsToPdfFile(ReportBindingModel model) => _reportLogic.SaveStudentStatementTestingsToPdfFile(model);
        [HttpPost]
        public void SendMessage(MailSendInfoBindingModel model) => _mailWorker.MailSendAsync(model);
    }
}
