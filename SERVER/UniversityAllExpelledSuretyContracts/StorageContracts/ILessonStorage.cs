using System.Collections.Generic;
using UniversityAllExpelledSuretyContracts.BindingModel;
using UniversityAllExpelledSuretyContracts.ViewModels;

namespace UniversityAllExpelledSuretyContracts.StorageContracts
{
    public interface ILessonStorage
    {
        List<LessonViewModel> GetFullList();

        List<LessonViewModel> GetFilteredList(LessonBindingModel model);

        LessonViewModel GetElement(LessonBindingModel model);

        void Insert(LessonBindingModel model);

        void Update(LessonBindingModel model);

        void Delete(LessonBindingModel model);
    }
}
