package ru.universityallexplled.teacherapplication.Helper;

import org.apache.poi.hssf.usermodel.HSSFWorkbook;
import org.apache.poi.ss.usermodel.Cell;
import org.apache.poi.ss.usermodel.Font;
import org.apache.poi.ss.usermodel.IndexedColors;
import org.apache.poi.ss.usermodel.Row;
import org.apache.poi.ss.usermodel.Sheet;
import org.apache.poi.ss.usermodel.Workbook;

import java.io.IOException;
import java.util.List;

import ru.universityallexplled.teacherapplication.Client.DtoModels.ReportStudentDisciplinesDto;
import ru.universityallexplled.teacherapplication.ReportStudentDisciplineFragment;

public class ExcelFileHelper {
    List<ReportStudentDisciplinesDto> studentDisciplines;

    public ExcelFileHelper(List<ReportStudentDisciplinesDto> studentDisciplines){
        this.studentDisciplines = studentDisciplines;
    }

    public void saveExcelFile() throws IOException {

        Workbook book = new HSSFWorkbook();
        Sheet sheet = book.createSheet("Список студентов по дисциплинам");

        Font fontBold = book.createFont();
        fontBold.setFontHeightInPoints((short) 12);
        fontBold.setFontName("Arial");
        fontBold.setColor(IndexedColors.BLACK.getIndex());
        fontBold.setBold(true);
        fontBold.setItalic(false);

        Font fontNormal = book.createFont();
        fontNormal.setFontHeightInPoints((short) 12);
        fontNormal.setFontName("Arial");
        fontNormal.setColor(IndexedColors.BLACK.getIndex());
        fontNormal.setBold(false);
        fontNormal.setItalic(false);

        Row rowTitle = sheet.createRow(0);
        Cell title = rowTitle.createCell(0);
        title.setCellValue("Студенты по дисциплинам");

        int rowIndex = 1;
        for (ReportStudentDisciplinesDto student : studentDisciplines) {
            Row rowStudent = sheet.createRow(rowIndex);
            Cell cellNumGB = rowStudent.createCell(0);
            Cell cellFlm = rowStudent.createCell(1);
            cellNumGB.setCellValue(student.numbGB);
            cellFlm.setCellValue(student.flm);
            rowIndex++;

            for (String naming : student.disciplineNames) {
                Row rowNaming = sheet.createRow(rowIndex);
                Cell cellNaming = rowNaming.createCell(2);
                cellNaming.setCellValue(naming);
                rowIndex++;
            }
        }
        ReportStudentDisciplineFragment.book = book;
        book.close();
    }
}
