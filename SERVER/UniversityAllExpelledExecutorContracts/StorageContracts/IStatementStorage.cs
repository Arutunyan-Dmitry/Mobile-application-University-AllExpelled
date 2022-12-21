using UniversityAllExpelledExecutorContracts.BindingModels;
using UniversityAllExpelledExecutorContracts.ViewModels;
using System.Collections.Generic;

namespace UniversityAllExpelledExecutorContracts.StorageContracts
{
    public interface IStatementStorage
    {
        List<StatementViewModel> GetFullList();
        List<StatementViewModel> GetFilteredList(StatementBindingModel model);
        StatementViewModel GetElement(StatementBindingModel model);
        void Insert(StatementBindingModel model);
        void Update(StatementBindingModel model);
        void Delete(StatementBindingModel model);
    }
}
