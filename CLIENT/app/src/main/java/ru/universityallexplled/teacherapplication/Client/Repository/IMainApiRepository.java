package ru.universityallexplled.teacherapplication.Client.Repository;

import java.util.List;

import io.reactivex.Single;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.Query;
import ru.universityallexplled.teacherapplication.Client.DtoModels.LessonDto;
import ru.universityallexplled.teacherapplication.Client.DtoModels.ReportStudentDisciplinesDto;
import ru.universityallexplled.teacherapplication.Client.DtoModels.SendMailDto;
import ru.universityallexplled.teacherapplication.Client.DtoModels.StatementDto;
import ru.universityallexplled.teacherapplication.Client.DtoModels.StudentDto;
import ru.universityallexplled.teacherapplication.Client.DtoModels.StudentStatementsDto;
import ru.universityallexplled.teacherapplication.Client.DtoModels.StudentTestingDto;
import ru.universityallexplled.teacherapplication.Client.DtoModels.TeacherDto;
import ru.universityallexplled.teacherapplication.Client.DtoModels.TestingDto;

public interface IMainApiRepository {

    //----------MAIN----------------

    @GET("/api/Main/GetAllLessons")
    Single<List<LessonDto>> GetAllLessons();

    @GET("/api/Main/GetStudents")
    Single<List<StudentDto>> GetStudents(@Query("teacherId") int teacherId);

    @GET("/api/Main/GetStatements")
    Single<List<StatementDto>> GetStatements(@Query("teacherId") int teacherId);

    @GET("/api/Main/GetTestings")
    Single<List<TestingDto>> GetTestings(@Query("teacherId") int teacherId);


    @POST("/api/Main/CreateOrUpdateStudent")
    Single<Boolean> CreateOrUpdateStudent(@Body StudentDto studentDto);

    @POST("/api/Main/DeleteStudent")
    Single<Boolean> DeleteStudent(@Body StudentDto studentDto);


    @POST("/api/Main/CreateOrUpdateStatement")
    Single<Boolean> CreateOrUpdateStatement(@Body StatementDto statementDto);

    @POST("/api/Main/DeleteStatement")
    Single<Boolean> DeleteStatement(@Body StatementDto statementDto);

    @POST("/api/Main/AddStudentToStatement")
    Single<Boolean> AddStudentToStatement(@Body StudentStatementsDto studentStatementsDto);

    @POST("/api/Main/DeleteStudentFromStatement")
    Single<Boolean> DeleteStudentFromStatement(@Body StudentStatementsDto studentStatementsDto);


    @POST("/api/Main/CreateOrUpdateTesting")
    Single<Boolean> CreateOrUpdateTesting(@Body TestingDto testingDto);

    @POST("/api/Main/DeleteTesting")
    Single<Boolean> DeleteTesting(@Body TestingDto testingDto);

    @POST("/api/Main/AddStudentToTesting")
    Single<Boolean> AddStudentToTesting(@Body StudentTestingDto studentTestingDto);

    @POST("/api/Main/DeleteStudentFromTesting")
    Single<Boolean> DeleteStudentFromTesting(@Body StudentTestingDto studentTestingDto);

    //--------TEACHER---------

    @GET("/api/Teacher/Login")
    Single<TeacherDto> LogInTeacher(@Query("login") String login, @Query("password") String password);

    @POST("/api/Teacher/Register")
    Single<Boolean> RegisterTeacher(@Body TeacherDto teacherDto);

    //---------ADMIN-----------

    @GET("/api/Test/GetAllTeachers")
    Single<List<TeacherDto>> GetAllTeachers();

    //--------REPORTS----------

    @POST("/api/Report/SendMessage")
    Single<Boolean> SendMessage(@Body SendMailDto sendMailDto);

    @GET("/api/Report/GetStudentDisciplines")
    Single<List<ReportStudentDisciplinesDto>> GetStudentDisciplines(@Query("students") String params);

}
