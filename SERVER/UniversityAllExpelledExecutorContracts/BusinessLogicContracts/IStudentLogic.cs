using UniversityAllExpelledExecutorContracts.BindingModels;
using UniversityAllExpelledExecutorContracts.ViewModels;
using System.Collections.Generic;

namespace UniversityAllExpelledExecutorContracts.BusinessLogicContracts
{
    public interface IStudentLogic
    {
        List<StudentViewModel> Read(StudentBindingModel model);
        void CreateOrUpdate(StudentBindingModel model);
        void Delete(StudentBindingModel model);
    }
}
