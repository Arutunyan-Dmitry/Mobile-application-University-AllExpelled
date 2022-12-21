using UniversityAllExpelledExecutorContracts.BindingModels;
using UniversityAllExpelledExecutorContracts.ViewModels;
using System.Collections.Generic;

namespace UniversityAllExpelledExecutorContracts.BusinessLogicContracts
{
    public interface ITeacherLogic
    {
        List<TeacherViewModel> Read(TeacherBindingModel model);
        void CreateOrUpdate(TeacherBindingModel model);
        void Delete(TeacherBindingModel model);
    }
}
