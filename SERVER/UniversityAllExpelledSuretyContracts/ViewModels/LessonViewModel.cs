using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace UniversityAllExpelledSuretyContracts.ViewModels
{
    public class LessonViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название занятия")]
        public string LessonName { get; set; }
        [DisplayName("Дата занятия")]
        public string LessonDate { get; set; }
        public Dictionary<int, string> LessonTypeReporting { get; set; }
    }
}
