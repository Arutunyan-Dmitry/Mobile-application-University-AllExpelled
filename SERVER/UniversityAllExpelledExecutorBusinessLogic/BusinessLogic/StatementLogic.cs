using System;
using System.Collections.Generic;
using UniversityAllExpelledExecutorContracts.BindingModels;
using UniversityAllExpelledExecutorContracts.ViewModels;
using UniversityAllExpelledExecutorContracts.BusinessLogicContracts;
using UniversityAllExpelledExecutorContracts.StorageContracts;
using UniversityAllExpelledExecutorContracts.Enums;

namespace UniversityAllExpelledExecutorBusinessLogic.BusinessLogic
{
    public class StatementLogic : IStatementLogic
    {
        private readonly IStatementStorage _statementStorage;
        private readonly IStudentStorage _studentStorage;
        public StatementLogic(IStatementStorage statementStorage, IStudentStorage studentStorage)
        {
            _statementStorage = statementStorage;
            _studentStorage = studentStorage;
        }
        public List<StatementViewModel> Read(StatementBindingModel model)
        {
            if (model == null)
            {
                return _statementStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<StatementViewModel> { _statementStorage.GetElement(model) };
            }
            return _statementStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(StatementBindingModel model)
        {
            var element = _statementStorage.GetElement(new StatementBindingModel
            {
                TeacherId = model.TeacherId,
                Naming = model.Naming
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть ведомость с таким названием");
            }
            if (model.Id.HasValue)
            {
                _statementStorage.Update(model);
            }
            else
            {
                _statementStorage.Insert(model);
            }
        }
        public void Delete(StatementBindingModel model)
        {
            var element = _statementStorage.GetElement(new StatementBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Ведомость не найдена");
            }
            _statementStorage.Delete(model);
        }
        public void AddStudent(StudentStatementBindingModel model)
        {
            var statement = _statementStorage.GetElement(new StatementBindingModel
            {
                Id = model.StatementId
            });

            var student = _studentStorage.GetElement(new StudentBindingModel
            {
                Id = model.StudentId
            });

            if (statement == null)
            {
                throw new Exception("Ведомость не найдена");
            }

            if (student == null)
            {
                throw new Exception("Студент не найден");
            }

            Dictionary<int, StudentStatementsViewModel> statementStudents = statement.StudentStatements;

            if (statementStudents.ContainsKey(model.StudentId))
            {
                statementStudents[model.StudentId] = new StudentStatementsViewModel
                {
                    NumbGb = statementStudents[model.StudentId].NumbGb,
                    Flm = statementStudents[model.StudentId].Flm,
                    Achievement = model.Achievement
                };
            }
            else
            {
                statementStudents.Add(model.StudentId, new StudentStatementsViewModel
                {
                    NumbGb = student.NumbGB,
                    Flm = student.Surname + " " + student.Name[0] + ". " + student.MiddleName[0] + ". ",
                    Achievement = model.Achievement
                });
            }

            var statementBindingModel = new StatementBindingModel
            {
                Id = statement.Id,
                Naming = statement.Naming,
                DateCreate = statement.DateCreate,
                StudentStatements = new Dictionary<int, (string, string, Achievements)>()
            };

            foreach(var value in statementStudents)
            {
                statementBindingModel.StudentStatements.Add(value.Key, (value.Value.NumbGb, value.Value.Flm,
                    value.Value.Achievement));
            }

            _statementStorage.Update(statementBindingModel);
        }
        public void DeleteStudent(StudentStatementBindingModel model)
        {
            var statement = _statementStorage.GetElement(new StatementBindingModel
            {
                Id = model.StatementId
            });

            var student = _studentStorage.GetElement(new StudentBindingModel
            {
                Id = model.StudentId
            });

            if (statement == null)
            {
                throw new Exception("Ведомость не найдена");
            }

            if (student == null)
            {
                throw new Exception("Студент не найден");
            }
            Dictionary<int, StudentStatementsViewModel> statementStudents = statement.StudentStatements;
            if (statementStudents.ContainsKey(model.StudentId))
            {
                statementStudents.Remove(model.StudentId);
            }
            else
            {
                throw new Exception("Запись не найдена");
            }

            var statementBindingModel = new StatementBindingModel
            {
                Id = statement.Id,
                Naming = statement.Naming,
                DateCreate = statement.DateCreate,
                StudentStatements = new Dictionary<int, (string, string, Achievements)>()
            };

            foreach (var value in statementStudents)
            {
                statementBindingModel.StudentStatements.Add(value.Key, (value.Value.NumbGb, value.Value.Flm,
                    value.Value.Achievement));
            }

            _statementStorage.Update(statementBindingModel);
        }
    }
}
