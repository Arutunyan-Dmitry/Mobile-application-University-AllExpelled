package ru.universityallexplled.teacherapplication.Client.DtoModels;

import androidx.annotation.NonNull;

import java.io.Serializable;

import ru.universityallexplled.teacherapplication.Client.Enums.Marks;

public class StudentTesting implements Serializable {
    public String numbGb;
    public String flm;
    public Marks mark;

    public StudentTesting() {}

    public StudentTesting(String numbGb, String flm, Marks mark) {
        this.numbGb = numbGb;
        this.flm = flm;
        this.mark = mark;
    }

    public String getNumbGb() {return numbGb;}
    public void setNumbGb(String numbGb) {this.numbGb = numbGb;}

    public String getFlm() {return flm;}
    public void setFlm(String flm) {this.flm = flm;}

    public Marks getMark() {return mark;}
    public void setMark(Marks mark) {this.mark = mark;}

    @NonNull
    @Override
    public String toString() {
        return numbGb + ", " + flm + ", " + mark;
    }
}
