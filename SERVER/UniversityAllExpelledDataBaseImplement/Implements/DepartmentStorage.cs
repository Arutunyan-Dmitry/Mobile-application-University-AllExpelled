using System;
using System.Collections.Generic;
using System.Linq;
using UniversityAllExpelledSuretyContracts.BindingModel;
using UniversityAllExpelledSuretyContracts.StorageContracts;
using UniversityAllExpelledSuretyContracts.ViewModels;
using UniversityAllExpelledDataBaseImplement.Models;

namespace UniversityAllExpelledDataBaseImplement.Implements
{
    public class DepartmentStorage : IDepartmentStorage
    {
        public List<DepartmentViewModel> GetFullList()
        {
            using var context = new UniversityAllExpelledDatabase();
            return context.Departments
            .Select(CreateModel)
            .ToList();
        }
        public List<DepartmentViewModel> GetFilteredList(DepartmentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityAllExpelledDatabase();
            return context.Departments
            .Where(rec => rec.Login.Equals(model.Login) && rec.Password == model.Password)
            .Select(CreateModel)
            .ToList();
        }
        public DepartmentViewModel GetElement(DepartmentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityAllExpelledDatabase();
            var department = context.Departments
            .FirstOrDefault(rec => rec.Login == model.Login || rec.Id == model.Id);
            return department != null ? CreateModel(department) : null;
        }
        public void Insert(DepartmentBindingModel model)
        {
            using var context = new UniversityAllExpelledDatabase();
            context.Departments.Add(CreateModel(model, new Department()));
            context.SaveChanges();
        }
        public void Update(DepartmentBindingModel model)
        {
            using var context = new UniversityAllExpelledDatabase();
            var element = context.Departments.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
        }
        public void Delete(DepartmentBindingModel model)
        {
            using var context = new UniversityAllExpelledDatabase();
            Department element = context.Departments.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Departments.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Department CreateModel(DepartmentBindingModel model, Department department)
        {
            department.DepartmentName = model.DepartmentName;
            department.Login = model.Login;
            department.Password = model.Password;
            return department;
        }
        private static DepartmentViewModel CreateModel(Department department)
        {
            return new DepartmentViewModel
            {
                Id = department.Id,
                DepartmentName = department.DepartmentName,
                Login = department.Login,
                Password = department.Password
            };
        }
    }
}

