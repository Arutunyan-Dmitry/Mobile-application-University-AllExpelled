using UniversityAllExpelledDataBaseImplement.Implements;
using UniversityAllExpelledSuretyContracts.BindingModel;
using UniversityAllExpelledExecutorContracts.BindingModels;

int TeacherId = 0;
Random rnd = new Random();
DepartmentStorage storageD = new DepartmentStorage();
LessonStorage storageL = new LessonStorage();
StatementStorage storageS = new StatementStorage();
DisciplineStorage storageDc = new DisciplineStorage();

Console.WriteLine("This is for adding some entities");
Console.WriteLine("Lets start");
Console.WriteLine("");
Console.WriteLine("Now Im creating department");
/*storageD.Insert(new DepartmentBindingModel
{
    DepartmentName = "Первая кафедра",
    Login = "test@mail.ru",
    Password = "qwerty-1234"
});*/
Console.WriteLine("");
Console.WriteLine("Department created! {Первая кафедра, test@mail.ru, qwerty-1234}");
Console.WriteLine("");
Console.WriteLine("Now I will create 3 lesson to it");
/*storageL.Insert(new LessonBindingModel
{
    LessonName = "Первое занятие",
    LessonDate = DateTime.Parse("21/04/2003"),
    LessonTypeReporting = new Dictionary<int, string>()
});
storageL.Insert(new LessonBindingModel
{
    LessonName = "Второе занятие",
    LessonDate = DateTime.Parse("27/04/2003"),
    LessonTypeReporting = new Dictionary<int, string>()
});
storageL.Insert(new LessonBindingModel
{
    LessonName = "Третье занятие",
    LessonDate = DateTime.Parse("29/04/2003"),
    LessonTypeReporting = new Dictionary<int, string>()
});*/
Console.WriteLine("");
Console.WriteLine("Huh, thats was challenging, wasnt it?");
Console.WriteLine("And now the hardest part, be ready");
Console.WriteLine("");
Console.WriteLine("I will add 3 disciplines and connect them to your statements");
Console.WriteLine("Please, enter your teacher id ");
int.TryParse(Console.ReadLine(), out TeacherId); // 1 - test@mail.ru 8 - retr073@yandex.ru
Console.WriteLine("");
Console.WriteLine("Cool! Okay, im going in");
Console.WriteLine("");
var statements = storageS.GetFilteredList(new StatementBindingModel
{
    TeacherId= TeacherId,
});

storageDc.Insert(new DisciplineBindingModel
{
    DepartmentId = 1,
    DisciplineName = "Основы профессионального права",
    StatementId = rnd.Next(1, statements.Count),
    DisciplineTypeReporting = new Dictionary<int, string>()
});
storageDc.Insert(new DisciplineBindingModel
{
    DepartmentId = 1,
    DisciplineName = "Разработка мобильных приложений",
    StatementId = rnd.Next(1, statements.Count),
    DisciplineTypeReporting = new Dictionary<int, string>()
});
storageDc.Insert(new DisciplineBindingModel
{
    DepartmentId = 1,
    DisciplineName = "Методы искуственного интеллекта",
    StatementId = rnd.Next(1, statements.Count),
    DisciplineTypeReporting = new Dictionary<int, string>()
});
storageDc.Insert(new DisciplineBindingModel
{
    DepartmentId = 1,
    DisciplineName = "Компонентно-ориентированное программирование",
    StatementId = rnd.Next(1, statements.Count),
    DisciplineTypeReporting = new Dictionary<int, string>()
});
storageDc.Insert(new DisciplineBindingModel
{
    DepartmentId = 1,
    DisciplineName = "Вычислительная математика",
    StatementId = rnd.Next(1, statements.Count),
    DisciplineTypeReporting = new Dictionary<int, string>()
});
storageDc.Insert(new DisciplineBindingModel
{
    DepartmentId = 1,
    DisciplineName = "Системный анализ",
    StatementId = rnd.Next(1, statements.Count),
    DisciplineTypeReporting = new Dictionary<int, string>()
});

Console.WriteLine("We did it! We are such a good programmers!");
Console.WriteLine("It was a great pleasure to work with you :З ");
