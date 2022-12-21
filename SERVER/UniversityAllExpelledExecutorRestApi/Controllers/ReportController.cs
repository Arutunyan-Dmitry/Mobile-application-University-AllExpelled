using UniversityAllExpelledExecutorContracts.BusinessLogicContracts;
using UniversityAllExpelledExecutorContracts.BindingModels;
using UniversityAllExpelledExecutorContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UniversityAllExpelledExecutorBusinessLogic.MailWorker;

namespace UniversityAllExpelledExecutorRestApi.Controllers
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

        [HttpPost]
        public void SendMessage(MailSendInfoBindingModel model) => _mailWorker.MailSendAsync(model);
    }
}
