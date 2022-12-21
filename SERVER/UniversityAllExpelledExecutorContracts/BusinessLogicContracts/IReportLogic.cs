using System.Collections.Generic;
using UniversityAllExpelledExecutorContracts.BindingModels;
using UniversityAllExpelledExecutorContracts.ViewModels;

namespace UniversityAllExpelledExecutorContracts.BusinessLogicContracts
{
    public interface IReportLogic
    {
        /// <summary>
        /// Получение списка дисциплин на основе выбранных студентов
        /// </summary>
        /// <returns></returns>
        List<ReportStudentDisciplineViewModel> GetStudentDisciplines(ReportBindingModel model);
        /// <summary>
        /// Получение списка студентов за определенный период с результатами испытаний и ведомостями
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<ReportStudentStatementTestingViewModel> GetStudentStatementTestings(ReportBindingModel model);
        /// <summary>
        /// Сохранение дисциплин на основе выбранных студентов в файл-Word
        /// </summary>
        /// <param name="model"></param>
        void SaveStudentDisciplinesToWordFile(ReportBindingModel model);
        /// <summary>
        /// Сохранение дисциплин на основе выбранных студентов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        void SaveStudentDisciplinesToExcelFile(ReportBindingModel model);
        /// <summary>
        /// Сохранение студентов в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        void SaveStudentStatementTestingsToPdfFile(ReportBindingModel model);
    }
}
