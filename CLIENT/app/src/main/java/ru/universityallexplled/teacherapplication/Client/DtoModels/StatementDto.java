package ru.universityallexplled.teacherapplication.Client.DtoModels;

import androidx.annotation.Nullable;

import java.io.Serializable;
import java.util.Map;

public class StatementDto implements Serializable {
    @Nullable
    private Integer id;
    @Nullable
    private Integer teacherId;
    private String naming;
    private String dateCreate;
    private Map<Integer,StudentStatements> studentStatements;

    public StatementDto() {}

    public StatementDto(@Nullable Integer id, @Nullable Integer teacherId, String naming,
                        String dateCreate, Map<Integer, StudentStatements> studentStatements) {
        this.id = id;
        this.teacherId = teacherId;
        this.naming = naming;
        this.dateCreate = dateCreate;
        this.studentStatements = studentStatements;
    }

    @Nullable
    public Integer getId() {return id;}
    public void setId(@Nullable Integer id) {this.id = id;}

    @Nullable
    public Integer getTeacherId() {return teacherId;}
    public void setTeacherId(@Nullable Integer teacherId) {this.teacherId = teacherId;}

    public String getNaming() {return naming;}
    public void setNaming(String naming) {this.naming = naming;}

    public String getDateCreate() {return dateCreate;}
    public void setDateCreate(String dateCreate) {this.dateCreate = dateCreate;}

    public Map<Integer, StudentStatements> getStudentStatements() {return studentStatements;}
    public void setStudentStatements(Map<Integer, StudentStatements> studentStatements) {this.studentStatements = studentStatements;}

    @Override
    public String toString() {
        return dateCreate.toString() + " " + naming;
    }
}
