using System.ComponentModel;
using UniversityAllExpelledExecutorContracts.Enums;
using System.Collections.Generic;

namespace UniversityAllExpelledExecutorContracts.ViewModels
{
    public class TestingViewModel
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        [DisplayName("Название")]
        public string Naming { get; set; }
        [DisplayName("Тема занятия")]
        public string LessonName { get; set; }
        [DisplayName("Тип")]
        public TestingTypes Type { get; set; }
        public Dictionary<int, StudentTestingViewModel> StudentTestings { get; set; }
        //public Dictionary<int, string> Lessons { get; set; }
    }
}
