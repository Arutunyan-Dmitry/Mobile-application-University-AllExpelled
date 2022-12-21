using System.Collections.Generic;
using UniversityAllExpelledExecutorBusinessLogic.OfficePackage.HelperEnums;

namespace UniversityAllExpelledExecutorBusinessLogic.OfficePackage.HelperModels
{
    public class PdfRowParameters
    {
        public List<string> Texts { get; set; }
        public string Style { get; set; }
        public PdfParagraphAlignmentType ParagraphAlignment { get; set; }
    }
}