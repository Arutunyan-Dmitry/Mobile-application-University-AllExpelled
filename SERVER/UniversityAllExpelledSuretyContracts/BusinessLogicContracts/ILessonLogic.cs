using System.Collections.Generic;
using UniversityAllExpelledSuretyContracts.BindingModel;
using UniversityAllExpelledSuretyContracts.ViewModels;

namespace UniversityAllExpelledSuretyContracts.BusinessLogicContracts
{
    public interface ILessonLogic
    {
        List<LessonViewModel> Read(LessonBindingModel model);

        void CreateOrUpdate(LessonBindingModel model);

        void Delete(LessonBindingModel model);
    }
}
