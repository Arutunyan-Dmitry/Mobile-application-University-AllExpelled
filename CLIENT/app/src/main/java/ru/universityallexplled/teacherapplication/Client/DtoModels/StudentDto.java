package ru.universityallexplled.teacherapplication.Client.DtoModels;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;

import java.io.Serializable;

public class StudentDto implements Serializable {
    @Nullable
    private Integer id;
    @Nullable
    private Integer teacherId;
    private String numbGB;
    private String name;
    private String surname;
    private String middleName;

    public StudentDto() {

    }

    public StudentDto(@Nullable Integer id, @Nullable Integer teacher_id, String num_gb, String name, String surname, String middle_name) {
        this.id = id;
        this.teacherId = teacher_id;
        this.numbGB = num_gb;
        this.name = name;
        this.surname = surname;
        this.middleName = middle_name;
    }

    public Integer getId() {return id;}
    public void setId(Integer id) {this.id = id;}

    public Integer getTeacherId() {return teacherId;}
    public void setTeacherId(Integer teacher_id) {this.teacherId = teacher_id;}

    public String getNum_gb() {return numbGB;}
    public void setNum_gb(String num_gb) {this.numbGB = num_gb;}

    public String getName() {return name;}
    public void setName(String name) {this.name = name;}

    public String getSurname() {return surname;}
    public void setSurname(String surname) {this.surname = surname;}

    public String getMiddle_name() {return middleName;}
    public void setMiddle_name(String middle_name) {this.middleName = middle_name;}

    @NonNull
    @Override
    public String toString() {
        return numbGB + " " + surname + " " + name.charAt(0) + ". " + middleName.charAt(0) + ".";
    }
}
