package ru.universityallexplled.teacherapplication.Client.DtoModels;

import java.io.Serializable;

import ru.universityallexplled.teacherapplication.Client.Enums.Achievements;

public class StudentStatementsDto implements Serializable {
    public Integer statementId;
    public Integer studentId;
    public Achievements achievement;

    public StudentStatementsDto() {}

    public StudentStatementsDto(Integer statementId, Integer studentId, Achievements achievement) {
        this.statementId = statementId;
        this.studentId = studentId;
        this.achievement = achievement;
    }

    public Integer getStatementId() {return statementId;}
    public void setStatementId(Integer statementId) {this.statementId = statementId;}

    public Integer getStudentId() {return studentId;}
    public void setStudentId(Integer studentId) {this.studentId = studentId;}

    public Achievements getAchievement() {return achievement;}
    public void setAchievement(Achievements achievement) {this.achievement = achievement;}
}
