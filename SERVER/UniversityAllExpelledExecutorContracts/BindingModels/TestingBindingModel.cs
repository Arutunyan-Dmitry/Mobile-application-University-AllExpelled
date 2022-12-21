using UniversityAllExpelledExecutorContracts.Enums;
using System.Collections.Generic;

namespace UniversityAllExpelledExecutorContracts.BindingModels
{
    public class TestingBindingModel
    {
        public int? Id { get; set; }
        public int TeacherId { get; set; }
        public int LessonId { get; set; }
        public string Naming { get; set; }
        public TestingTypes Type { get; set; }
        public Dictionary<int, (string, string, Marks)> StudentTestings { get; set; }
    }
}
