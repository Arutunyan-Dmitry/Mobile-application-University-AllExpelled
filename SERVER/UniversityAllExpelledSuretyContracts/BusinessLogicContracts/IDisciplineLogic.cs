using System.Collections.Generic;
using UniversityAllExpelledSuretyContracts.BindingModel;
using UniversityAllExpelledSuretyContracts.ViewModels;

namespace UniversityAllExpelledSuretyContracts.BusinessLogicContracts
{
    public interface IDisciplineLogic
    {
        List<DisciplineViewModel> Read(DisciplineBindingModel model);

        void CreateOrUpdate(DisciplineBindingModel model);

        void Delete(DisciplineBindingModel model);
    }
}
