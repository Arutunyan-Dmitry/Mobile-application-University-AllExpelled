package ru.universityallexplled.teacherapplication.Helper;

import org.apache.poi.xwpf.usermodel.ParagraphAlignment;
import org.apache.poi.xwpf.usermodel.XWPFDocument;
import org.apache.poi.xwpf.usermodel.XWPFParagraph;
import org.apache.poi.xwpf.usermodel.XWPFRun;
import org.apache.poi.xwpf.usermodel.XWPFTable;
import org.apache.poi.xwpf.usermodel.XWPFTableRow;

import java.util.List;

import ru.universityallexplled.teacherapplication.Client.DtoModels.ReportStudentDisciplinesDto;
import ru.universityallexplled.teacherapplication.ReportStudentDisciplineFragment;

public class WordFileHelper {
    List<ReportStudentDisciplinesDto> studentDisciplines;

    public WordFileHelper(List<ReportStudentDisciplinesDto> studentDisciplines) {
        this.studentDisciplines = studentDisciplines;
    }

    public void saveWordFile() {
        XWPFDocument doc = new XWPFDocument();
        XWPFParagraph header = doc.createParagraph();
        header.setAlignment(ParagraphAlignment.CENTER);
        header.setSpacingAfter(500);

        XWPFRun headerRun = header.createRun();
        headerRun.setText("Студенты по дисциплинам");
        headerRun.setBold(true);
        headerRun.setFontSize(18);

        XWPFTable table = doc.createTable();
        XWPFTableRow headerRow = table.insertNewTableRow(0);
        XWPFRun run1 = headerRow.addNewTableCell().addParagraph().createRun();
        run1.setBold(true);
        run1.setText("Номер ЗК");

        XWPFRun run2 = headerRow.addNewTableCell().addParagraph().createRun();
        run2.setBold(true);
        run2.setText("Фамилия И. О.");

        XWPFRun run3 = headerRow.addNewTableCell().addParagraph().createRun();
        run3.setBold(true);
        run3.setText("Дисциплина");

        int rowIndex = 1;
        for (ReportStudentDisciplinesDto student : studentDisciplines) {
            XWPFTableRow row = table.insertNewTableRow(rowIndex);
            row.addNewTableCell().addParagraph().createRun().setText(student.numbGB);
            row.addNewTableCell().addParagraph().createRun().setText(student.flm);
            row.addNewTableCell().addParagraph().createRun().setText("");

            rowIndex++;

            for (String naming : student.disciplineNames) {

                XWPFTableRow partRow = table.insertNewTableRow(rowIndex);
                partRow.addNewTableCell().addParagraph().createRun().setText("");
                partRow.addNewTableCell().addParagraph().createRun().setText("");
                partRow.addNewTableCell().addParagraph().createRun().setText(naming);

                rowIndex++;
            }
        }
        ReportStudentDisciplineFragment.doc = doc;
    }
}
