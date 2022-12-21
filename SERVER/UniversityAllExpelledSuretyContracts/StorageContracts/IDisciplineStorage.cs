using System.Collections.Generic;
using UniversityAllExpelledSuretyContracts.BindingModel;
using UniversityAllExpelledSuretyContracts.ViewModels;

namespace UniversityAllExpelledSuretyContracts.StorageContracts
{
    public interface IDisciplineStorage
    {
        List<DisciplineViewModel> GetFullList();

        List<DisciplineViewModel> GetFilteredList(DisciplineBindingModel model);

        DisciplineViewModel GetElement(DisciplineBindingModel model);

        void Insert(DisciplineBindingModel model);

        void Update(DisciplineBindingModel model);

        void Delete(DisciplineBindingModel model);
    }
}
