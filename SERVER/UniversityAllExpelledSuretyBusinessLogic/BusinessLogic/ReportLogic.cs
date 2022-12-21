using System;
using System.Collections.Generic;
using UniversityAllExpelledSuretyBusinessLogic.OfficePackage;
using UniversityAllExpelledSuretyBusinessLogic.OfficePackage.HelperModels;
using UniversityAllExpelledSuretyContracts.BindingModel;
using UniversityAllExpelledSuretyContracts.BusinessLogicContracts;
using UniversityAllExpelledSuretyContracts.StorageContracts;
using UniversityAllExpelledSuretyContracts.ViewModels;
using UniversityAllExpelledExecutorContracts.StorageContracts;
using UniversityAllExpelledExecutorContracts.Enums;
using System.Linq;

namespace UniversityAllExpelledSuretyBusinessLogic.BusinessLogic
{
    public class ReportLogic : IReportLogic
    {
        private readonly AbstractSaveToExcel _saveToExcel;
        private readonly AbstractSaveToWord _saveToWord;
        private readonly AbstractSaveToPdf _saveToPdf;
        private readonly ITypeReportingStorage _typeReportingStorage;
        private readonly ITestingStorage _testingStorage;
        private readonly ILessonStorage _lessonStorage;
        private readonly IDisciplineStorage _disciplineStorage;
        public ReportLogic(ITypeReportingStorage typeReportingStorage, ITestingStorage testingStorage, ILessonStorage lessonStorage, 
            IDisciplineStorage disciplineStorage, AbstractSaveToPdf saveToPdf,
       AbstractSaveToExcel saveToExcel, AbstractSaveToWord saveToWord)
        {
            _typeReportingStorage = typeReportingStorage;
            _testingStorage = testingStorage;
            _lessonStorage = lessonStorage;
            _disciplineStorage = disciplineStorage;
            _saveToExcel = saveToExcel;
            _saveToWord = saveToWord;
            _saveToPdf = saveToPdf;
        }
        public List<ReportDisciplineLessonViewModel> GetDisciplineLesson(ReportBindingModel model)
        {
            
            var lessons = _lessonStorage.GetFilteredList(new LessonBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo,
                DepartmentId = model.DepartmentId
            });
            var typeReportings = _typeReportingStorage.GetFilteredList(new TypeReportingBindingModel
            {
                DepartmentId = model.DepartmentId
            });
            var disciplines = _disciplineStorage.GetFilteredList(new DisciplineBindingModel
            {
                DepartmentId = model.DepartmentId
            });
            var list = new List<ReportDisciplineLessonViewModel>();

            if (disciplines == null)
            {
                return list;
            }

            foreach (var typeReporting in typeReportings)
            {
                var reportDL = new ReportDisciplineLessonViewModel();
                foreach (var lesson in lessons)
                {
                    if (lesson.LessonTypeReporting.ContainsKey(typeReporting.Id))
                    {
                        reportDL.LessonName = lesson.LessonName;
                        reportDL.DateLesson = DateTime.Parse(lesson.LessonDate);
                    }
                }
                foreach (var discipline in disciplines)
                {
                    if (discipline.DisciplineTypeReporting.ContainsKey(typeReporting.Id))
                    {
                       reportDL.DisciplineName = discipline.DisciplineName;
                    }
                }
                if (reportDL.LessonName != null)
                {
                    list.Add(reportDL);
                }
            }
            return list;
        }
        public List<ReportTestingTypeReportingViewModel> GetTestingTypeReporting(ReportTestingTypeReportingBindingModel model)
        {
            var testings = _testingStorage.GetFullList();
            var list = new List<ReportTestingTypeReportingViewModel>();

            var typeReportings = _typeReportingStorage.GetFilteredList(new TypeReportingBindingModel
            {
                DepartmentId = model.DepartmentId,
                ReportsId = model.TypeReportingId
            });
            foreach (var typeReporting in typeReportings)
            {
                var record = new ReportTestingTypeReportingViewModel
                {
                    ReportNumber = typeReporting.ReportNumber,
                    ReportName = typeReporting.ReportName,
                    TestingNameType = new Dictionary<string, string>()
                };
                foreach(var testing in testings)
                {

                    var lesson = _lessonStorage.GetElement(new LessonBindingModel
                    {
                        DepartmentId = model.DepartmentId,
                        LessonName = testing.LessonName
                    });
                    var tmp_reports = lesson.LessonTypeReporting.Where(rec => rec.Key == typeReporting.Id);
                    if (tmp_reports.Any())
                    {
                        record.TestingNameType.Add(testing.Naming, testing.Type.ToString());
                    }
                }
                if (record.TestingNameType != null)
                {
                    list.Add(record);
                }
            }
            return list;
        }
        /// <summary>
        /// Сохранение в файл-Word
        /// </summary>
        /// <param name="model"></param>
       public void SaveTestingTypeReportingToWordFile(ReportTestingTypeReportingBindingModel model)
        {
            _saveToWord.CreateTableDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список результатов испытаний",
                TestingTypeReporting = GetTestingTypeReporting(model)
            });
        }
        /// <summary>
        /// Сохранение в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SaveTestingTypeReportingExcelFile(ReportTestingTypeReportingBindingModel model)
        {
            _saveToExcel.CreateReport(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список результатов испытаний",
                TestingTypeReporting = GetTestingTypeReporting(model)
            });
        }
        /// <summary>
        /// Сохранение в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        public void SaveDisciplineLessonToPdfFile(ReportBindingModel model)
        {
            _saveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список дисциплин и занятий",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                DisciplineLesson = GetDisciplineLesson(model)
            });
        }
    }
}
