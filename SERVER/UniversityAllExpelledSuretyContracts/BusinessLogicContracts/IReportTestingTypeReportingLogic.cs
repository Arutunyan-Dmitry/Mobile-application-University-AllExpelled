using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityAllExpelledSuretyContracts.BindingModel;
using UniversityAllExpelledSuretyContracts.ViewModels;

namespace UniversityAllExpelledSuretyContracts.BusinessLogicContracts
{
    public interface IReportTestingTypeReportingLogic
    {
        List<ReportTestingTypeReportingViewModel> GetTestingTypeReporting();
        /// <summary>
        /// Сохранение в файл-Word
        /// </summary>
        /// <param name="model"></param>
        void SaveDetailsToWordFile(ReportTestingTypeReportingBindingModel model);
        /// <summary>
        /// Сохранение в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        void SaveCarDetailToExcelFile(ReportTestingTypeReportingBindingModel model);
    }
}
