using UniversityAllExpelledExecutorContracts.BindingModels;
using UniversityAllExpelledExecutorContracts.ViewModels;
using System.Collections.Generic;

namespace UniversityAllExpelledExecutorContracts.BusinessLogicContracts
{
    public interface ITestingLogic
    {
        List<TestingViewModel> Read(TestingBindingModel model);
        void CreateOrUpdate(TestingBindingModel model);
        void Delete(TestingBindingModel model);
        void AddStudent(StudentTestingBindingModel model);
        public void DeleteStudent(StudentTestingBindingModel model);
    }
}
