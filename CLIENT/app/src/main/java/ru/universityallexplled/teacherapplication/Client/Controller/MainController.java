package ru.universityallexplled.teacherapplication.Client.Controller;

import androidx.annotation.MainThread;

import java.util.List;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.disposables.CompositeDisposable;
import io.reactivex.functions.BiConsumer;
import io.reactivex.schedulers.Schedulers;
import ru.universityallexplled.teacherapplication.AdminActivity;
import ru.universityallexplled.teacherapplication.Client.APIClient;
import ru.universityallexplled.teacherapplication.Client.DtoModels.LessonDto;
import ru.universityallexplled.teacherapplication.Client.DtoModels.ReportStudentDisciplinesDto;
import ru.universityallexplled.teacherapplication.Client.DtoModels.SendMailDto;
import ru.universityallexplled.teacherapplication.Client.DtoModels.StatementDto;
import ru.universityallexplled.teacherapplication.Client.DtoModels.StudentDto;
import ru.universityallexplled.teacherapplication.Client.DtoModels.StudentStatementsDto;
import ru.universityallexplled.teacherapplication.Client.DtoModels.StudentTestingDto;
import ru.universityallexplled.teacherapplication.Client.DtoModels.TeacherDto;
import ru.universityallexplled.teacherapplication.Client.DtoModels.TestingDto;
import ru.universityallexplled.teacherapplication.Helper.ExcelFileHelper;
import ru.universityallexplled.teacherapplication.Helper.WordFileHelper;
import ru.universityallexplled.teacherapplication.RegistrationActivity;
import ru.universityallexplled.teacherapplication.ReportStudentDisciplineFragment;
import ru.universityallexplled.teacherapplication.ReportStudentTestingFragment;
import ru.universityallexplled.teacherapplication.SignInActivity;
import ru.universityallexplled.teacherapplication.StatementFragment;
import ru.universityallexplled.teacherapplication.StatementsFragment;
import ru.universityallexplled.teacherapplication.StudentFragment;
import ru.universityallexplled.teacherapplication.StudentStatementFragment;
import ru.universityallexplled.teacherapplication.StudentTestingFragment;
import ru.universityallexplled.teacherapplication.StudentsFragment;
import ru.universityallexplled.teacherapplication.TestingFragment;
import ru.universityallexplled.teacherapplication.TestingsFragment;

public class MainController {

    private final APIClient apiClient;
    private final CompositeDisposable disposable;

    public MainController() {
        apiClient = new APIClient();
        disposable = new CompositeDisposable();
    }

    //------------------------------STUDENT------------------------------------------

    public void getStudents(int teacherId, StudentsFragment studentsFragment) {
        StudentsFragment.errorMessage = null;
        StudentsFragment.students = null;
        disposable.add(apiClient.getMainApiRepository().GetStudents(teacherId)
                .subscribeOn(Schedulers.newThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new BiConsumer<List<StudentDto>, Throwable>() {
                    @Override
                    public void accept(List<StudentDto> data, Throwable throwable) throws Exception {
                        if (throwable != null) {
                            StudentsFragment.students = null;
                            StudentsFragment.errorMessage = throwable.getMessage();
                            studentsFragment.onGetData();
                        } else {
                            StudentsFragment.errorMessage = null;
                            StudentsFragment.students = data;
                            studentsFragment.onGetData();
                        }
                    }
                }));
    }

    @MainThread
    public void createOrUpdateStudent(StudentDto studentDto, StudentFragment fragment) {
        StudentFragment.errorMessage = null;
        StudentFragment.gotRequested = null;
        disposable.add(apiClient.getMainApiRepository().CreateOrUpdateStudent(studentDto)
                .subscribeOn(Schedulers.newThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new BiConsumer<Boolean, Throwable>() {
                    @MainThread
                    @Override
                    public void accept(Boolean data, Throwable throwable) throws Exception {
                        if (throwable == null) {
                            // do nothing
                        } else {
                            if(throwable.getClass().getSimpleName().equals("EOFException")) {
                                StudentFragment.errorMessage = null;
                                StudentFragment.gotRequested = Boolean.TRUE;
                                fragment.onCreateOrUpdate();
                            } else {
                                StudentFragment.errorMessage = throwable.getMessage();
                                fragment.onCreateOrUpdate();
                            }
                        }
                    }
                }));
    }

