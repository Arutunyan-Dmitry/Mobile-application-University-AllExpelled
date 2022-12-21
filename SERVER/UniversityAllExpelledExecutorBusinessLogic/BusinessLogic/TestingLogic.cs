using System;
using System.Collections.Generic;
using UniversityAllExpelledExecutorContracts.BindingModels;
using UniversityAllExpelledExecutorContracts.ViewModels;
using UniversityAllExpelledExecutorContracts.BusinessLogicContracts;
using UniversityAllExpelledExecutorContracts.StorageContracts;
using UniversityAllExpelledExecutorContracts.Enums;

namespace UniversityAllExpelledExecutorBusinessLogic.BusinessLogic
{
    public class TestingLogic : ITestingLogic
    {
        private readonly ITestingStorage _testingtStorage;
        private readonly IStudentStorage _studentStorage;
        public TestingLogic(ITestingStorage testingStorage, IStudentStorage studentStorage)
        {
            _testingtStorage = testingStorage;
            _studentStorage = studentStorage;
        }
        public List<TestingViewModel> Read(TestingBindingModel model)
        {
            if (model == null)
            {
                return _testingtStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<TestingViewModel> { _testingtStorage.GetElement(model) };
            }
            return _testingtStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(TestingBindingModel model)
        {
            var element = _testingtStorage.GetElement(new TestingBindingModel
            {
                TeacherId = model.TeacherId,
                Naming = model.Naming
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть испытание с таким названием");
            }
            if (model.Id.HasValue)
            {
                _testingtStorage.Update(model);
            }
            else
            {
                _testingtStorage.Insert(model);
            }
        }
        public void Delete(TestingBindingModel model)
        {
            var element = _testingtStorage.GetElement(new TestingBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Испытание не найдено");
            }
            _testingtStorage.Delete(model);
        }
        public void AddStudent(StudentTestingBindingModel model)
        {
            var testing = _testingtStorage.GetElement(new TestingBindingModel
            {
                Id = model.TestingId
            });

            var student = _studentStorage.GetElement(new StudentBindingModel
            {
                Id = model.StudentId
            });

            if (testing == null)
            {
                throw new Exception("Испытание не найдено");
            }

            if (student == null)
            {
                throw new Exception("Студент не найден");
            }

            Dictionary<int, StudentTestingViewModel> testingStudents = testing.StudentTestings;

            if (testingStudents.ContainsKey(model.StudentId))
            {
                testingStudents[model.StudentId] = new StudentTestingViewModel
                {
                    NumbGb = testingStudents[model.StudentId].NumbGb, 
                    Flm = testingStudents[model.StudentId].Flm,
                    Mark = model.Mark 
                };
            }
            else
            {
                testingStudents.Add(model.StudentId, new StudentTestingViewModel
                {
                    NumbGb = student.NumbGB,
                    Flm = student.Surname + " " + student.Name[0] + ". " + student.MiddleName[0] + ". ", 
                    Mark = model.Mark
                });
            }

            var testingBindingModel = new TestingBindingModel
            {
                Id = testing.Id,
                LessonId = testing.LessonId,
                Naming = testing.Naming,
                Type = testing.Type,
                StudentTestings = new Dictionary<int, (string, string, Marks)>() 
            };

            foreach (var value in testingStudents)
            {
                testingBindingModel.StudentTestings.Add(value.Key, (value.Value.NumbGb, value.Value.Flm,
                    value.Value.Mark));
            }

            _testingtStorage.Update(testingBindingModel);
        }
        public void DeleteStudent(StudentTestingBindingModel model)
        {
            var testing = _testingtStorage.GetElement(new TestingBindingModel
            {
                Id = model.TestingId
            });

            var student = _studentStorage.GetElement(new StudentBindingModel
            {
                Id = model.StudentId
            });

            if (testing == null)
            {
                throw new Exception("Испытание не найдено");
            }

            if (student == null)
            {
                throw new Exception("Студент не найден");
            }
            Dictionary<int, StudentTestingViewModel> testingStudents = testing.StudentTestings;
            if (testingStudents.ContainsKey(model.StudentId))
            {
                testingStudents.Remove(model.StudentId);
            } else
            {
                throw new Exception("Запись не найдена");
            }

            var testingBindingModel = new TestingBindingModel
            {
                Id = testing.Id,
                LessonId = testing.LessonId,
                Naming = testing.Naming,
                Type = testing.Type,
                StudentTestings = new Dictionary<int, (string, string, Marks)>()
            };

            foreach (var value in testingStudents)
            {
                testingBindingModel.StudentTestings.Add(value.Key, (value.Value.NumbGb, value.Value.Flm,
                    value.Value.Mark));
            }

            _testingtStorage.Update(testingBindingModel);
        }
    }
}
