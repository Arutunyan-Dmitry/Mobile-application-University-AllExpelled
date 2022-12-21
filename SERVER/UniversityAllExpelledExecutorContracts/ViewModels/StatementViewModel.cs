using System;
using System.Collections.Generic;
using System.ComponentModel;
using UniversityAllExpelledExecutorContracts.Enums;

namespace UniversityAllExpelledExecutorContracts.ViewModels
{
    public class StatementViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название")]
        public string Naming { get; set; }
        [DisplayName("Дата создания")]
        public string DateCreate { get; set; }
        public Dictionary<int, StudentStatementsViewModel> StudentStatements { get; set; }
    }
}
