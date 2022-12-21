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
import ru.universityallexplled.teacherapplication.Client.DtoModels.StudentDto;
import ru.universityallexplled.teacherapplication.Client.DtoModels.StudentTestingDto;
import ru.universityallexplled.teacherapplication.Client.DtoModels.TestingDto;
import ru.universityallexplled.teacherapplication.Client.Enums.Marks;

public class StudentTestingFragment extends Fragment {

    MainController mc = new MainController();
    public static List<StudentDto> students;
    public static String errorMessage;
    public static Boolean gotRequested;
    public static Boolean isConnection;

    public StudentTestingFragment() {}

    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);
        if (students == null && errorMessage == null) {
            mc.getStudentsForTesting(MainActivity.teacher.id, this);
        }
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {super.onCreate(savedInstanceState);}

    @Override
    public View onCreateView(@NotNull LayoutInflater inflater, @Nullable ViewGroup container,
                             @Nullable Bundle savedInstanceState) {

        Bundle arguments = getArguments();
        View view = inflater.inflate(R.layout.fragment_student_testing, container, false);

        Button buttonSave = view.findViewById(R.id.button_save);
        Button buttonCancel = view.findViewById(R.id.button_cancel);

        Spinner student = view.findViewById(R.id.student_choice);
        Spinner testing = view.findViewById(R.id.testing_choice);
        Spinner mark = view.findViewById(R.id.mark_choice);

        ArrayAdapter<TestingDto> testingsAA = new ArrayAdapter<>(getActivity(), android.R.layout.simple_spinner_item, TestingsFragment.testings);
        testingsAA.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        testing.setAdapter(testingsAA);

        if (arguments != null) {
            isConnection = arguments.getBoolean("isConnection");
        }

        if (isConnection != null) {
            if (errorMessage == null && students != null) {
                if (isConnection) {
                    ArrayAdapter<Marks> marksAA = new ArrayAdapter<>(getActivity(), android.R.layout.simple_spinner_item, Marks.values());
                    marksAA.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                    mark.setAdapter(marksAA);
                } else {
                    mark.setVisibility(View.INVISIBLE);
                }

                ArrayAdapter<StudentDto> studentsAA = new ArrayAdapter<>(getActivity(), android.R.layout.simple_spinner_item, students);
                studentsAA.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                student.setAdapter(studentsAA);

                buttonSave.setOnClickListener(v -> {
                    if (isConnection) {
                        StudentTestingDto std = new StudentTestingDto(
                                ((TestingDto) testing.getSelectedItem()).getId(),
                                ((StudentDto) student.getSelectedItem()).getId(),
                                (Marks) mark.getSelectedItem()
                        );
                        mc.addStudentToTesting(std, StudentTestingFragment.this);
                    } else {
                        StudentTestingDto std = new StudentTestingDto(
                                ((TestingDto) testing.getSelectedItem()).getId(),
                                ((StudentDto) student.getSelectedItem()).getId(),
                                null);
                        mc.deleteStudentFromTesting(std, StudentTestingFragment.this);
                    }
                });

                buttonCancel.setOnClickListener(v -> {
                    TestingsFragment.testings = null;
                    TestingsFragment.errorMessage = null;
                    FragmentManager fragmentManager = getFragmentManager();
                    FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
                    TestingsFragment testingsFragment = new TestingsFragment();
                    fragmentTransaction.setCustomAnimations(R.anim.enter_from_left, R.anim.exit_to_right, R.anim.enter_from_right, R.anim.exit_to_left);
                    fragmentTransaction.replace(R.id.fragment_container, testingsFragment);
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
        fragmentTransaction.replace(R.id.fragment_container, new StudentTestingFragment());
        fragmentTransaction.commit();
    }

    public void onAddStudentToTesting() {
        if (errorMessage == null && gotRequested) {
            Toast.makeText(getActivity(), "Студент привязан", Toast.LENGTH_SHORT).show();
            onGetData();
        } else if (errorMessage != null) {
            Toast.makeText(getActivity(), errorMessage, Toast.LENGTH_LONG).show();
        }
    }

    public void onDeleteStudentFromTesting() {
        if (errorMessage == null && gotRequested) {
            Toast.makeText(getActivity(), "Студент отвязан", Toast.LENGTH_SHORT).show();
            onGetData();
        } else if (errorMessage != null) {
            Toast.makeText(getActivity(), errorMessage, Toast.LENGTH_LONG).show();
        }
    }
}