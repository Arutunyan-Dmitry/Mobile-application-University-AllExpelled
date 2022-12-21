using System;
using System.Collections.Generic;
using UniversityAllExpelledExecutorContracts.BindingModels;
using UniversityAllExpelledExecutorContracts.ViewModels;
using UniversityAllExpelledExecutorContracts.BusinessLogicContracts;
using UniversityAllExpelledExecutorContracts.StorageContracts;

namespace UniversityAllExpelledExecutorBusinessLogic.BusinessLogic
{
    public class StudentLogic : IStudentLogic
    {
        private readonly IStudentStorage _studentStorage;
        public StudentLogic(IStudentStorage studentStorage)
        {
            _studentStorage = studentStorage;
        }
        public List<StudentViewModel> Read(StudentBindingModel model)
        {
            if (model == null)
            {
                return _studentStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<StudentViewModel> { _studentStorage.GetElement(model) };
            }
            return _studentStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(StudentBindingModel model)
        {
            var element = _studentStorage.GetElement(new StudentBindingModel
            {
                TeacherId = model.TeacherId,
                NumbGB = model.NumbGB
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть студент с таким номером зачётной книжки");
            }
            if (model.Id.HasValue)
            {
                _studentStorage.Update(model);
            }
            else
            {
                _studentStorage.Insert(model);
            }
        }
        public void Delete(StudentBindingModel model)
        {
            var element = _studentStorage.GetElement(new StudentBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Студент не найден");
            }
            _studentStorage.Delete(model);
        }
    }
}
