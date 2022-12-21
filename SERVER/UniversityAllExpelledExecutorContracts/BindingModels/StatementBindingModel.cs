using System;
using System.Collections.Generic;
using UniversityAllExpelledExecutorContracts.Enums;

namespace UniversityAllExpelledExecutorContracts.BindingModels
{
    public class StatementBindingModel
    {
        public int? Id { get; set; }
        public int TeacherId { get; set; }
        public string Naming { get; set; }
        public string DateCreate { get; set; }
        public string? DateFrom { get; set; }
        public string? DateTo { get; set; }
        public Dictionary<int, (string, string, Achievements)> StudentStatements { get; set; }
    }
}
