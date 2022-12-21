package ru.universityallexplled.teacherapplication.Client.DtoModels;

import java.io.Serializable;
import java.util.List;

public class ReportStudentDisciplinesDto implements Serializable {
    public String numbGB;
    public String flm;
    public List<String> disciplineNames;

    public ReportStudentDisciplinesDto() {}

    public ReportStudentDisciplinesDto(String numbGB, String flm, List<String> disciplineNames) {
        this.numbGB = numbGB;
        this.flm = flm;
        this.disciplineNames = disciplineNames;
    }

    public String getNumbGB() {return numbGB;}
    public void setNumbGB(String numbGB) {this.numbGB = numbGB;}

    public String getFlm() {return flm;}
    public void setFlm(String flm) {this.flm = flm;}

    public List<String> getDisciplineNames() {return disciplineNames;}
    public void setDisciplineNames(List<String> disciplineNames) {this.disciplineNames = disciplineNames;}
}
