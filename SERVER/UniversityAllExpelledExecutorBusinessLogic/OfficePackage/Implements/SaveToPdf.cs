using UniversityAllExpelledExecutorBusinessLogic.OfficePackage.HelperEnums;
using UniversityAllExpelledExecutorBusinessLogic.OfficePackage.HelperModels;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System.Collections.Generic;

namespace UniversityAllExpelledExecutorBusinessLogic.OfficePackage.Implements
{
    public class SaveToPdf : AbstractSaveToPdf
    {
        private Document _document;
        private Section _section;
        private Table _table;
        private static ParagraphAlignment GetParagraphAlignment(PdfParagraphAlignmentType type)
        {
            return type switch
            {
                PdfParagraphAlignmentType.Center => ParagraphAlignment.Center,
                PdfParagraphAlignmentType.Left => ParagraphAlignment.Left,
                _ => ParagraphAlignment.Justify,
            };
        }
        /// <summary>
        /// Создание стилей для документа
        /// </summary>
        /// <param name="document"></param>
        private static void DefineStyles(Document document)
        {
            document.DefaultPageSetup.Orientation = Orientation.Landscape;
            var style = document.Styles["Normal"];
            style.Font.Name = "Times New Roman";
            style.Font.Size = 14;
            style = document.Styles.AddStyle("NormalTitle", "Normal");
            style.Font.Bold = true;
        }
        protected override void CreatePdf()
        {
            _document = new Document();
            DefineStyles(_document);
            _section = _document.AddSection();
        }
        protected override void CreateParagraph(PdfParagraph pdfParagraph)
        {
            var paragraph = _section.AddParagraph(pdfParagraph.Text);
            paragraph.Format.SpaceAfter = "0.4cm";
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Style = pdfParagraph.Style;
        }
        protected override void CreateTable(List<string> columns)
        {
            _table = _document.LastSection.AddTable();
            foreach (var elem in columns)
            {
                _table.AddColumn(elem);
            }
        }
        protected override void CreateRow(PdfRowParameters rowParameters)
        {
            var row = _table.AddRow();
            for (int i = 0; i < rowParameters.Texts.Count; ++i)
            {
                row.Cells[i].AddParagraph(rowParameters.Texts[i]);
                if (!string.IsNullOrEmpty(rowParameters.Style))
                {
                    row.Cells[i].Style = rowParameters.Style;
                }
                Unit borderWidth = 0.5;
                row.Cells[i].Borders.Left.Width = borderWidth;
                row.Cells[i].Borders.Right.Width = borderWidth;
                row.Cells[i].Borders.Top.Width = borderWidth;
                row.Cells[i].Borders.Bottom.Width = borderWidth;
                row.Cells[i].Format.Alignment =
               GetParagraphAlignment(rowParameters.ParagraphAlignment);
                row.Cells[i].VerticalAlignment = VerticalAlignment.Center;
            }
        }
        protected override void SavePdf(string fileName)
        {
            var renderer = new PdfDocumentRenderer(true)
            {
                Document = _document
            };
            renderer.RenderDocument();
            renderer.PdfDocument.Save(fileName);
        }
    }
}