    @MainThread
    public void deleteStudent(StudentDto studentDto, StudentsFragment fragment) {
        StudentsFragment.errorMessage = null;
        StudentsFragment.gotDeleted = null;
        StudentsFragment.students = null;
        disposable.add(apiClient.getMainApiRepository().DeleteStudent(studentDto)
                .subscribeOn(Schedulers.newThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new BiConsumer<Boolean, Throwable>() {
                    @MainThread
                    @Override
                    public void accept(Boolean data, Throwable throwable) throws Exception {
                        if (throwable == null) {
                            // do nothing
                        } else {
                            if(throwable.getClass().getSimpleName().equals("EOFException")) {
                                StudentsFragment.errorMessage = null;
                                StudentsFragment.gotDeleted = Boolean.TRUE;
                                fragment.onDeleteStudent();
                            } else {
                                StudentsFragment.errorMessage = throwable.getMessage();
                                fragment.onDeleteStudent();
                            }
                        }
                    }
                }));
    }

    //------------------------------STUDENT------------------------------------------

    //----------------------------STATEMENT------------------------------------------
    @MainThread
    public void getStatements(int teacherId, StatementsFragment statementsFragment) {
        StatementsFragment.errorMessage = null;
        StatementsFragment.statements = null;
        disposable.add(apiClient.getMainApiRepository().GetStatements(teacherId)
                .subscribeOn(Schedulers.newThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new BiConsumer<List<StatementDto>, Throwable>() {
                    @MainThread
                    @Override
                    public void accept(List<StatementDto> data, Throwable throwable) throws Exception {
                        if (throwable != null) {
                            StatementsFragment.statements = null;
                            StatementsFragment.errorMessage = throwable.getMessage();
                            statementsFragment.onGetData();
                        } else {
                            StatementsFragment.errorMessage = null;
                            StatementsFragment.statements = data;
                            statementsFragment.onGetData();
                        }
                    }
                }));
    }

    @MainThread
    public void CreateOrUpdateStatement(StatementDto statementDto, StatementFragment statementsFragment) {
        StatementFragment.errorMessage = null;
        StatementFragment.gotRequested = null;
        disposable.add(apiClient.getMainApiRepository().CreateOrUpdateStatement(statementDto)
                .subscribeOn(Schedulers.newThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new BiConsumer<Boolean, Throwable>() {
                    @MainThread
                    @Override
                    public void accept(Boolean data, Throwable throwable) throws Exception {
                        if (throwable == null) {
                            // do nothing
                        } else {
                            if (throwable.getClass().getSimpleName().equals("EOFException")) {
                                StatementFragment.errorMessage = null;
                                StatementFragment.gotRequested = Boolean.TRUE;
                                statementsFragment.onCreateOrUpdate();
                            } else {
                                StatementFragment.errorMessage = throwable.getMessage();
                                statementsFragment.onCreateOrUpdate();
                            }
                        }
                    }
                }));
    }

    @MainThread
    public void deleteStatement(StatementDto statementDto, StatementsFragment fragment) {
        StatementsFragment.errorMessage = null;
        StatementsFragment.gotDeleted = null;
        StatementsFragment.statements = null;
        disposable.add(apiClient.getMainApiRepository().DeleteStatement(statementDto)
                .subscribeOn(Schedulers.newThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new BiConsumer<Boolean, Throwable>() {
                    @MainThread
                    @Override
                    public void accept(Boolean data, Throwable throwable) throws Exception {
                        if (throwable == null) {
                            // do nothing
                        } else {
                            if(throwable.getClass().getSimpleName().equals("EOFException")) {
                                StatementsFragment.errorMessage = null;
                                StatementsFragment.gotDeleted = Boolean.TRUE;
                                fragment.onDeleteStatement();
                            } else {
                                StatementsFragment.errorMessage = throwable.getMessage();
                                fragment.onDeleteStatement();
                            }
                        }
                    }
                }));
    }

