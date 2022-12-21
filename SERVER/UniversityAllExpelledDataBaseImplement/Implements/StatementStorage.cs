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
    public class StatementStorage : IStatementStorage
    {
        public List<StatementViewModel> GetFullList()
        {
            using var context = new UniversityAllExpelledDatabase();
            return context.Statements
            .Include(rec => rec.Teacher)
            .Include(rec => rec.StudentStatements)
            .ThenInclude(rec => rec.Student)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public List<StatementViewModel> GetFilteredList(StatementBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityAllExpelledDatabase();
            return context.Statements
            .Include(rec => rec.Teacher)
            .Include(rec => rec.StudentStatements)
            .ThenInclude(rec => rec.Student)
            .Where(rec => rec.Naming.Equals(model.Naming) || (model.DateFrom == null && model.DateTo == null && rec.TeacherId == model.TeacherId) ||
            (model.DateFrom != null && model.DateTo != null && rec.DateCreate.Date >= DateTime.Parse(model.DateFrom) && rec.DateCreate.Date <= DateTime.Parse(model.DateTo) && rec.TeacherId == model.TeacherId))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public StatementViewModel GetElement(StatementBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityAllExpelledDatabase();
            var statement = context.Statements
            .Include(rec => rec.Teacher)
            .Include(rec => rec.StudentStatements)
            .ThenInclude(rec => rec.Student)
            .FirstOrDefault(rec => (rec.Naming == model.Naming && rec.TeacherId == model.TeacherId) || rec.Id == model.Id);
            return statement != null ? CreateModel(statement) : null;
        }
        public void Insert(StatementBindingModel model)
        {
            using var context = new UniversityAllExpelledDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Statement statement = new Statement()
                {
                    TeacherId = model.TeacherId,
                    Naming = model.Naming,
                    DateCreate = DateTime.Parse(model.DateCreate),
                };
                context.Statements.Add(statement);
                context.SaveChanges();
                CreateModel(model, statement, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(StatementBindingModel model)
        {
            using var context = new UniversityAllExpelledDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Statements.FirstOrDefault(rec => rec.Id == model.Id);
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
        public void Delete(StatementBindingModel model)
        {
            using var context = new UniversityAllExpelledDatabase();
            Statement element = context.Statements.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Statements.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Statement CreateModel(StatementBindingModel model, Statement statement, UniversityAllExpelledDatabase context)
        {
            statement.Naming = model.Naming;
            statement.DateCreate = DateTime.Parse(model.DateCreate);
            if (model.StudentStatements == null)
            {
                return statement;
            }
            if (model.Id.HasValue)
            {
                var studentStatements = context.StudentStatements.Where(rec => rec.StatementId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.StudentStatements.RemoveRange(studentStatements.Where(rec =>
               !model.StudentStatements.ContainsKey(rec.StudentId)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateStudent in studentStatements)
                {
                    try
                    {
                        updateStudent.Achievement = model.StudentStatements[updateStudent.StudentId].Item3;
                    } catch { }
                    model.StudentStatements.Remove(updateStudent.StudentId);
                }
                context.SaveChanges();
            }
            // добавили новые
            foreach (var ss in model.StudentStatements)
            {
                context.StudentStatements.Add(new StudentStatement
                {
                    StatementId = statement.Id,
                    StudentId = ss.Key,
                    Achievement = ss.Value.Item3
                });
                context.SaveChanges();
            }
            return statement;
        }
        private static StatementViewModel CreateModel(Statement statement)
        {
            return new StatementViewModel
            {
                Id = statement.Id,
                Naming = statement.Naming,
                DateCreate = statement.DateCreate.ToShortDateString(),
                StudentStatements = statement.StudentStatements
            .ToDictionary(recSS => recSS.StudentId, recSS => new StudentStatementsViewModel
            {
                NumbGb = recSS.Student?.NumbGB,
                Flm = recSS.Student?.Surname + " " + recSS.Student?.Name + " " + recSS.Student?.MiddleName,
                Achievement = recSS.Achievement
            })};
        }
    }
}
