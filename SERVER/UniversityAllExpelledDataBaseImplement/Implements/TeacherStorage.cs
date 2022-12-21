using System;
using System.Collections.Generic;
using System.Linq;
using UniversityAllExpelledExecutorContracts.BindingModels;
using UniversityAllExpelledExecutorContracts.ViewModels;
using UniversityAllExpelledExecutorContracts.StorageContracts;
using UniversityAllExpelledDataBaseImplement.Models;

namespace UniversityAllExpelledDataBaseImplement.Implements
{
    public class TeacherStorage : ITeacherStorage
    {
        public List<TeacherViewModel> GetFullList()
        {
            using var context = new UniversityAllExpelledDatabase();
            return context.Teachers
            .Select(CreateModel)
            .ToList();
        }
        public List<TeacherViewModel> GetFilteredList(TeacherBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityAllExpelledDatabase();
            return context.Teachers
            .Where(rec => rec.Email.Equals(model.Email) && rec.Password == model.Password)
            .Select(CreateModel)
            .ToList();
        }
        public TeacherViewModel GetElement(TeacherBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityAllExpelledDatabase();
            var teacher = context.Teachers
            .FirstOrDefault(rec => rec.Email == model.Email || rec.Id == model.Id);
            return teacher != null ? CreateModel(teacher) : null;
        }
        public void Insert(TeacherBindingModel model)
        {
            using var context = new UniversityAllExpelledDatabase();
            context.Teachers.Add(CreateModel(model, new Teacher()));
            context.SaveChanges();
        }
        public void Update(TeacherBindingModel model)
        {
            using var context = new UniversityAllExpelledDatabase();
            var element = context.Teachers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
        }
        public void Delete(TeacherBindingModel model)
        {
            using var context = new UniversityAllExpelledDatabase();
            Teacher element = context.Teachers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Teachers.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Teacher CreateModel(TeacherBindingModel model, Teacher teacher)
        {
            teacher.Name = model.Name;
            teacher.Surname = model.Surname;
            teacher.MiddleName = model.MiddleName;
            teacher.Email = model.Email;
            teacher.Password = model.Password;
            return teacher;
        }
        private static TeacherViewModel CreateModel(Teacher teacher)
        {
            return new TeacherViewModel
            {
                Id = teacher.Id,
                Name = teacher.Name,
                Surname = teacher.Surname,
                MiddleName = teacher.MiddleName,
                Email = teacher.Email,
                Password = teacher.Password
            };
        }
    }
}
