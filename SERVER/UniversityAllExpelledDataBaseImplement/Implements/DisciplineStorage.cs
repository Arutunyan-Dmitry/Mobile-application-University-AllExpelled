using System;
using System.Collections.Generic;
using System.Linq;
using UniversityAllExpelledSuretyContracts.BindingModel;
using UniversityAllExpelledSuretyContracts.StorageContracts;
using UniversityAllExpelledSuretyContracts.ViewModels;
using UniversityAllExpelledDataBaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using UniversityAllExpelledExecutorContracts.StorageContracts;

namespace UniversityAllExpelledDataBaseImplement.Implements
{
    public class DisciplineStorage : IDisciplineStorage
    {
        public List<DisciplineViewModel> GetFullList()
        {
            using var context = new UniversityAllExpelledDatabase();
            return context.Disciplines
            .Include(rec => rec.Department)
            .Include(rec => rec.Statement)
            .Include(rec => rec.TypeReportingDisciplines)
            .ThenInclude(rec => rec.TypeReporting)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public List<DisciplineViewModel> GetFilteredList(DisciplineBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityAllExpelledDatabase();
            return context.Disciplines
            .Include(rec => rec.Department)
            .Include(rec => rec.Statement)
            .Include(rec => rec.TypeReportingDisciplines)
            .ThenInclude(rec => rec.TypeReporting)
            .Where(rec => (rec.DisciplineName == model.DisciplineName) || (rec.DepartmentId == model.DepartmentId))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public DisciplineViewModel GetElement(DisciplineBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityAllExpelledDatabase();
            var discipline = context.Disciplines
            .Include(rec => rec.Department)
            .Include(rec => rec.Statement)
            .Include(rec => rec.TypeReportingDisciplines)
            .ThenInclude(rec => rec.TypeReporting)
            .FirstOrDefault(rec => (rec.DisciplineName == model.DisciplineName && rec.DepartmentId == model.DepartmentId) || rec.Id == model.Id);
            return discipline != null ? CreateModel(discipline) : null;
        }
        public void Insert(DisciplineBindingModel model)
        {
            using var context = new UniversityAllExpelledDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Discipline discipline = new Discipline()
                {
                    DepartmentId = model.DepartmentId,
                    DisciplineName = model.DisciplineName,
                    StatementId = model.StatementId
                };
                context.Disciplines.Add(discipline);
                context.SaveChanges();
                CreateModel(model, discipline, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(DisciplineBindingModel model)
        {
            using var context = new UniversityAllExpelledDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Disciplines.FirstOrDefault(rec => rec.Id == model.Id);
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
        public void Delete(DisciplineBindingModel model)
        {
            using var context = new UniversityAllExpelledDatabase();
            Discipline element = context.Disciplines.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Disciplines.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }  
        private static Discipline CreateModel(DisciplineBindingModel model, Discipline discipline, UniversityAllExpelledDatabase context)
        {
            discipline.StatementId = model.StatementId;
            discipline.DisciplineName = model.DisciplineName;
            if (model.Id.HasValue)
            {
                var disciplineTypeReporting = context.TypeReportingDisciplines.Where(rec => rec.DisciplineId == model.Id.Value).ToList();
                context.TypeReportingDisciplines.RemoveRange(disciplineTypeReporting.Where(rec => !model.DisciplineTypeReporting.ContainsKey(rec.TypeReportingId)).ToList());
                context.SaveChanges();
                foreach (var updateReports in disciplineTypeReporting)
                {
                    model.DisciplineTypeReporting.Remove(updateReports.TypeReportingId);
                }
                context.SaveChanges();
            }
            foreach (var ltr in model.DisciplineTypeReporting)
            {
                context.TypeReportingDisciplines.Add(new TypeReportingDiscipline
                {
                    DisciplineId = discipline.Id,
                    TypeReportingId = ltr.Key,
                });
                context.SaveChanges();
            }

            return discipline;
        }
        private static DisciplineViewModel CreateModel(Discipline discipline)
        {
            return new DisciplineViewModel
            {
               Id = discipline.Id,
               DisciplineName = discipline.DisciplineName,
               StatementName = discipline.Statement.Naming,
               DisciplineTypeReporting = discipline.TypeReportingDisciplines.ToDictionary(recTD => recTD.TypeReportingId,
               recTD => recTD.TypeReporting?.ReportName)
            };
        }
    }
}
