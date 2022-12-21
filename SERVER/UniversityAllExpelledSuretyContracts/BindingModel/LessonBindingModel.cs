using System;
using System.Collections.Generic;

namespace UniversityAllExpelledSuretyContracts.BindingModel
{
    public class LessonBindingModel
    {
        public int? Id { get; set; }
        public int? TestingId { get; set; }
        public string LessonName { get; set; }
        public DateTime LessonDate { get; set; }
        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo
        {
            get; set;
        }
        public Dictionary<int, string> LessonTypeReporting { get; set; }
        public int DepartmentId { get; set; }
    }
}
