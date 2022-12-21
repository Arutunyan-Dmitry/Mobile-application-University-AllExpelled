using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityAllExpelledSuretyBusinessLogic.OfficePackage.HelperEnums;
using UniversityAllExpelledSuretyBusinessLogic.OfficePackage.HelperModels;

namespace UniversityAllExpelledSuretyBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToExcel
    {
        /// <summary>
        /// Создание отчета
        /// </summary>
        /// <param name="info"></param>
        public void CreateReport(ExcelInfo info)
        {
            CreateExcel(info);
            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "A",
                RowIndex = 1,
                Text = info.Title,
                StyleInfo = ExcelStyleInfoType.Title
            });
            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "A",
                RowIndex = 2,
                Text = "Номер отчета",
                StyleInfo = ExcelStyleInfoType.Text
            });
            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "B",
                RowIndex = 2,
                Text = "Название отчета",
                StyleInfo = ExcelStyleInfoType.Text
            });
            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "C",
                RowIndex = 2,
                Text = "Название испытания",
                StyleInfo = ExcelStyleInfoType.Text
            });
            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "D",
                RowIndex = 2,
                Text = "Тип испытания",
                StyleInfo = ExcelStyleInfoType.Text
            });
            MergeCells(new ExcelMergeParameters
            {
                CellFromName = "A1",
                CellToName = "C1"
            });
            uint rowIndex = 3;
            foreach (var cd in info.TestingTypeReporting)
            {
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "A",
                    RowIndex = rowIndex,
                    Text = cd.ReportNumber.ToString(),
                    StyleInfo = ExcelStyleInfoType.Text
                });
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "B",
                    RowIndex = rowIndex,
                    Text = cd.ReportName,
                    StyleInfo = ExcelStyleInfoType.Text
                });
                rowIndex++;
                foreach (var testing in cd.TestingNameType)
                {
                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = "C",
                        RowIndex = rowIndex,
                        Text = testing.Key,
                        StyleInfo = ExcelStyleInfoType.TextWithBroder
                    });
                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = "D",
                        RowIndex = rowIndex,
                        Text = testing.Value.ToString(),
                        StyleInfo = ExcelStyleInfoType.TextWithBroder
                    });
                    rowIndex++;
                }
                rowIndex++;
            }
            SaveExcel(info);
        }
        /// <summary>
        /// Создание excel-файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void CreateExcel(ExcelInfo info);
        /// <summary>
        /// Добавляем новую ячейку в лист
        /// </summary>
        /// <param name="cellParameters"></param>
        protected abstract void InsertCellInWorksheet(ExcelCellParameters
        excelParams);
        /// <summary>
        /// Объединение ячеек
        /// </summary>
        /// <param name="mergeParameters"></param>
        protected abstract void MergeCells(ExcelMergeParameters excelParams);
        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void SaveExcel(ExcelInfo info);
    }
}
