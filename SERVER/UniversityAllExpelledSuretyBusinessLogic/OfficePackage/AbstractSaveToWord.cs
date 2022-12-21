using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityAllExpelledSuretyBusinessLogic.OfficePackage.HelperEnums;
using UniversityAllExpelledSuretyBusinessLogic.OfficePackage.HelperModels;


namespace UniversityAllExpelledSuretyBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToWord
    {
        public void CreateDoc(WordInfo info)
        {
            CreateWord(info);
            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)> { (info.Title, new
WordTextProperties { Bold = true, Size = "24", }) },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });           
            SaveWord(info);
        }
        /// <summary>
        /// Создание doc-файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void CreateWord(WordInfo info);
        /// <summary>
        /// Создание абзаца с текстом
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        protected abstract void CreateParagraph(WordParagraph paragraph);
        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void SaveWord(WordInfo info);

        /// <summary>
        /// Создание строки таблицы с текстом
        /// </summary>
        /// <param name="tableRow"></param>
        protected abstract void CreateRow(List<string> tableRow);
        /// <summary>
        /// Создание заголовка таблицы с текстом
        /// </summary>
        /// <param name="tableHeader"></param>
        protected abstract void CreateTable(List<string> tableHeader);

        public void CreateTableDoc(WordInfo info)
        {
            CreateWord(info);
            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)> { (info.Title, new
WordTextProperties { Bold = true, Size = "24", }) },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });
            CreateTable(new List<string>()
            {
                 "Номер отчета", "Название отчета", "Название испытания", "Тип испытания"
            });
            foreach (var testingReporting in info.TestingTypeReporting)
            {
                string tmpTestingName = "";
                string tmpTestingType = "";
                CreateRow(new List<string>()
                        {
                             testingReporting.ReportNumber.ToString(),
                             testingReporting.ReportName,
                             "",
                             ""
                        });
                foreach (KeyValuePair<string, string> keyValues in testingReporting.TestingNameType)
                {
                    tmpTestingName = keyValues.Key;
                    tmpTestingType = keyValues.Value;                       
                        CreateRow(new List<string>()
                        {
                             "",
                             "",
                             tmpTestingName,
                             tmpTestingType
                        });
                }
            }
            SaveWord(info);
        }

    }
}
