using System;
using System.Collections.Generic;
using UniversityAllExpelledSuretyContracts.BindingModel;
using UniversityAllExpelledSuretyContracts.BusinessLogicContracts;
using UniversityAllExpelledSuretyContracts.StorageContracts;
using UniversityAllExpelledSuretyContracts.ViewModels;

namespace UniversityAllExpelledSuretyBusinessLogic.BusinessLogic
{
    public class TypeReportingLogic : ITypeReportingLogic
    {
        private readonly ITypeReportingStorage _typeReportingStorage;

        public TypeReportingLogic(ITypeReportingStorage typeReportingStorage)
        {
            _typeReportingStorage = typeReportingStorage;
        }
        public List<TypeReportingViewModel> Read(TypeReportingBindingModel model)
        {
            if (model == null)
            {
                return _typeReportingStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<TypeReportingViewModel> { _typeReportingStorage.GetElement(model) };
            }
            return _typeReportingStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(TypeReportingBindingModel model)
        {
            var element = _typeReportingStorage.GetElement(new TypeReportingBindingModel
            {
                ReportName = model.ReportName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть отчет с таким названием");
            }
            if (model.Id.HasValue)
            {
                _typeReportingStorage.Update(model);
            }
            else
            {
                _typeReportingStorage.Insert(model);
            }
        }
        public void Delete(TypeReportingBindingModel model)
        {
            var element = _typeReportingStorage.GetElement(new TypeReportingBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Отчет не найден");
            }
            _typeReportingStorage.Delete(model);
        }
    }
}
