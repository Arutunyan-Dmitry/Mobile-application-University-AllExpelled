using System;
using System.Collections.Generic;
using UniversityAllExpelledExecutorContracts.BindingModels;
using UniversityAllExpelledExecutorContracts.ViewModels;
using UniversityAllExpelledExecutorContracts.BusinessLogicContracts;
using UniversityAllExpelledExecutorContracts.StorageContracts;
using System.Text.RegularExpressions;

namespace UniversityAllExpelledExecutorBusinessLogic.BusinessLogic
{
    public class TeacherLogic : ITeacherLogic
    {
        private readonly ITeacherStorage _teacherStorage;
        private readonly int _passwordMaxLength = 50;
        private readonly int _passwordMinLength = 10;

        public TeacherLogic(ITeacherStorage teacherStorage)
        {
            _teacherStorage = teacherStorage;
        }
        public List<TeacherViewModel> Read(TeacherBindingModel model)
        {
            if (model == null)
            {
                return _teacherStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<TeacherViewModel> { _teacherStorage.GetElement(model) };
            }
            return _teacherStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(TeacherBindingModel model)
        {
            var element = _teacherStorage.GetElement(new TeacherBindingModel
            {
                Email = model.Email
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть такой учитель");
            }
            if (!Regex.IsMatch(model.Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                throw new Exception("В качестве логина должна быть указана почта");
            }
            if (model.Password.Length > _passwordMaxLength || model.Password.Length < _passwordMinLength ||
                !Regex.IsMatch(model.Password, @"^((\w+\d+\W+)|(\w+\W+\d+)|(\d+\w+\W+)|(\d+\W+\w+)|(\W+\w+\d+)|(\W+\d+\w+))[\w\d\W]*$"))
            {
                throw new Exception($"Пароль должен быть длиной от {_passwordMinLength} до " +
                    $"{ _passwordMaxLength } и состоять из цифр, букв и небуквенных символов");
            }
            if (model.Id.HasValue)
            {
                _teacherStorage.Update(model);
            }
            else
            {
                _teacherStorage.Insert(model);
            }
        }
        public void Delete(TeacherBindingModel model)
        {
            var element = _teacherStorage.GetElement(new TeacherBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Учитель не найден");
            }
            _teacherStorage.Delete(model);
        }
    }
}
