using System;
using System.Collections.Generic;
using UniversityAllExpelledSuretyContracts.BindingModel;
using UniversityAllExpelledSuretyContracts.BusinessLogicContracts;
using UniversityAllExpelledSuretyContracts.StorageContracts;
using UniversityAllExpelledSuretyContracts.ViewModels;
using System.Text.RegularExpressions;

namespace UniversityAllExpelledSuretyBusinessLogic.BusinessLogic
{
    public class DepartmentLogic : IDepartmentLogic
    {
        private readonly IDepartmentStorage _departmentStorage;
        private readonly int _passwordMinLength = 8;
        private readonly int _passwordMaxLength = 50;

        public DepartmentLogic(IDepartmentStorage departmentStorage)
        {
            _departmentStorage = departmentStorage;
        }
        public List<DepartmentViewModel> Read(DepartmentBindingModel model)
        {
            if (model == null)
            {
                return _departmentStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<DepartmentViewModel> { _departmentStorage.GetElement(model) };
            }
            return _departmentStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(DepartmentBindingModel model)
        {
            var element = _departmentStorage.GetElement(new DepartmentBindingModel
            {
                DepartmentName = model.DepartmentName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть кафедра с таким названием");
            }
            if (model.Id.HasValue)
            {
                _departmentStorage.Update(model);
            }
            if (!Regex.IsMatch(model.Login, @"^([\w.-]+)@([\w-]+)((.(\w){2,3})+)$"))
            {
                throw new Exception("В качестве логина должна быть указана почта");
            }
            if (model.Password.Length > _passwordMaxLength || model.Password.Length < _passwordMinLength ||
                !Regex.IsMatch(model.Password, @"^((\w+\d+\W+)|(\w+\W+\d+)|(\d+\w+\W+)|(\d+\W+\w+)|(\W+\w+\d+)|(\W+\d+\w+))[\w\d\W]*$"))
            {
                throw new Exception($"Пароль должен быть длиной от {_passwordMinLength} до " +
                    $"{ _passwordMaxLength } и состоять из цифр, букв и небуквенных символов");
            }
            else
            {
                _departmentStorage.Insert(model);
            }
        }
        public void Delete(DepartmentBindingModel model)
        {
            var element = _departmentStorage.GetElement(new DepartmentBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Кафедра не найдена");
            }
            _departmentStorage.Delete(model);
        }

        public int CheckPassword(string login, string password)
        {
            var department = _departmentStorage.GetElement(new DepartmentBindingModel
            {
                Login = login
            });
            if (department == null)
            {
                throw new Exception("Кафедра не найдена");
            }
            if (!department.Password.Equals(password))
            {
                throw new Exception("Неверный пароль");
            }
            return department.Id;
        }
    }
}
