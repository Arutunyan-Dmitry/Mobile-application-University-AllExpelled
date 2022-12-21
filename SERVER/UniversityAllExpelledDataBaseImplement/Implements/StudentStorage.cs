using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using UniversityAllExpelledExecutorContracts.BindingModels;
using UniversityAllExpelledExecutorContracts.ViewModels;
using UniversityAllExpelledExecutorContracts.StorageContracts;
using UniversityAllExpelledDataBaseImplement.Models;

namespace UniversityAllExpelledDataBaseImplement.Implements
{
    public class StudentStorage : IStudentStorage
    {
        public List<StudentViewModel> GetFullList()
        {
            using var context = new UniversityAllExpelledDatabase();
            return context.Students
                .Include(rec => rec.Teacher)
                .Select(CreateModel)
                .ToList();
        }
        public List<StudentViewModel> GetFilteredList(StudentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityAllExpelledDatabase();
            return context.Students
            .Include(rec => rec.Teacher)
            .Where(rec => (rec.NumbGB.Equals(model.NumbGB) && model.Students == null) || 
            (model.Students == null && rec.TeacherId == model.TeacherId) || 
            (model.Students != null && rec.TeacherId == model.TeacherId && model.Students.Contains(rec.Id)))
            .Select(CreateModel)
            .ToList();
        }
        public StudentViewModel GetElement(StudentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityAllExpelledDatabase();
            var student = context.Students
            .Include(rec => rec.Teacher)
            .FirstOrDefault(rec => (rec.NumbGB == model.NumbGB && rec.TeacherId == model.TeacherId) || rec.Id == model.Id);
            return student != null ? CreateModel(student) : null;
        }
        public void Insert(StudentBindingModel model)
        {
            using var context = new UniversityAllExpelledDatabase();
            context.Students.Add(CreateModel(model, new Student()));
            context.SaveChanges();
        }
        public void Update(StudentBindingModel model)
        {
            using var context = new UniversityAllExpelledDatabase();
            var element = context.Students.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
        }
        public void Delete(StudentBindingModel model)
        {
            using var context = new UniversityAllExpelledDatabase();
            Student element = context.Students.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Students.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Student CreateModel(StudentBindingModel model, Student student)
        {
            student.TeacherId = model.TeacherId;
            student.NumbGB = model.NumbGB;
            student.Name = model.Name;
            student.Surname = model.Surname;
            student.MiddleName = model.MiddleName;
            return student;
        }
        private static StudentViewModel CreateModel(Student student)
        {
            return new StudentViewModel
            {
                Id = student.Id,
                NumbGB = student.NumbGB,
                Name = student.Name,
                Surname = student.Surname,
                MiddleName = student.MiddleName
            };
        }
    }
}
