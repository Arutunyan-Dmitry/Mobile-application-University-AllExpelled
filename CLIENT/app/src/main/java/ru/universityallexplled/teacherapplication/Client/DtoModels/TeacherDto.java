package ru.universityallexplled.teacherapplication.Client.DtoModels;

import androidx.annotation.Nullable;

import org.jetbrains.annotations.NotNull;

import java.io.Serializable;

public class TeacherDto implements Serializable {
    @Nullable
    public Integer id;
    public String name;
    public String surname;
    public String middleName;
    public String email;
    public String password;

    public TeacherDto() {}

    public TeacherDto(@Nullable Integer id, String name, String surname, String middleName, String email, String password) {
        this.id = id;
        this.name = name;
        this.surname = surname;
        this.middleName = middleName;
        this.email = email;
        this.password = password;
    }

    @Nullable
    public Integer getId() {return id;}
    public void setId(@Nullable Integer id) {this.id = id;}

    public String getName() {return name;}
    public void setName(String name) {this.name = name;}

    public String getSurname() {return surname;}
    public void setSurname(String surname) {this.surname = surname;}

    public String getMiddleName() {return middleName;}
    public void setMiddleName(String middleName) {this.middleName = middleName;}

    public String getEmail() {return email;}
    public void setEmail(String email) {this.email = email;}

    public String getPassword() {return password;}
    public void setPassword(String password) {this.password = password;}

    @NotNull
    @Override
    public String toString() {
        return surname + ", " + name + ", " + middleName + ", " + email + " / " + password;
    }
}
