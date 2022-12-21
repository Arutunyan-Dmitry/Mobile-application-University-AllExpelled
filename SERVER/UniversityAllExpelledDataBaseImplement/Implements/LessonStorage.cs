using System;
using System.Collections.Generic;
using System.Linq;
using UniversityAllExpelledSuretyContracts.BindingModel;
using UniversityAllExpelledSuretyContracts.StorageContracts;
using UniversityAllExpelledSuretyContracts.ViewModels;
using UniversityAllExpelledDataBaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace UniversityAllExpelledDataBaseImplement.Implements
{
    public class LessonStorage : ILessonStorage
    {
        public List<LessonViewModel> GetFullList()
        {
            using var context = new UniversityAllExpelledDatabase();
            return context.Lessons
            .Include(rec => rec.Department)
            .Include(rec => rec.TypeReportingLessons)
            .ThenInclude(rec => rec.TypeReporting)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public List<LessonViewModel> GetFilteredList(LessonBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityAllExpelledDatabase();
            return context.Lessons
                .Include(rec => rec.Department)
            .Include(rec => rec.TypeReportingLessons)
            .ThenInclude(rec => rec.TypeReporting)
            .Where(rec =>
            (!model.DateFrom.HasValue && !model.DateTo.HasValue && rec.LessonDate.Date == model.LessonDate.Date) 
            || (model.DateFrom.HasValue && model.DateTo.HasValue && rec.LessonDate.Date >= model.DateFrom.Value.Date 
            && rec.LessonDate.Date <= model.DateTo.Value.Date) ||
            rec.LessonName.Equals(model.LessonName) || (rec.DepartmentId == model.DepartmentId))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public LessonViewModel GetElement(LessonBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityAllExpelledDatabase();
            var lesson = context.Lessons
                .Include(rec => rec.Department)
            .Include(rec => rec.TypeReportingLessons)
            .ThenInclude(rec => rec.TypeReporting)
            .FirstOrDefault(rec => ((rec.LessonName == model.LessonName && rec.DepartmentId == model.DepartmentId) || rec.Id == model.Id));
            return lesson != null ? CreateModel(lesson) : null;
        }
        public void Insert(LessonBindingModel model)
        {
            using var context = new UniversityAllExpelledDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Lesson lesson = new Lesson()
                {
                    DepartmentId = model.DepartmentId,
                    LessonName = model.LessonName,
                    LessonDate = model.LessonDate
                };
                context.Lessons.Add(lesson);
                context.SaveChanges();
                CreateModel(model, lesson, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(LessonBindingModel model)
        {
            using var context = new UniversityAllExpelledDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Lessons.FirstOrDefault(rec => rec.Id == model.Id);
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
        public void Delete(LessonBindingModel model)
        {
            using var context = new UniversityAllExpelledDatabase();
            Lesson element = context.Lessons.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Lessons.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Lesson CreateModel(LessonBindingModel model, Lesson lesson, UniversityAllExpelledDatabase context)
        {

            lesson.DepartmentId = model.DepartmentId;
            lesson.LessonName = model.LessonName;
            lesson.LessonDate = model.LessonDate;



            if (model.Id.HasValue)
            {
                var lessonTypeReporting = context.TypeReportingLessons.Where(rec => rec.LessonId == model.Id.Value).ToList();
                context.TypeReportingLessons.RemoveRange(lessonTypeReporting.Where(rec => 
                !model.LessonTypeReporting.ContainsKey(rec.TypeReportingId)).ToList());
                context.SaveChanges();
                foreach (var updateReports in lessonTypeReporting)
                {
                    model.LessonTypeReporting.Remove(updateReports.TypeReportingId);
                }
                context.SaveChanges();
            }
            foreach (var ltr in model.LessonTypeReporting)
            {
                context.TypeReportingLessons.Add(new TypeReportingLesson
                {
                    LessonId = lesson.Id,
                    TypeReportingId = ltr.Key
                });
                context.SaveChanges();
            }
            return lesson;
        }
        private static LessonViewModel CreateModel(Lesson lesson)
        {
            return new LessonViewModel
            {
                Id = lesson.Id,
                LessonName = lesson.LessonName,
                LessonDate = lesson.LessonDate.ToShortDateString(),
                LessonTypeReporting = lesson.TypeReportingLessons.ToDictionary(recLT => recLT.TypeReportingId, recLT => (recLT.TypeReporting?.ReportName))
            };
        }
    }
}
