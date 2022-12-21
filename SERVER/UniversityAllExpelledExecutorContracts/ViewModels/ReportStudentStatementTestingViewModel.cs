using System;
using System.Collections.Generic;
using UniversityAllExpelledExecutorContracts.Enums;

namespace UniversityAllExpelledExecutorContracts.ViewModels
{
    public class ReportStudentStatementTestingViewModel
    {
        public string NumbGB { get; set; }
        public string FLM { get; set; }
        public List<Tuple<string, Marks>> StudTestings { get; set; }
        public List<Tuple<DateTime, string, Achievements>> StudStatements { get; set; }
    }
}
