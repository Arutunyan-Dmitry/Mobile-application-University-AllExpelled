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
import android.widget.ListView;
import android.widget.Spinner;
import android.widget.Toast;

import com.google.android.material.textfield.TextInputLayout;

import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

import ru.universityallexplled.teacherapplication.Client.Controller.MainController;
import ru.universityallexplled.teacherapplication.Client.DtoModels.LessonDto;
import ru.universityallexplled.teacherapplication.Client.DtoModels.StudentTesting;
import ru.universityallexplled.teacherapplication.Client.DtoModels.TestingDto;
import ru.universityallexplled.teacherapplication.Client.Enums.TestingTypes;

public class TestingFragment extends Fragment {

    MainController mc = new MainController();
    public static List<LessonDto> lessons;
    public static String errorMessage;
    public static Boolean gotRequested;
    private static TestingDto testing;

    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);
        if (lessons == null && errorMessage == null) {
            mc.getAllLessons(this);
        }
    }

    public TestingFragment() {}

    @Override
    public void onCreate(Bundle savedInstanceState) {super.onCreate(savedInstanceState);}

    @Override
    public View onCreateView(@NotNull LayoutInflater inflater,@Nullable ViewGroup container,
                             @Nullable Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_testing, container, false);

        Button buttonSave = view.findViewById(R.id.button_save);
        Button buttonCancel = view.findViewById(R.id.button_cancel);

        TextInputLayout testingName = view.findViewById(R.id.testing_name);

        ListView viewTestingStudents = view.findViewById(R.id.view_testing_students);
        ArrayAdapter<StudentTesting> testingStudentsAA;

        Spinner testingLesson = view.findViewById(R.id.testing_lesson);
        Spinner testingType = view.findViewById(R.id.testing_type);
        ArrayAdapter<TestingTypes> testingTypesAA = new ArrayAdapter<>(getActivity(), android.R.layout.simple_spinner_item, TestingTypes.values());
        testingTypesAA.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        testingType.setAdapter(testingTypesAA);

        Bundle arguments = getArguments();
        if(arguments!=null) {
            testing = (TestingDto) arguments.getSerializable(TestingDto.class.getSimpleName());
        }

        if (testing != null) {
            testingName.getEditText().setText(testing.getNaming());
            List<StudentTesting> stts = new ArrayList<>(testing.getStudentTestings().values());
            testingStudentsAA = new ArrayAdapter<>(getActivity(), android.R.layout.simple_list_item_1, stts);
            viewTestingStudents.setAdapter(testingStudentsAA);
            viewTestingStudents.setChoiceMode(ListView.CHOICE_MODE_NONE);
            testingType.setSelection(testingTypesAA.getPosition(testing.getType()));
        } else {
            viewTestingStudents.setVisibility(View.INVISIBLE);
        }

        if(lessons != null) {
            ArrayAdapter<LessonDto> testingLessonAA = new ArrayAdapter<>(getActivity(), android.R.layout.simple_spinner_item, lessons);
            testingLessonAA.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
            testingLesson.setAdapter(testingLessonAA);

            if (testing != null) {
                LessonDto tmpLesson = new LessonDto();
                for(LessonDto lesson : lessons) {
                    if(lesson.getId() == testing.getLessonId()) {
                        tmpLesson = lesson;
                    }
                }
                testingLesson.setSelection(testingLessonAA.getPosition(tmpLesson));
            }

            buttonSave.setOnClickListener(v -> {
                String naming = testingName.getEditText().getText().toString();
                if (!naming.equals("")) {
                    if(arguments!=null) {
                        testing = (TestingDto) arguments.getSerializable(TestingDto.class.getSimpleName());
                    }
                    TestingDto testingDto;
                    if(testing != null) {
                        testingDto = testing;
                        testingDto.setTeacherId(MainActivity.teacher.id);
                        testingDto.setNaming(naming);
                        testingDto.setType((TestingTypes) testingType.getSelectedItem());
                        testingDto.setLessonId(((LessonDto) testingLesson.getSelectedItem()).id);
                        testingDto.setLessonName(((LessonDto) testingLesson.getSelectedItem()).lessonName);
                    } else {
                        testingDto = new TestingDto(
                                null,
                                MainActivity.teacher.id,
                                ((LessonDto) testingLesson.getSelectedItem()).id,
                                naming,
                                ((LessonDto) testingLesson.getSelectedItem()).lessonName,
                                (TestingTypes) testingType.getSelectedItem(),
                                new HashMap<>()
                        );
                    }
                    mc.createOrUpdateTesting(testingDto, TestingFragment.this);
                } else {
                    Toast.makeText(getActivity(), "Заполните форму", Toast.LENGTH_SHORT).show();
                }
            });

            buttonCancel.setOnClickListener(v -> {
                FragmentManager fragmentManager = getFragmentManager();
                FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
                TestingsFragment testingsFragment = new TestingsFragment();
                fragmentTransaction.setCustomAnimations(R.anim.enter_from_left, R.anim.exit_to_right, R.anim.enter_from_right, R.anim.exit_to_left);
                fragmentTransaction.replace(R.id.fragment_container, testingsFragment);
                fragmentTransaction.commit();
            });
        }
        return view;
    }

    public void onCreateOrUpdate() {
        if (errorMessage == null && gotRequested) {
            Toast.makeText(getActivity(), "Сохранено", Toast.LENGTH_SHORT).show();
            TestingsFragment.testings = null;
            TestingsFragment.errorMessage = null;
            FragmentManager fragmentManager = getFragmentManager();
            FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
            TestingsFragment testingsFragment = new TestingsFragment();
            fragmentTransaction.replace(R.id.fragment_container, testingsFragment);
            fragmentTransaction.commit();
        } else if (errorMessage != null) {
            Toast.makeText(getActivity(), errorMessage, Toast.LENGTH_LONG).show();
        }
    }

    public void onGetData() {
        FragmentManager fragmentManager = getFragmentManager();
        FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
        fragmentTransaction.replace(R.id.fragment_container, new TestingFragment());
        fragmentTransaction.commit();
    }
}