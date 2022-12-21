using UniversityAllExpelledExecutorBusinessLogic.OfficePackage;
using UniversityAllExpelledExecutorBusinessLogic.OfficePackage.HelperModels;
using UniversityAllExpelledExecutorContracts.BindingModels;
using UniversityAllExpelledExecutorContracts.BusinessLogicContracts;
using UniversityAllExpelledExecutorContracts.StorageContracts;
using UniversityAllExpelledExecutorContracts.ViewModels;
using UniversityAllExpelledExecutorContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using UniversityAllExpelledSuretyContracts.StorageContracts;

namespace UniversityAllExpelledExecutorBusinessLogic.BusinessLogic
{
    public class ReportLogic : IReportLogic
    {
        private readonly IStudentStorage _studentStorage;
        private readonly ITestingStorage _testingStorage;
        private readonly IStatementStorage _statementStorage;
        private readonly IDisciplineStorage _disciplineStorage;

        private readonly AbstractSaveToExcel _saveToExcel;
        private readonly AbstractSaveToWord _saveToWord;
        private readonly AbstractSaveToPdf _saveToPdf;
        public ReportLogic(ITestingStorage testingStorage, IStudentStorage studenttStorage,
         IStatementStorage statementStorage, IDisciplineStorage disciplineStorage, AbstractSaveToExcel saveToExcel, AbstractSaveToWord saveToWord,
         AbstractSaveToPdf saveToPdf)
        {
            _testingStorage = testingStorage;
            _studentStorage = studenttStorage;
            _statementStorage = statementStorage;
            _disciplineStorage = disciplineStorage;

            _saveToExcel = saveToExcel;
            _saveToWord = saveToWord;
            _saveToPdf = saveToPdf;
        }

        // [studentsId, teacherId]
        public List<ReportStudentDisciplineViewModel> GetStudentDisciplines(ReportBindingModel model)
        {

            int[] tmpArr = model.Parametrs.Split(' ').Select(x => int.Parse(x)).ToArray();
            var students = _studentStorage.GetFilteredList(new StudentBindingModel
            {
                TeacherId = tmpArr[tmpArr.Length - 1],
                Students = tmpArr.Take(tmpArr.Length - 1).ToArray()
            });
            var disciplines = _disciplineStorage.GetFullList();
            var list = new List<ReportStudentDisciplineViewModel>();

            foreach (var student in students)
            {
                var StDic = new ReportStudentDisciplineViewModel
                {
                    NumbGB = student.NumbGB,
                    FLM = student.Surname + " " + student.Name[0] + ". " + student.MiddleName[0] + ".",
                    DisciplineNames = new List<string>()
                };
                foreach (var discipline in disciplines)
                {
                    var statement = _statementStorage.GetElement(new StatementBindingModel
                    {
                        TeacherId = tmpArr[tmpArr.Length - 1],
                        Naming = discipline.StatementName
                    });
                    var tmp_students = statement.StudentStatements.Where(rec => rec.Key == student.Id);
                    if (tmp_students.Any())
                    {
                        StDic.DisciplineNames.Add(discipline.DisciplineName);
                    }
                }
                if (StDic.DisciplineNames.Count != 0)
                {
                    list.Add(StDic);
                }
            }
            return list;
        }
        /// <summary>
        /// Получение списка заказов за определенный период
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ReportStudentStatementTestingViewModel> GetStudentStatementTestings(ReportBindingModel model)
        {
            string[] tmpArr = model.Parametrs.Split(' ');
            var statements = _statementStorage.GetFilteredList(new StatementBindingModel
            {
                DateFrom = tmpArr[0],
                DateTo = tmpArr[1],
                TeacherId = Convert.ToInt32(tmpArr[2])
            });
            var students = _studentStorage.GetFilteredList(new StudentBindingModel
            {
                TeacherId = Convert.ToInt32(tmpArr[2])
            });
            var testings = _testingStorage.GetFilteredList(new TestingBindingModel
            {
                TeacherId = Convert.ToInt32(tmpArr[2])
            });
            var list = new List<ReportStudentStatementTestingViewModel>();

            if (statements == null)
            {
                return list;
            }

            foreach (var student in students)
            {
                var StStTe = new ReportStudentStatementTestingViewModel
                {
                    NumbGB = student.NumbGB,
                    FLM = student.Surname + " " + student.Name[0] + ". " + student.MiddleName[0] + ".",
                    StudStatements = new List<Tuple<DateTime, string, Achievements>>(),
                    StudTestings = new List<Tuple<string, Marks>>()
                };
                foreach (var statement in statements)
                {
                    if (statement.StudentStatements.ContainsKey(student.Id))
                    {
                        StStTe.StudStatements.Add(new Tuple<DateTime, string, Achievements>(
                                DateTime.Parse(statement.DateCreate),
                                statement.Naming,
                                statement.StudentStatements[student.Id].Achievement
                            ));
                    }
                }
                foreach (var testing in testings)
                {
                    if (testing.StudentTestings.ContainsKey(student.Id))
                    {
                        StStTe.StudTestings.Add(new Tuple<string, Marks>(
                                testing.Naming,
                                testing.StudentTestings[student.Id].Mark
                            ));
                    }
                }
                if (StStTe.StudStatements.Count != 0 && StStTe.StudTestings.Count != 0)
                {
                    list.Add(StStTe);
                }
            }
            return list;
        }
        public void SaveStudentDisciplinesToWordFile(ReportBindingModel model)
        {
             _saveToWord.CreateDoc(new WordInfo
             {
                 FileName = model.FileName,
                 Title = "Список студентов и дисциплин",
                 StudDisc = GetStudentDisciplines(model)
             });
        }
        public void SaveStudentDisciplinesToExcelFile(ReportBindingModel model)
        {
             _saveToExcel.CreateReport(new ExcelInfo
             {
                 FileName = model.FileName,
                 Title = "Список студентов и дисциплин",
                 StudDisc = GetStudentDisciplines(model)
             });
        }
        public void SaveStudentStatementTestingsToPdfFile(ReportBindingModel model)
        {
            string[] tmpArr = model.Parametrs.Split(' ');

            _saveToPdf.CreateDoc(new PdfInfo
             {
                 FileName = model.FileName,
                 Title = "Список студентов, ведомостей и испытаний",
                 DateFrom = DateTime.Parse(tmpArr[0]),
                 DateTo = DateTime.Parse(tmpArr[1]),
                 StudStatTest = GetStudentStatementTestings(model)
             });
        }
    }
}
