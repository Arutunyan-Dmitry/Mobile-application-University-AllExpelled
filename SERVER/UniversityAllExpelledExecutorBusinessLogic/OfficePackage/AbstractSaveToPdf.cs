using System.Collections.Generic;
using UniversityAllExpelledExecutorBusinessLogic.OfficePackage.HelperEnums;
using UniversityAllExpelledExecutorBusinessLogic.OfficePackage.HelperModels;

namespace UniversityAllExpelledExecutorBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToPdf
    {
        public void CreateDoc(PdfInfo info)
        {
            CreatePdf();
            CreateParagraph(new PdfParagraph
            {
                Text = info.Title,
                Style = "NormalTitle"
            });
            CreateParagraph(new PdfParagraph
            {
                Text = $"с { info.DateFrom.ToShortDateString() } по " +
                $"{  info.DateTo.ToShortDateString() }",
                Style = "Normal"
            });
            CreateTable(new List<string> { "2cm", "6cm", "5cm", "2cm", "3cm", "5cm", "2cm" });
            CreateRow(new PdfRowParameters
            {
                Texts = new List<string> { "№ЗК", "ФИО студента", "Название испытания", "Оценка", "Дата", "Ведомость", "Рез." },
                Style = "NormalTitle",
                ParagraphAlignment = PdfParagraphAlignmentType.Center
            });
            foreach (var sst in info.StudStatTest)
            {
                CreateRow(new PdfRowParameters
                {
                    Texts = new List<string> { sst.NumbGB, sst.FLM, "", "", "", "", "" },
                    Style = "Normal",
                    ParagraphAlignment = PdfParagraphAlignmentType.Left
                });
                int tmpTestings = sst.StudTestings.Count;
                int tmpStatements = sst.StudStatements.Count;

                int tmp;
                if (tmpTestings > tmpStatements)
                    tmp = tmpTestings;
                else
                    tmp = tmpStatements;

                for (int i = 0; i < tmp; i++)
                {
                    string str1 = "";
                    string str2 = "";
                    string str3 = "";
                    string str4 = "";
                    string str5 = "";

                    if (sst.StudTestings != null)
                    {
                        if(sst.StudTestings.Count > i)
                        {
                            str1 = sst.StudTestings[i].Item1;
                            str2 = sst.StudTestings[i].Item2.ToString();
                        } 
                    }

                    if (sst.StudStatements != null)
                    {
                        if (sst.StudStatements.Count > i)
                        {
                            str3 = sst.StudStatements[i].Item1.ToShortDateString();
                            str4 = sst.StudStatements[i].Item2;
                            str5 = sst.StudStatements[i].Item3.ToString();
                        }
                    }

                    CreateRow(new PdfRowParameters
                    {            
                        Texts = new List<string> { "", "", str1, str2, str3, str4, str5 },
                        Style = "Normal",
                        ParagraphAlignment = PdfParagraphAlignmentType.Left
                    }); ;
                }
            }
            SavePdf(info.FileName);
        }      

        /// <summary>
        /// Создание doc-файла
        /// </summary>
        protected abstract void CreatePdf();
        /// <summary>
        /// Создание параграфа с текстом
        /// </summary>
        /// <param name="title"></param>
        /// <param name="style"></param>
        protected abstract void CreateParagraph(PdfParagraph paragraph);
        /// <summary>
        /// Создание таблицы
        /// </summary>
        /// <param name="title"></param>
        /// <param name="style"></param>
        protected abstract void CreateTable(List<string> columns);
        /// <summary>
        /// Создание и заполнение строки
        /// </summary>
        /// <param name="rowParameters"></param>
        protected abstract void CreateRow(PdfRowParameters rowParameters);
        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <param name="fileName"></param>
        protected abstract void SavePdf(string fileName);
    }
}
