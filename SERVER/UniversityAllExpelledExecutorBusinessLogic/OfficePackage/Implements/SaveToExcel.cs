using UniversityAllExpelledExecutorBusinessLogic.OfficePackage.HelperEnums;
using UniversityAllExpelledExecutorBusinessLogic.OfficePackage.HelperModels;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2013.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Linq;

namespace UniversityAllExpelledExecutorBusinessLogic.OfficePackage.Implements
{
    public class SaveToExcel : AbstractSaveToExcel
    {
        private SpreadsheetDocument _spreadsheetDocument;
        private SharedStringTablePart _shareStringPart;
        private Worksheet _worksheet;
        /// <summary>
        /// Настройка стилей для файла
        /// </summary>
        /// <param name="workbookpart"></param>
        private static void CreateStyles(WorkbookPart workbookpart)
        {
            var sp = workbookpart.AddNewPart<WorkbookStylesPart>();
            sp.Stylesheet = new Stylesheet();
            var fonts = new Fonts() { Count = 2U, KnownFonts = true };
            var fontUsual = new Font();
            fontUsual.Append(new FontSize() { Val = 12D });
            fontUsual.Append(new DocumentFormat.OpenXml.Office2010.Excel.Color()
            {
                Theme
           = 1U
            });
            fontUsual.Append(new FontName() { Val = "Times New Roman" });
            fontUsual.Append(new FontFamilyNumbering() { Val = 2 });
            fontUsual.Append(new FontScheme() { Val = FontSchemeValues.Minor });
            var fontTitle = new Font();
            fontTitle.Append(new Bold());
            fontTitle.Append(new FontSize() { Val = 14D });
            fontTitle.Append(new DocumentFormat.OpenXml.Office2010.Excel.Color()
            {
                Theme
           = 1U
            });
            fontTitle.Append(new FontName() { Val = "Times New Roman" });
            fontTitle.Append(new FontFamilyNumbering() { Val = 2 });
            fontTitle.Append(new FontScheme() { Val = FontSchemeValues.Minor });
            fonts.Append(fontUsual);
            fonts.Append(fontTitle);
            var fills = new Fills() { Count = 2U };
            var fill1 = new Fill();
            fill1.Append(new PatternFill() { PatternType = PatternValues.None });
            var fill2 = new Fill();
            fill2.Append(new PatternFill() { PatternType = PatternValues.Gray125 });
            fills.Append(fill1);
            fills.Append(fill2);
            var borders = new Borders() { Count = 2U };
            var borderNoBorder = new Border();
            borderNoBorder.Append(new LeftBorder());
            borderNoBorder.Append(new RightBorder());
            borderNoBorder.Append(new TopBorder());
            borderNoBorder.Append(new BottomBorder());
            borderNoBorder.Append(new DiagonalBorder());
            var borderThin = new Border();
            var leftBorder = new LeftBorder() { Style = BorderStyleValues.Thin };
            leftBorder.Append(new DocumentFormat.OpenXml.Office2010.Excel.Color()
            {
                Indexed = 64U
            });
            var rightBorder = new RightBorder() { Style = BorderStyleValues.Thin };
            rightBorder.Append(new DocumentFormat.OpenXml.Office2010.Excel.Color()
            {
                Indexed = 64U
            });
            var topBorder = new TopBorder() { Style = BorderStyleValues.Thin };
            topBorder.Append(new DocumentFormat.OpenXml.Office2010.Excel.Color()
            {
                Indexed = 64U
            });
            var bottomBorder = new BottomBorder() { Style = BorderStyleValues.Thin };
            bottomBorder.Append(new DocumentFormat.OpenXml.Office2010.Excel.Color()
            {
                Indexed = 64U
            });
            borderThin.Append(leftBorder);
            borderThin.Append(rightBorder);
            borderThin.Append(topBorder);
            borderThin.Append(bottomBorder);
            borderThin.Append(new DiagonalBorder());
            borders.Append(borderNoBorder);
            borders.Append(borderThin);
            var cellStyleFormats = new CellStyleFormats() { Count = 1U };
            var cellFormatStyle = new CellFormat()
            {
                NumberFormatId = 0U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 0U
            };
            cellStyleFormats.Append(cellFormatStyle);
            var cellFormats = new CellFormats() { Count = 3U };
            var cellFormatFont = new CellFormat()
            {
                NumberFormatId = 0U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyFont = true
            };
            var cellFormatFontAndBorder = new CellFormat()
            {
                NumberFormatId = 0U,
                FontId
           = 0U,
                FillId = 0U,
                BorderId = 1U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true
            };
            var cellFormatTitle = new CellFormat()
            {
                NumberFormatId = 0U,
                FontId = 1U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                Alignment = new Alignment()
                {
                    Vertical = VerticalAlignmentValues.Center,
                    WrapText = true,
                    Horizontal = HorizontalAlignmentValues.Center
                },
                ApplyFont = true
            };
            cellFormats.Append(cellFormatFont);
            cellFormats.Append(cellFormatFontAndBorder);
            cellFormats.Append(cellFormatTitle);
            var cellStyles = new CellStyles() { Count = 1U };
            cellStyles.Append(new CellStyle()
            {
                Name = "Normal",
                FormatId = 0U,
                BuiltinId
           = 0U
            });
            var differentialFormats = new
           DocumentFormat.OpenXml.Office2013.Excel.DifferentialFormats()
            { Count = 0U };
            var tableStyles = new TableStyles()
            {
                Count = 0U,
                DefaultTableStyle = "TableStyleMedium2",
                DefaultPivotStyle = "PivotStyleLight16"
            };
            var stylesheetExtensionList = new StylesheetExtensionList();
            var stylesheetExtension1 = new StylesheetExtension()
            {
                Uri = "{EB79DEF2-80B8-43e5 - 95BD - 54CBDDF9020C }" };
                stylesheetExtension1.AddNamespaceDeclaration("x14", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
                stylesheetExtension1.Append(new SlicerStyles()
            {
                DefaultSlicerStyle = "SlicerStyleLight1"
            });
            var stylesheetExtension2 = new StylesheetExtension()
            {
                Uri = "{9260A510-F301-46a8 - 8635 - F512D64BE5F5 }" };
            stylesheetExtension2.AddNamespaceDeclaration("x15", "http://schemas.microsoft.com/office/spreadsheetml/2010/11/main");
            stylesheetExtension2.Append(new TimelineStyles()
            {
                DefaultTimelineStyle = "TimeSlicerStyleLight1"
            });
            stylesheetExtensionList.Append(stylesheetExtension1);
            stylesheetExtensionList.Append(stylesheetExtension2);
            sp.Stylesheet.Append(fonts);
            sp.Stylesheet.Append(fills);
            sp.Stylesheet.Append(borders);
            sp.Stylesheet.Append(cellStyleFormats);
            sp.Stylesheet.Append(cellFormats);
            sp.Stylesheet.Append(cellStyles);
            sp.Stylesheet.Append(differentialFormats);
            sp.Stylesheet.Append(tableStyles);
            sp.Stylesheet.Append(stylesheetExtensionList);
        }
        /// <summary>
        /// Получение номера стиля из типа
        /// </summary>
        /// <param name="styleInfo"></param>
        /// <returns></returns>
        private static uint GetStyleValue(ExcelStyleInfoType styleInfo)
        {
            return styleInfo switch
            {
                ExcelStyleInfoType.Title => 2U,
                ExcelStyleInfoType.TextWithBroder => 1U,
                ExcelStyleInfoType.Text => 0U,
                _ => 0U,
            };
        }
        protected override void CreateExcel(string fileName)
        {
            _spreadsheetDocument = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook);
            // Создаем книгу (в ней хранятся листы)
            var workbookpart = _spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();
            CreateStyles(workbookpart);
            // Получаем/создаем хранилище текстов для книги
            _shareStringPart = _spreadsheetDocument.WorkbookPart.GetPartsOfType<SharedStringTablePart>().Any()
            ?
            _spreadsheetDocument.WorkbookPart.GetPartsOfType<SharedStringTablePart>().First(): _spreadsheetDocument.WorkbookPart.AddNewPart<SharedStringTablePart>();
            // Создаем SharedStringTable, если его нет
            if (_shareStringPart.SharedStringTable == null)
            {
                _shareStringPart.SharedStringTable = new SharedStringTable();
            }
            // Создаем лист в книгу
            var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());
            // Добавляем лист в книгу
            var sheets = _spreadsheetDocument.WorkbookPart.Workbook.AppendChild(new Sheets());
            var sheet = new Sheet()
            {
                Id = _spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = "Лист"
            };
            sheets.Append(sheet);
            _worksheet = worksheetPart.Worksheet;
        }
        protected override void InsertCellInWorksheet(ExcelCellParameters excelParams)
        {
            var sheetData = _worksheet.GetFirstChild<SheetData>();
            // Ищем строку, либо добавляем ее
            Row row;
            if (sheetData.Elements<Row>().Where(r => r.RowIndex == excelParams.RowIndex).Any())
            {
                row = sheetData.Elements<Row>().Where(r => r.RowIndex == excelParams.RowIndex).First();
            }
            else
            {
                row = new Row() { RowIndex = excelParams.RowIndex };
                sheetData.Append(row);
            }
            // Ищем нужную ячейку
            Cell cell;
            if (row.Elements<Cell>().Where(c => c.CellReference.Value == excelParams.CellReference).Any())
            {
                cell = row.Elements<Cell>().Where(c => c.CellReference.Value == excelParams.CellReference).First();
            }
            else
            {
                // Все ячейки должны быть последовательно друг за другом расположены
                // нужно определить, после какой вставлять
                Cell refCell = null;
                foreach (Cell rowCell in row.Elements<Cell>())
                {
                    if (string.Compare(rowCell.CellReference.Value, excelParams.CellReference, true) > 0)
                    {
                        refCell = rowCell;
                        break;
                    }
                }
                var newCell = new Cell() { CellReference = excelParams.CellReference };
                row.InsertBefore(newCell, refCell);
                cell = newCell;
            }
            // вставляем новый текст
            _shareStringPart.SharedStringTable.AppendChild(new SharedStringItem(new Text(excelParams.Text)));
            _shareStringPart.SharedStringTable.Save();
            cell.CellValue = new CellValue((_shareStringPart.SharedStringTable.Elements<SharedStringItem>().Count() - 1).ToString());
            cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
            cell.StyleIndex = GetStyleValue(excelParams.StyleInfo);
        }
        protected override void MergeCells(ExcelMergeParameters excelParams)
        {
            MergeCells mergeCells;
            if (_worksheet.Elements<MergeCells>().Any())
            {
                mergeCells = _worksheet.Elements<MergeCells>().First();
            }
            else
            {
                mergeCells = new MergeCells();
                if (_worksheet.Elements<CustomSheetView>().Any())
                {
                    _worksheet.InsertAfter(mergeCells, _worksheet.Elements<CustomSheetView>().First());
                }
                else
                {
                    _worksheet.InsertAfter(mergeCells, _worksheet.Elements<SheetData>().First());
                }
            }
            var mergeCell = new MergeCell()
            {
                Reference = new StringValue(excelParams.Merge)
            };
            mergeCells.Append(mergeCell);
        }
        protected override void SaveExcel()
        {
            _spreadsheetDocument.WorkbookPart.Workbook.Save();
            _spreadsheetDocument.Close();
        }
    }
}
