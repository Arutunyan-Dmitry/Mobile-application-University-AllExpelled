using System.ComponentModel.DataAnnotations;

namespace UniversityAllExpelledExecutorContracts.Enums
{
    public enum TestingTypes
    {
        [Display(Name = "Лабораторная")]
        Лабораторная = 0,
        [Display(Name = "Лекция")]
        Лекция = 1,
        [Display(Name = "Тест")]
        Тест = 2,
        [Display(Name = "Другое")]
        Другое = 3
    }
}
