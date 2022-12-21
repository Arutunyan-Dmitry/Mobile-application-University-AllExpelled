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
    public class TestingStorage : ITestingStorage
    {
        public List<TestingViewModel> GetFullList()
        {
            using var context = new UniversityAllExpelledDatabase();
            return context.Testings
            .Include(rec => rec.Teacher)
            .Include(rec => rec.Lesson)
            .Include(rec => rec.StudentTestings)
            .ThenInclude(rec => rec.Student)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public List<TestingViewModel> GetFilteredList(TestingBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityAllExpelledDatabase();
            return context.Testings
            .Include(rec => rec.Teacher)
            .Include(rec => rec.Lesson)
            .Include(rec => rec.StudentTestings)
            .ThenInclude(rec => rec.Student)
            .Where(rec => rec.Naming.Equals(model.Naming) || rec.TeacherId == model.TeacherId)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public TestingViewModel GetElement(TestingBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityAllExpelledDatabase();
            var testing = context.Testings
            .Include(rec => rec.Teacher)
            .Include(rec => rec.Lesson)
            .Include(rec => rec.StudentTestings)
            .ThenInclude(rec => rec.Student)
            .FirstOrDefault(rec => (rec.Naming == model.Naming && rec.TeacherId == model.TeacherId) || rec.Id == model.Id);
            return testing != null ? CreateModel(testing) : null;
        }
        public void Insert(TestingBindingModel model)
        {
            using var context = new UniversityAllExpelledDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Testing testing = new Testing()
                {
                    Naming = model.Naming,
                    Type = model.Type,
                    LessonId = model.LessonId,
                    TeacherId = model.TeacherId,
                };
                context.Testings.Add(testing);
                context.SaveChanges();
                CreateModel(model, testing, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(TestingBindingModel model)
        {
            using var context = new UniversityAllExpelledDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Testings.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Delete(TestingBindingModel model)
        {
            using var context = new UniversityAllExpelledDatabase();
            Testing element = context.Testings.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Testings.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Testing CreateModel(TestingBindingModel model, Testing testing, UniversityAllExpelledDatabase context)
        {
            testing.Naming = model.Naming;
            testing.Type = model.Type;
            testing.LessonId = model.LessonId;
            if (model.StudentTestings == null)
            {
                return testing;
            }
            if (model.Id.HasValue)
            {
                var studentTestings = context.StudentTestings.Where(rec => rec.TestingId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.StudentTestings.RemoveRange(studentTestings.Where(rec =>
               !model.StudentTestings.ContainsKey(rec.StudentId)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateStudent in studentTestings)
                {
                    try
                    {
                        updateStudent.Mark = model.StudentTestings[updateStudent.StudentId].Item3;
                    } catch { }
                    model.StudentTestings.Remove(updateStudent.StudentId);
                }
                context.SaveChanges();
            }
            // добавили новые
            foreach (var st in model.StudentTestings)
            {
                context.StudentTestings.Add(new StudentTesting
                {
                    TestingId = testing.Id,
                    StudentId = st.Key,
                    Mark = st.Value.Item3
                });
                context.SaveChanges();
            }
            return testing;
        }
        private static TestingViewModel CreateModel(Testing testing)
        {
            return new TestingViewModel
            {
                Id = testing.Id,
                LessonId = testing.LessonId,
                Naming = testing.Naming,
                Type = testing.Type,
                LessonName = testing.Lesson.LessonName,
                StudentTestings = testing.StudentTestings
            .ToDictionary(recST => recST.StudentId, recSS => new StudentTestingViewModel
            {
                NumbGb = recSS.Student?.NumbGB,
                Flm = recSS.Student?.Surname + " " + recSS.Student?.Name + " " + recSS.Student?.MiddleName,
                Mark = recSS.Mark
            })
            };
        }
    }
}
