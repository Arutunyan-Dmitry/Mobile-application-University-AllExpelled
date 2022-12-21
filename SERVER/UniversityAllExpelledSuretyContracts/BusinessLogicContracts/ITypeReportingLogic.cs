using System.Collections.Generic;
using UniversityAllExpelledSuretyContracts.BindingModel;
using UniversityAllExpelledSuretyContracts.ViewModels;

namespace UniversityAllExpelledSuretyContracts.BusinessLogicContracts
{
    public interface ITypeReportingLogic
    {
        List<TypeReportingViewModel> Read(TypeReportingBindingModel model);

        void CreateOrUpdate(TypeReportingBindingModel model);

        void Delete(TypeReportingBindingModel model);
    }
}