    public void getStudentsForStatements(int teacherId, StudentStatementFragment studentsFragment) {
        StudentStatementFragment.errorMessage = null;
        StudentStatementFragment.students = null;
        disposable.add(apiClient.getMainApiRepository().GetStudents(teacherId)
                .subscribeOn(Schedulers.newThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new BiConsumer<List<StudentDto>, Throwable>() {
                    @Override
                    public void accept(List<StudentDto> data, Throwable throwable) throws Exception {
                        if (throwable != null) {
                            StudentStatementFragment.students = null;
                            StudentStatementFragment.errorMessage = throwable.getMessage();
                            studentsFragment.onGetData();
                        } else {
                            StudentStatementFragment.errorMessage = null;
                            StudentStatementFragment.students = data;
                            studentsFragment.onGetData();
                        }
                    }
                }));
    }

    @MainThread
    public void addStudentToStatement(StudentStatementsDto dto, StudentStatementFragment fragment) {
        StudentStatementFragment.errorMessage = null;
        StudentStatementFragment.gotRequested = null;
        disposable.add(apiClient.getMainApiRepository().AddStudentToStatement(dto)
                .subscribeOn(Schedulers.newThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new BiConsumer<Boolean, Throwable>() {
                    @MainThread
                    @Override
                    public void accept(Boolean data, Throwable throwable) throws Exception {
                        if (throwable == null) {
                            // do nothing
                        } else {
                            if(throwable.getClass().getSimpleName().equals("EOFException")) {
                                StudentStatementFragment.errorMessage = null;
                                StudentStatementFragment.gotRequested = Boolean.TRUE;
                                fragment.onAddStudentToStatement();
                            } else {
                                StudentStatementFragment.errorMessage = throwable.getMessage();
                                fragment.onAddStudentToStatement();
                            }
                        }
                    }
                }));
    }

    @MainThread
    public void deleteStudentFromStatement(StudentStatementsDto dto, StudentStatementFragment fragment) {
        StudentStatementFragment.errorMessage = null;
        StudentStatementFragment.gotRequested = null;
        disposable.add(apiClient.getMainApiRepository().DeleteStudentFromStatement(dto)
                .subscribeOn(Schedulers.newThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new BiConsumer<Boolean, Throwable>() {
                    @MainThread
                    @Override
                    public void accept(Boolean data, Throwable throwable) throws Exception {
                        if (throwable == null) {
                            // do nothing
                        } else {
                            if(throwable.getClass().getSimpleName().equals("EOFException")) {
                                StudentStatementFragment.errorMessage = null;
                                StudentStatementFragment.gotRequested = Boolean.TRUE;
                                fragment.onDeleteStudentFromStatement();
                            } else {
                                StudentStatementFragment.errorMessage = throwable.getMessage();
                                fragment.onDeleteStudentFromStatement();
                            }
                        }
                    }
                }));
    }

    //----------------------------STATEMENT------------------------------------------

    //-----------------------------TESTING------------------------------------------

    @MainThread
    public void getTestings(int teacherId, TestingsFragment testingsFragment) {
        TestingsFragment.errorMessage = null;
        TestingsFragment.testings = null;
        disposable.add(apiClient.getMainApiRepository().GetTestings(teacherId)
                .subscribeOn(Schedulers.newThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new BiConsumer<List<TestingDto>, Throwable>() {
                    @MainThread
                    @Override
                    public void accept(List<TestingDto> data, Throwable throwable) throws Exception {
                        if (throwable != null) {
                            TestingsFragment.testings = null;
                            TestingsFragment.errorMessage = throwable.getMessage();
                            testingsFragment.onGetData();
                        } else {
                            TestingsFragment.errorMessage = null;
                            TestingsFragment.testings = data;
                            testingsFragment.onGetData();
                        }
                    }
                }));
    }

