package ru.universityallexplled.teacherapplication;

import android.os.Bundle;

import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.Toast;

import com.google.android.material.textfield.TextInputLayout;

import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;

import java.util.regex.Matcher;
import java.util.regex.Pattern;

import ru.universityallexplled.teacherapplication.Client.Controller.MainController;
import ru.universityallexplled.teacherapplication.Client.DtoModels.StudentDto;

public class StudentFragment extends Fragment {

    MainController mc = new MainController();
    public static String errorMessage;
    public static Boolean gotRequested;
    public static final Pattern VALID_GRADE_BOOK_NUMBER_REGEX =
            Pattern.compile("^\\d{2}/\\d{3}$", Pattern.CASE_INSENSITIVE);

    public StudentFragment() {}

    @Override
    public void onCreate(Bundle savedInstanceState) {super.onCreate(savedInstanceState);}

    @Override
    public View onCreateView(@NotNull LayoutInflater inflater, @Nullable ViewGroup container,
                             @Nullable Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_student, container, false);

        Button buttonSave = view.findViewById(R.id.button_save);
        Button buttonCancel = view.findViewById(R.id.button_cancel);

        TextInputLayout studentGradeNumb = view.findViewById(R.id.student_grade_numb);
        TextInputLayout studentName = view.findViewById(R.id.student_name);
        TextInputLayout studentSurname = view.findViewById(R.id.student_surname);
        TextInputLayout studentMiddleName= view.findViewById(R.id.student_middle_name);

        Bundle arguments = getArguments();
        StudentDto student;
        if(arguments!=null) {
            student = (StudentDto) arguments.getSerializable(StudentDto.class.getSimpleName());
            if (student != null) {
                studentGradeNumb.getEditText().setText(student.getNum_gb());
                studentName.getEditText().setText(student.getName());
                studentSurname.getEditText().setText(student.getSurname());
                studentMiddleName.getEditText().setText(student.getMiddle_name());
            }
        }

        buttonSave.setOnClickListener(v -> {
            String numGB = studentGradeNumb.getEditText().getText().toString();
            String name = studentName.getEditText().getText().toString();
            String surname = studentSurname.getEditText().getText().toString();
            String middleName = studentMiddleName.getEditText().getText().toString();

            if (!numGB.equals("") && !name.equals("") && !surname.equals("") && !middleName.equals("")) {
                Matcher matcher = VALID_GRADE_BOOK_NUMBER_REGEX.matcher(numGB);
                if(matcher.find()) {

                    StudentDto st;
                    if (arguments != null) {
                        st = (StudentDto) arguments.getSerializable(StudentDto.class.getSimpleName());
                        if (st != null) {
                            st.setTeacherId(MainActivity.teacher.id);
                            st.setNum_gb(numGB);
                            st.setName(name);
                            st.setSurname(surname);
                            st.setMiddle_name(middleName);
                        } else {
                            st = new StudentDto(
                                    null,
                                    MainActivity.teacher.id,
                                    numGB,
                                    name,
                                    surname,
                                    middleName
                            );
                        }
                        mc.createOrUpdateStudent(st, StudentFragment.this);

                    } else {
                        Toast.makeText(getActivity(), "Некорректно заполнен номер ЗК", Toast.LENGTH_SHORT).show();
                    }
                } else {
                    Toast.makeText(getActivity(), "Ошибка передачи аргументов", Toast.LENGTH_SHORT).show();
                }
            } else {
                Toast.makeText(getActivity(), "Заполните форму", Toast.LENGTH_SHORT).show();
            }
        });

        buttonCancel.setOnClickListener(v -> {
            FragmentManager fragmentManager = getFragmentManager();
            FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
            StudentsFragment studentsFragment = new StudentsFragment();
            fragmentTransaction.setCustomAnimations(R.anim.enter_from_left, R.anim.exit_to_right, R.anim.enter_from_right, R.anim.exit_to_left);
            fragmentTransaction.replace(R.id.fragment_container, studentsFragment);
            fragmentTransaction.commit();
        });
        return view;
    }

    public void onCreateOrUpdate() {
        if (errorMessage == null && gotRequested) {
            Toast.makeText(getActivity(), "Сохранено", Toast.LENGTH_SHORT).show();
            StudentsFragment.students = null;
            StudentsFragment.errorMessage = null;
            FragmentManager fragmentManager = getFragmentManager();
            FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
            StudentsFragment studentsFragment = new StudentsFragment();
            fragmentTransaction.setCustomAnimations(R.anim.enter_from_left, R.anim.exit_to_right, R.anim.enter_from_right, R.anim.exit_to_left);
            fragmentTransaction.replace(R.id.fragment_container, studentsFragment);
            fragmentTransaction.commit();
        } else if (errorMessage != null) {
            Toast.makeText(getActivity(), errorMessage, Toast.LENGTH_LONG).show();
        }
    }
}