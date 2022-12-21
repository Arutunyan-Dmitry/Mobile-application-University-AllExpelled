using System;
using System.Collections.Generic;
using UniversityAllExpelledSuretyContracts.BindingModel;
using UniversityAllExpelledSuretyContracts.BusinessLogicContracts;
using UniversityAllExpelledSuretyContracts.StorageContracts;
using UniversityAllExpelledSuretyContracts.ViewModels;

namespace UniversityAllExpelledSuretyBusinessLogic.BusinessLogic
{
    public class LessonLogic : ILessonLogic
    {
        private readonly ILessonStorage _lessonStorage;

        public LessonLogic(ILessonStorage lessonStorage)
        {
            _lessonStorage = lessonStorage;
        }
        public List<LessonViewModel> Read(LessonBindingModel model)
        {
            if (model == null)
            {
                return _lessonStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<LessonViewModel> { _lessonStorage.GetElement(model) };
            }
            return _lessonStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(LessonBindingModel model)
        {
            var element = _lessonStorage.GetElement(new LessonBindingModel
            {
                LessonName = model.LessonName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть занятие с таким названием");
            }
            if (model.Id.HasValue)
            {
                _lessonStorage.Update(model);
            }
            else
            {
                _lessonStorage.Insert(model);
            }
        }
        public void Delete(LessonBindingModel model)
        {
            var element = _lessonStorage.GetElement(new LessonBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Занятие не найдено");
            }
            _lessonStorage.Delete(model);
        }
    }
}
