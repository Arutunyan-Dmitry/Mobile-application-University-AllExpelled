using System.Collections.Generic;
using UniversityAllExpelledSuretyContracts.BindingModel;
using UniversityAllExpelledSuretyContracts.ViewModels;

namespace UniversityAllExpelledSuretyContracts.StorageContracts
{
    public interface IDepartmentStorage
    {
        List<DepartmentViewModel> GetFullList();

        List<DepartmentViewModel> GetFilteredList(DepartmentBindingModel model);

        DepartmentViewModel GetElement(DepartmentBindingModel model);

        void Insert(DepartmentBindingModel model);

        void Update(DepartmentBindingModel model);

        void Delete(DepartmentBindingModel model);
    }
}