    @MainThread
    public void createOrUpdateTesting(TestingDto testingDto, TestingFragment testingFragment) {
        TestingFragment.errorMessage = null;
        TestingFragment.gotRequested = null;
        disposable.add(apiClient.getMainApiRepository().CreateOrUpdateTesting(testingDto)
                .subscribeOn(Schedulers.newThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new BiConsumer<Boolean, Throwable>() {
                    @MainThread
                    @Override
                    public void accept(Boolean data, Throwable throwable) throws Exception {
                        if (throwable == null) {
                            // do nothing
                        } else {
                            if (throwable.getClass().getSimpleName().equals("EOFException")) {
                                TestingFragment.errorMessage = null;
                                TestingFragment.gotRequested = Boolean.TRUE;
                                testingFragment.onCreateOrUpdate();
                            } else {
                                TestingFragment.errorMessage = throwable.getMessage();
                                testingFragment.onCreateOrUpdate();
                            }
                        }
                    }
                }));
    }

    @MainThread
    public void deleteTesting(TestingDto testingDto, TestingsFragment fragment) {
        TestingsFragment.errorMessage = null;
        TestingsFragment.gotDeleted = null;
        TestingsFragment.testings = null;
        disposable.add(apiClient.getMainApiRepository().DeleteTesting(testingDto)
                .subscribeOn(Schedulers.newThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new BiConsumer<Boolean, Throwable>() {
                    @MainThread
                    @Override
                    public void accept(Boolean data, Throwable throwable) throws Exception {
                        if (throwable == null) {
                            // do nothing
                        } else {
                            if(throwable.getClass().getSimpleName().equals("EOFException")) {
                                TestingsFragment.errorMessage = null;
                                TestingsFragment.gotDeleted = Boolean.TRUE;
                                fragment.onDeleteTesting();
                            } else {
                                TestingsFragment.errorMessage = throwable.getMessage();
                                fragment.onDeleteTesting();
                            }
                        }
                    }
                }));
    }

    public void getStudentsForTesting(int teacherId, StudentTestingFragment fragment) {
        StudentTestingFragment.errorMessage = null;
        StudentTestingFragment.students = null;
        disposable.add(apiClient.getMainApiRepository().GetStudents(teacherId)
                .subscribeOn(Schedulers.newThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new BiConsumer<List<StudentDto>, Throwable>() {
                    @Override
                    public void accept(List<StudentDto> data, Throwable throwable) throws Exception {
                        if (throwable != null) {
                            StudentTestingFragment.students = null;
                            StudentTestingFragment.errorMessage = throwable.getMessage();
                            fragment.onGetData();
                        } else {
                            StudentTestingFragment.errorMessage = null;
                            StudentTestingFragment.students = data;
                            fragment.onGetData();
                        }
                    }
                }));
    }

    @MainThread
    public void addStudentToTesting(StudentTestingDto dto, StudentTestingFragment fragment) {
        StudentTestingFragment.errorMessage = null;
        StudentTestingFragment.gotRequested = null;
        disposable.add(apiClient.getMainApiRepository().AddStudentToTesting(dto)
                .subscribeOn(Schedulers.newThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new BiConsumer<Boolean, Throwable>() {
                    @MainThread
                    @Override
                    public void accept(Boolean data, Throwable throwable) throws Exception {
                        if (throwable == null) {
                            // do nothing
                        } else {
                            if(throwable.getClass().getSimpleName().equals("EOFException")) {
                                StudentTestingFragment.errorMessage = null;
                                StudentTestingFragment.gotRequested = Boolean.TRUE;
                                fragment.onAddStudentToTesting();
                            } else {
                                StudentTestingFragment.errorMessage = throwable.getMessage();
                                fragment.onAddStudentToTesting();
                            }
                        }
                    }
                }));
    }

    @MainThread
    public void deleteStudentFromTesting(StudentTestingDto dto, StudentTestingFragment fragment) {
        StudentTestingFragment.errorMessage = null;
        StudentTestingFragment.gotRequested = null;
        disposable.add(apiClient.getMainApiRepository().DeleteStudentFromTesting(dto)
                .subscribeOn(Schedulers.newThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new BiConsumer<Boolean, Throwable>() {
                    @MainThread
                    @Override
                    public void accept(Boolean data, Throwable throwable) throws Exception {
                        if (throwable == null) {
                            // do nothing
                        } else {
                            if(throwable.getClass().getSimpleName().equals("EOFException")) {
                                StudentTestingFragment.errorMessage = null;
                                StudentTestingFragment.gotRequested = Boolean.TRUE;
                                fragment.onDeleteStudentFromTesting();
                            } else {
                                StudentTestingFragment.errorMessage = throwable.getMessage();
                                fragment.onDeleteStudentFromTesting();
                            }
                        }
                    }
                }));
    }

