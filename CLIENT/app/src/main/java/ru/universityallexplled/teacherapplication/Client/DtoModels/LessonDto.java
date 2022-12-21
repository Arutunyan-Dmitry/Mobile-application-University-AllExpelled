package ru.universityallexplled.teacherapplication.Client.DtoModels;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;

import java.io.Serializable;

public class LessonDto implements Serializable {
    @Nullable
    public Integer id;
    public String lessonName;
    public String lessonDate;

    public LessonDto() {}

    public LessonDto(@Nullable Integer id, String lessonName, String lessonDate) {
        this.id = id;
        this.lessonName = lessonName;
        this.lessonDate = lessonDate;
    }

    @Nullable
    public Integer getId() {return id;}
    public void setId(@Nullable Integer id) {this.id = id;}

    public String getLessonName() {return lessonName;}
    public void setLessonName(String lessonName) {this.lessonName = lessonName;}

    public String getLessonDate() {return lessonDate;}
    public void setLessonDate(String lessonDate) {this.lessonDate = lessonDate;}

    @NonNull
    @Override
    public String toString() {
        return lessonName;
    }
}
