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
    public class TypeReportingStorage : ITypeReportingStorage
    {
        public List<TypeReportingViewModel> GetFullList()
        {
            using var context = new UniversityAllExpelledDatabase();
            return context.TypeReportings
            .Include(rec => rec.Department)
            .Select(CreateModel)
            .ToList();
        }

        public List<TypeReportingViewModel> GetFilteredList(TypeReportingBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityAllExpelledDatabase();
            return context.TypeReportings
                .Include(rec => rec.Department)
            .Where(rec => 
            (model.ReportsId==null && rec.Id == model.Id && rec.DepartmentId == model.DepartmentId) || 
          (model.ReportsId == null && rec.ReportName.Contains(model.ReportName) ) 
            || (model.ReportsId == null && rec.DepartmentId == model.DepartmentId) || (model.ReportsId != null && rec.DepartmentId == model.DepartmentId &&
            model.ReportsId.Contains(rec.Id)))
            .Select(CreateModel)
            .ToList();
        }

        public TypeReportingViewModel GetElement(TypeReportingBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityAllExpelledDatabase();
            var typeReporting = context.TypeReportings
                .Include(rec => rec.Department)
            .FirstOrDefault(rec => rec.ReportName == model.ReportName || rec.Id == model.Id);
            return typeReporting != null ? CreateModel(typeReporting) : null;
        }

        public void Insert(TypeReportingBindingModel model)
        {
            using var context = new UniversityAllExpelledDatabase();
            context.TypeReportings.Add(CreateModel(model, new TypeReporting()));
            context.SaveChanges();
        }

        public void Update(TypeReportingBindingModel model)
        {
            using var context = new UniversityAllExpelledDatabase();
            var element = context.TypeReportings.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
        }

        public void Delete(TypeReportingBindingModel model)
        {
            using var context = new UniversityAllExpelledDatabase();
            TypeReporting element = context.TypeReportings.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.TypeReportings.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        private static TypeReporting CreateModel(TypeReportingBindingModel model, TypeReporting typeReporting)
        {
            typeReporting.ReportNumber = model.ReportNumber;
            typeReporting.ReportName = model.ReportName;
            typeReporting.DepartmentId = model.DepartmentId;
            return typeReporting;
        }
        private static TypeReportingViewModel CreateModel(TypeReporting typeReporting)
        {
            return new TypeReportingViewModel
            {
                Id = typeReporting.Id,
                ReportNumber = typeReporting.ReportNumber,
                ReportName = typeReporting.ReportName
            };
        }
    }
}