    //-----------------------------TESTING------------------------------------------

    //------------------------------LESSON------------------------------------------

    public void getAllLessons(TestingFragment testingFragment) {
        TestingFragment.errorMessage = null;
        TestingFragment.lessons = null;
        disposable.add(apiClient.getMainApiRepository().GetAllLessons()
                .subscribeOn(Schedulers.newThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new BiConsumer<List<LessonDto>, Throwable>() {
                    @Override
                    public void accept(List<LessonDto> data, Throwable throwable) throws Exception {
                        if (throwable != null) {
                            TestingFragment.lessons = null;
                            TestingFragment.errorMessage = throwable.getMessage();
                            testingFragment.onGetData();
                        } else {
                            TestingFragment.errorMessage = null;
                            TestingFragment.lessons = data;
                            testingFragment.onGetData();
                        }
                    }
                }));
    }

    //------------------------------LESSON------------------------------------------

    //-------------------------------MAIL------------------------------------------

    @MainThread
    public void sendMail(SendMailDto sendMailDto, ReportStudentTestingFragment fragment) {
        ReportStudentTestingFragment.isSent = null;
        ReportStudentTestingFragment.errorMessage = null;
        disposable.add(apiClient.getMainApiRepository().SendMessage(sendMailDto)
                .subscribeOn(Schedulers.newThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new BiConsumer<Boolean, Throwable>() {
                    @Override
                    public void accept(Boolean data, Throwable throwable) throws Exception {
                        if (throwable == null) {
                            // do nothing
                        } else {
                            if (throwable.getClass().getSimpleName().equals("EOFException")) {
                                ReportStudentTestingFragment.errorMessage = null;
                                ReportStudentTestingFragment.isSent = Boolean.TRUE;
                                fragment.onMessageSent();
                            } else {
                                ReportStudentTestingFragment.errorMessage = throwable.getMessage();
                                fragment.onMessageSent();
                            }
                        }
                    }
                }));
    }

    //-------------------------------MAIL------------------------------------------

    //------------------------------REPORT-----------------------------------------

    public void getStudentsForDisciplines(int teacherId, ReportStudentDisciplineFragment fragment) {
        ReportStudentDisciplineFragment.errorMessage = null;
        ReportStudentDisciplineFragment.students = null;
        disposable.add(apiClient.getMainApiRepository().GetStudents(teacherId)
                .subscribeOn(Schedulers.newThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new BiConsumer<List<StudentDto>, Throwable>() {
                    @Override
                    public void accept(List<StudentDto> data, Throwable throwable) throws Exception {
                        if (throwable != null) {
                            ReportStudentDisciplineFragment.students = null;
                            ReportStudentDisciplineFragment.errorMessage = throwable.getMessage();
                            fragment.onGetData();
                        } else {
                            ReportStudentDisciplineFragment.errorMessage = null;
                            ReportStudentDisciplineFragment.students = data;
                            fragment.onGetData();
                        }
                    }
                }));
    }

    public void getStudentDisciplinesExcel(String params, ReportStudentDisciplineFragment fragment) {
        ReportStudentDisciplineFragment.errorMessage = null;
        ReportStudentDisciplineFragment.book = null;
        disposable.add(apiClient.getMainApiRepository().GetStudentDisciplines(params)
                .subscribeOn(Schedulers.newThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new BiConsumer<List<ReportStudentDisciplinesDto>, Throwable>() {
                    @Override
                    public void accept(List<ReportStudentDisciplinesDto> data, Throwable throwable) throws Exception {
                        if (throwable != null) {
                            ReportStudentDisciplineFragment.book = null;
                            ReportStudentDisciplineFragment.errorMessage = throwable.getMessage();
                            //something wrong
                        } else {
                            try {
                                ExcelFileHelper ex = new ExcelFileHelper(data);
                                ReportStudentDisciplineFragment.errorMessage = null;
                                ex.saveExcelFile();
                                fragment.onExcelCreated();
                            } catch (Exception e) {
                                ReportStudentDisciplineFragment.errorMessage = e.getMessage();
                                ReportStudentDisciplineFragment.book = null;
                                fragment.onExcelCreated();
                            }
                        }
                    }
                }));
    }

