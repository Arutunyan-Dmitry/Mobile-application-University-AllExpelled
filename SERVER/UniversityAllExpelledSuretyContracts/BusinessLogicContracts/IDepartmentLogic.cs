using System.Collections.Generic;
using UniversityAllExpelledSuretyContracts.BindingModel;
using UniversityAllExpelledSuretyContracts.ViewModels;

namespace UniversityAllExpelledSuretyContracts.BusinessLogicContracts
{
    public interface IDepartmentLogic
    {
        List<DepartmentViewModel> Read(DepartmentBindingModel model);

        void CreateOrUpdate(DepartmentBindingModel model);

        void Delete(DepartmentBindingModel model);
        int CheckPassword(string login, string password);
    }
}
