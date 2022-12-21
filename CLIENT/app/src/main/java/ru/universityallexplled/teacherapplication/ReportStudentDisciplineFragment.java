package ru.universityallexplled.teacherapplication;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;

import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import android.util.SparseBooleanArray;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;
import android.widget.Toast;

import org.apache.poi.ss.usermodel.Workbook;
import org.apache.poi.xwpf.usermodel.XWPFDocument;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;

import java.util.ArrayList;
import java.util.List;

import ru.universityallexplled.teacherapplication.Client.Controller.MainController;
import ru.universityallexplled.teacherapplication.Client.DtoModels.StudentDto;

public class ReportStudentDisciplineFragment extends Fragment {

    MainController mc = new MainController();
    public static List<StudentDto> students;
    public static String errorMessage;
    public static Workbook book;
    public static XWPFDocument doc;

    public ReportStudentDisciplineFragment() {}

    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);
        if (students == null && errorMessage == null) {
            mc.getStudentsForDisciplines(MainActivity.teacher.id, this);
        }
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {super.onCreate(savedInstanceState);}

    @Override
    public View onCreateView(@NotNull LayoutInflater inflater, @Nullable ViewGroup container,
                             @Nullable Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_report_student_discipline, container, false);

        Button buttonWord = view.findViewById(R.id.button_to_word);
        Button buttonExcel = view.findViewById(R.id.button_to_excel);

        ListView viewStudentsChoice = view.findViewById(R.id.view_students_choice);
        ArrayAdapter<StudentDto> StudentsAA;

        if (errorMessage == null && students != null) {
            StudentsAA = new ArrayAdapter<>(getActivity(), android.R.layout.simple_list_item_multiple_choice, students);
            viewStudentsChoice.setAdapter(StudentsAA);
            viewStudentsChoice.setChoiceMode(ListView.CHOICE_MODE_MULTIPLE);

            buttonWord.setOnClickListener(v -> {

                if (viewStudentsChoice.getCheckedItemPositions() != null) {
                    SparseBooleanArray checked = viewStudentsChoice.getCheckedItemPositions();
                    List<StudentDto> tmp = new ArrayList<>();
                    for (int i = 0; i < checked.size(); i++) {
                        if (checked.valueAt(i)) {
                            tmp.add(students.get(checked.keyAt(i)));
                        }
                    }
                    if (tmp.size() != 0) {
                        String params = "";
                        for (StudentDto student : tmp) {
                            params += student.getId() + " ";
                        }
                        params += MainActivity.teacher.id;
                        mc.getStudentDisciplinesWord(params, ReportStudentDisciplineFragment.this);
                    } else {
                        Toast.makeText(getActivity(), "Студенты не выбраны", Toast.LENGTH_SHORT).show();
                    }
                } else {
                    Toast.makeText(getActivity(), "Студенты не выбраны", Toast.LENGTH_SHORT).show();
                }
            });

            buttonExcel.setOnClickListener(v -> {

                if (viewStudentsChoice.getCheckedItemPositions() != null) {
                    SparseBooleanArray checked = viewStudentsChoice.getCheckedItemPositions();
                    List<StudentDto> tmp = new ArrayList<>();
                    for (int i = 0; i < checked.size(); i++) {
                        if (checked.valueAt(i)) {
                            tmp.add(students.get(checked.keyAt(i)));
                        }
                    }
                    if (tmp.size() != 0) {
                        String params = "";
                        for (StudentDto student : tmp) {
                            params += student.getId() + " ";
                        }
                        params += MainActivity.teacher.id;
                        mc.getStudentDisciplinesExcel(params, ReportStudentDisciplineFragment.this);
                    } else {
                        Toast.makeText(getActivity(), "Студенты не выбраны", Toast.LENGTH_SHORT).show();
                    }
                } else {
                    Toast.makeText(getActivity(), "Студенты не выбраны", Toast.LENGTH_SHORT).show();
                }
            });
        } else if(errorMessage != null) {
            Toast.makeText(getActivity(), errorMessage, Toast.LENGTH_LONG).show();
        }
        return view;
    }

    public void onGetData() {
        FragmentManager fragmentManager = getFragmentManager();
        FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
        fragmentTransaction.replace(R.id.list_container, new ReportStudentDisciplineFragment());
        fragmentTransaction.commit();
    }

    public void onExcelCreated() {
        if (errorMessage == null && book != null) {
            FragmentManager fragmentManager = getFragmentManager();
            FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
            fragmentTransaction.setCustomAnimations(R.anim.enter_from_right, R.anim.exit_to_left, R.anim.enter_from_left, R.anim.exit_to_right);
            fragmentTransaction.remove(ReportStudentDisciplineFragment.this);
            fragmentTransaction.commit();

            ExcelSaveDialogActivity.book = book;
            Intent intent = new Intent(getActivity(), ExcelSaveDialogActivity.class);
            startActivity(intent);
        } else if(errorMessage != null) {
            Toast.makeText(getActivity(), errorMessage, Toast.LENGTH_LONG).show();
        }
    }

    public void onWordCreated() {
        if (errorMessage == null && doc != null) {
            FragmentManager fragmentManager = getFragmentManager();
            FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
            fragmentTransaction.setCustomAnimations(R.anim.enter_from_right, R.anim.exit_to_left, R.anim.enter_from_left, R.anim.exit_to_right);
            fragmentTransaction.remove(ReportStudentDisciplineFragment.this);
            fragmentTransaction.commit();

            WordSaveDialogActivity.doc = doc;
            Intent intent = new Intent(getActivity(), WordSaveDialogActivity.class);
            startActivity(intent);
        } else if(errorMessage != null) {
            Toast.makeText(getActivity(), errorMessage, Toast.LENGTH_LONG).show();
        }
    }
}