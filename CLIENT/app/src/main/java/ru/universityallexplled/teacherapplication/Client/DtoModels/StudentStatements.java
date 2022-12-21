package ru.universityallexplled.teacherapplication.Client.DtoModels;

import androidx.annotation.NonNull;

import java.io.Serializable;

import ru.universityallexplled.teacherapplication.Client.Enums.Achievements;

public class StudentStatements implements Serializable {
    public String numbGb;
    public String flm;
    public Achievements achievement;

    public StudentStatements() {}

    public StudentStatements(String numbGb, String flm, Achievements achievement) {
        this.numbGb = numbGb;
        this.flm = flm;
        this.achievement = achievement;
    }

    public String getNumbGb() {return numbGb;}
    public void setNumbGb(String numbGb) {this.numbGb = numbGb;}

    public String getFlm() {return flm;}
    public void setFlm(String flm) {this.flm = flm;}

    public Achievements getAchievement() {return achievement;}
    public void setAchievement(Achievements achievement) {this.achievement = achievement;}

    @NonNull
    @Override
    public String toString() {
        return numbGb + ", " + flm + ", " + achievement;
    }
}
