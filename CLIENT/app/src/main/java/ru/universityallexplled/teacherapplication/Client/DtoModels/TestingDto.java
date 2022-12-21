package ru.universityallexplled.teacherapplication.Client.DtoModels;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;

import java.io.Serializable;
import java.util.Map;

import ru.universityallexplled.teacherapplication.Client.Enums.TestingTypes;

public class TestingDto implements Serializable {
    @Nullable
    private Integer id;
    @Nullable
    private Integer teacherId;
    private Integer lessonId;
    private String naming;
    private String lessonName;
    private TestingTypes type;
    private Map<Integer, StudentTesting> studentTestings;

    public TestingDto() {}

    public TestingDto(@Nullable Integer id, @Nullable Integer teacherId, Integer lessonId,
                      String naming, String lessonName, TestingTypes type,
                      Map<Integer, StudentTesting> studentTestings) {
        this.id = id;
        this.teacherId = teacherId;
        this.lessonId = lessonId;
        this.naming = naming;
        this.lessonName = lessonName;
        this.type = type;
        this.studentTestings = studentTestings;
    }

    @Nullable
    public Integer getId() {return id;}
    public void setId(@Nullable Integer id) {this.id = id;}

    @Nullable
    public Integer getTeacherId() {return teacherId;}
    public void setTeacherId(@Nullable Integer teacherId) {this.teacherId = teacherId;}

    public Integer getLessonId() {return lessonId;}
    public void setLessonId(Integer lessonId) {this.lessonId = lessonId;}

    public String getNaming() {return naming;}
    public void setNaming(String naming) {this.naming = naming;}

    public String getLessonName() {return lessonName;}
    public void setLessonName(String lessonName) {this.lessonName = lessonName;}

    public TestingTypes getType() {return type;}
    public void setType(TestingTypes type) {this.type = type;}

    public Map<Integer, StudentTesting> getStudentTestings() {return studentTestings;}
    public void setStudentTestings(Map<Integer, StudentTesting> studentTestings) {this.studentTestings = studentTestings;}

    @NonNull
    @Override
    public String toString() {
        return naming + " " + type;
    }
}
