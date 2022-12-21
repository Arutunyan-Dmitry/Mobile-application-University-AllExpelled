using System.Collections.Generic;
using UniversityAllExpelledSuretyContracts.BindingModel;
using UniversityAllExpelledSuretyContracts.ViewModels;

namespace UniversityAllExpelledSuretyContracts.StorageContracts
{
    public interface ITypeReportingStorage
    {
        List<TypeReportingViewModel> GetFullList();

        List<TypeReportingViewModel> GetFilteredList(TypeReportingBindingModel model);

        TypeReportingViewModel GetElement(TypeReportingBindingModel model);

        void Insert(TypeReportingBindingModel model);

        void Update(TypeReportingBindingModel model);

        void Delete(TypeReportingBindingModel model);
    }
}
