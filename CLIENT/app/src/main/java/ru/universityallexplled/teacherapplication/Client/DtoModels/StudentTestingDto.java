package ru.universityallexplled.teacherapplication.Client.DtoModels;

import java.io.Serializable;

import ru.universityallexplled.teacherapplication.Client.Enums.Marks;

public class StudentTestingDto implements Serializable {
    public Integer testingId;
    public Integer studentId;
    public Marks mark;

    public StudentTestingDto() {}

    public StudentTestingDto(Integer testingId, Integer studentId, Marks mark) {
        this.testingId = testingId;
        this.studentId = studentId;
        this.mark = mark;
    }

    public Integer getTestingId() {return testingId;}
    public void setTestingId(Integer testingId) {this.testingId = testingId;}

    public Integer getStudentId() {return studentId;}
    public void setStudentId(Integer studentId) {this.studentId = studentId;}

    public Marks getMark() {return mark;}
    public void setMark(Marks mark) {this.mark = mark;}
}
