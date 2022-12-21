using System.Collections.Generic;
using UniversityAllExpelledExecutorBusinessLogic.OfficePackage.HelperEnums;
using UniversityAllExpelledExecutorBusinessLogic.OfficePackage.HelperModels;

namespace UniversityAllExpelledExecutorBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToWord
    {
        public void CreateDoc(WordInfo info)
        {
            CreateWord(info.FileName);
            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)> {
                (info.Title, new WordTextProperties { Bold = true, Size = "24", })},
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });
            CreateTable(new List<string>() { "№ ЗК", "ФИО студента", "Дисциплины" });
            foreach (var studDisc in info.StudDisc)
            {
                CreateRow(new List<string>() {
                    studDisc.NumbGB,
                    studDisc.FLM,
                    ""
                });
                foreach (var discipline in studDisc.DisciplineNames)
                {
                    CreateRow(new List<string>() {
                        "",
                        "",
                        discipline
                    });
                }
            }
            SaveWord();
        }     
        /// <summary>
        /// Создание doc-файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void CreateWord(string info);
        /// <summary>
        /// Создание абзаца с текстом
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        protected abstract void CreateParagraph(WordParagraph paragraph);
        /// <summary>
        /// Создание заголовка таблицы с текстом
        /// </summary>
        /// <param name="tableHeader"></param>
        protected abstract void CreateTable(List<string> tableHeader);
        /// <summary>
        /// Создание строки таблицы с текстом
        /// </summary>
        /// <param name="tableRow"></param>
        protected abstract void CreateRow(List<string> tableRow);
        /// <summary>
        /// Сохранение файла
        /// </summary>
        protected abstract void SaveWord();
    }
}
