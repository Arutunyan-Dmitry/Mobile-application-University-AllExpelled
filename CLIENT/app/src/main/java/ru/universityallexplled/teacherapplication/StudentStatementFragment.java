package ru.universityallexplled.teacherapplication;

import android.app.Activity;
import android.os.Bundle;

import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.Spinner;
import android.widget.Toast;

import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;

import java.util.List;

import ru.universityallexplled.teacherapplication.Client.Controller.MainController;
import ru.universityallexplled.teacherapplication.Client.DtoModels.StatementDto;
import ru.universityallexplled.teacherapplication.Client.DtoModels.StudentDto;
import ru.universityallexplled.teacherapplication.Client.DtoModels.StudentStatementsDto;
import ru.universityallexplled.teacherapplication.Client.Enums.Achievements;

public class StudentStatementFragment extends Fragment {

    MainController mc = new MainController();
    public static List<StudentDto> students;
    public static String errorMessage;
    public static Boolean gotRequested;
    public static Boolean isConnection;

    public StudentStatementFragment() {}

    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);
        if (students == null && errorMessage == null) {
            mc.getStudentsForStatements(MainActivity.teacher.id, this);
        }
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {super.onCreate(savedInstanceState);}

    @Override
    public View onCreateView(@NotNull LayoutInflater inflater, @Nullable ViewGroup container,
                             @Nullable Bundle savedInstanceState) {

        Bundle arguments = getArguments();
        View view = inflater.inflate(R.layout.fragment_student_statement, container, false);

        Button buttonSave = view.findViewById(R.id.button_save);
        Button buttonCancel = view.findViewById(R.id.button_cancel);

        Spinner student = view.findViewById(R.id.student_choice);
        Spinner statement = view.findViewById(R.id.statement_choice);
        Spinner achievement = view.findViewById(R.id.achievement_choice);

        ArrayAdapter<StatementDto> statementsAA = new ArrayAdapter<>(getActivity(), android.R.layout.simple_spinner_item, StatementsFragment.statements);
        statementsAA.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        statement.setAdapter(statementsAA);

        if (arguments != null) {
            isConnection = arguments.getBoolean("isConnection");
        }

        if (isConnection != null) {
            if (errorMessage == null && students != null) {
                if(isConnection) {
                    ArrayAdapter<Achievements> achievementsAA = new ArrayAdapter<>(getActivity(), android.R.layout.simple_spinner_item, Achievements.values());
                    achievementsAA.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                    achievement.setAdapter(achievementsAA);
                } else {
                    achievement.setVisibility(View.INVISIBLE);
                }

                ArrayAdapter<StudentDto> studentsAA = new ArrayAdapter<>(getActivity(), android.R.layout.simple_spinner_item, students);
                studentsAA.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                student.setAdapter(studentsAA);

                buttonSave.setOnClickListener(v -> {
                    if(isConnection) {
                        StudentStatementsDto std = new StudentStatementsDto(
                                ((StatementDto)statement.getSelectedItem()).getId(),
                                ((StudentDto)student.getSelectedItem()).getId(),
                                (Achievements)achievement.getSelectedItem()
                        );
                        mc.addStudentToStatement(std,StudentStatementFragment.this);
                    } else {
                        StudentStatementsDto std = new StudentStatementsDto(
                                ((StatementDto)statement.getSelectedItem()).getId(),
                                ((StudentDto)student.getSelectedItem()).getId(),
                                null);
                        mc.deleteStudentFromStatement(std,StudentStatementFragment.this);
                    }
                });

                buttonCancel.setOnClickListener(v -> {
                    StatementsFragment.statements = null;
                    StatementsFragment.errorMessage = null;
                    FragmentManager fragmentManager = getFragmentManager();
                    FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
                    StatementsFragment statementsFragment = new StatementsFragment();
                    fragmentTransaction.setCustomAnimations(R.anim.enter_from_left, R.anim.exit_to_right, R.anim.enter_from_right, R.anim.exit_to_left);
                    fragmentTransaction.replace(R.id.fragment_container, statementsFragment);
                    fragmentTransaction.commit();
                });
            }
        } else if (errorMessage != null) {
            Toast.makeText(getActivity(), errorMessage, Toast.LENGTH_LONG).show();
        }
        return view;
    }

    public void onGetData() {
        FragmentManager fragmentManager = getFragmentManager();
        FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
        fragmentTransaction.replace(R.id.fragment_container, new StudentStatementFragment());
        fragmentTransaction.commit();
    }

    public void onAddStudentToStatement() {
        if (errorMessage == null && gotRequested) {
            Toast.makeText(getActivity(), "Студент привязан", Toast.LENGTH_SHORT).show();
            onGetData();
        } else if (errorMessage != null) {
            Toast.makeText(getActivity(), errorMessage, Toast.LENGTH_LONG).show();
        }
    }

    public void onDeleteStudentFromStatement() {
        if (errorMessage == null && gotRequested) {
            Toast.makeText(getActivity(), "Студент отвязан", Toast.LENGTH_SHORT).show();
            onGetData();
        } else if (errorMessage != null) {
            Toast.makeText(getActivity(), errorMessage, Toast.LENGTH_LONG).show();
        }
    }
}