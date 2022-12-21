using UniversityAllExpelledExecutorContracts.BindingModels;
using UniversityAllExpelledExecutorContracts.ViewModels;
using System.Collections.Generic;


namespace UniversityAllExpelledExecutorContracts.BusinessLogicContracts
{
    public interface IStatementLogic
    {
        List<StatementViewModel> Read(StatementBindingModel model);
        void CreateOrUpdate(StatementBindingModel model);
        void Delete(StatementBindingModel model);
        void AddStudent(StudentStatementBindingModel model);
        public void DeleteStudent(StudentStatementBindingModel model);
    }
}
