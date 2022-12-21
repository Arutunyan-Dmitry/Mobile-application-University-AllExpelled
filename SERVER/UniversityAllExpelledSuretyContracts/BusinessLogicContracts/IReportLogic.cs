using System;
using System.Collections.Generic;
using UniversityAllExpelledSuretyContracts.BindingModel;
using UniversityAllExpelledSuretyContracts.ViewModels;

namespace UniversityAllExpelledSuretyContracts.BusinessLogicContracts
{
    public interface IReportLogic
    {
        List<ReportDisciplineLessonViewModel> GetDisciplineLesson(ReportBindingModel model);
        List<ReportTestingTypeReportingViewModel> GetTestingTypeReporting(ReportTestingTypeReportingBindingModel model);
        /// <summary>
        /// Сохранение в файл-Word
        /// </summary>
        /// <param name="model"></param>
        void SaveTestingTypeReportingToWordFile(ReportTestingTypeReportingBindingModel model);
        /// <summary>
        /// Сохранение в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        void SaveTestingTypeReportingExcelFile(ReportTestingTypeReportingBindingModel model);       
        /// <summary>
        /// Сохранение в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        void SaveDisciplineLessonToPdfFile(ReportBindingModel model);
    }
}