    public void getStudentDisciplinesWord(String params, ReportStudentDisciplineFragment fragment) {
        ReportStudentDisciplineFragment.errorMessage = null;
        ReportStudentDisciplineFragment.doc = null;
        disposable.add(apiClient.getMainApiRepository().GetStudentDisciplines(params)
                .subscribeOn(Schedulers.newThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new BiConsumer<List<ReportStudentDisciplinesDto>, Throwable>() {
                    @Override
                    public void accept(List<ReportStudentDisciplinesDto> data, Throwable throwable) throws Exception {
                        if (throwable != null) {
                            ReportStudentDisciplineFragment.doc = null;
                            ReportStudentDisciplineFragment.errorMessage = throwable.getMessage();
                            //something wrong
                        } else {
                            try {
                                WordFileHelper dc = new WordFileHelper(data);
                                ReportStudentDisciplineFragment.errorMessage = null;
                                dc.saveWordFile();
                                fragment.onWordCreated();
                            } catch (Exception e) {
                                ReportStudentDisciplineFragment.errorMessage = e.getMessage();
                                ReportStudentDisciplineFragment.doc = null;
                                fragment.onWordCreated();
                            }
                        }
                    }
                }));
    }

    //------------------------------REPORT-----------------------------------------

    //------------------------------ADMIN------------------------------------------

    @MainThread
    public void getAllTeachers(AdminActivity activity) {
        AdminActivity.errorMessage = null;
        AdminActivity.teachers = null;
        disposable.add(apiClient.getMainApiRepository().GetAllTeachers()
                .subscribeOn(Schedulers.newThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new BiConsumer<List<TeacherDto>, Throwable>() {
                    @MainThread
                    @Override
                    public void accept(List<TeacherDto> data, Throwable throwable) throws Exception {
                        if (throwable != null) {
                            AdminActivity.teachers = null;
                            AdminActivity.errorMessage = throwable.getMessage();
                            activity.onGetData();
                        } else {
                            AdminActivity.errorMessage = null;
                            AdminActivity.teachers = data;
                            activity.onGetData();
                        }
                    }
                }));
    }
    //------------------------------ADMIN------------------------------------------

    //------------------------------TEACHER------------------------------------------

    @MainThread
    public void logInTeacher(String login, String password, SignInActivity activity) {
        SignInActivity.errorMessage = null;
        SignInActivity.teacher = null;
        disposable.add(apiClient.getMainApiRepository().LogInTeacher(login, password)
                .subscribeOn(Schedulers.newThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new BiConsumer<TeacherDto, Throwable>() {
                    @Override
                    @MainThread
                    public void accept(TeacherDto data, Throwable throwable) throws Exception {
                        if (throwable != null) {
                            SignInActivity.errorMessage = throwable.getMessage();
                            SignInActivity.teacher = null;
                            activity.onSignIn();
                        } else {
                            SignInActivity.errorMessage = null;
                            SignInActivity.teacher = data;
                            activity.onSignIn();
                        }
                    }
                }));
    }

    public void registerTeacher(TeacherDto teacherDto, RegistrationActivity activity) {
        RegistrationActivity.gotRequested = null;
        RegistrationActivity.errorMessage = null;
        disposable.add(apiClient.getMainApiRepository().RegisterTeacher(teacherDto)
                .subscribeOn(Schedulers.newThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(new BiConsumer<Boolean, Throwable>() {
                    @Override
                    public void accept(Boolean data, Throwable throwable) throws Exception {
                        if (throwable == null) {
                            // do nothing
                        } else {
                            if (throwable.getClass().getSimpleName().equals("EOFException")) {
                                RegistrationActivity.errorMessage = null;
                                RegistrationActivity.gotRequested = Boolean.TRUE;
                                activity.onRegister();
                            } else {
                                RegistrationActivity.errorMessage = throwable.getMessage();
                                activity.onRegister();
                            }
                        }
                    }
                }));
    }

    //------------------------------TEACHER------------------------------------------
}
