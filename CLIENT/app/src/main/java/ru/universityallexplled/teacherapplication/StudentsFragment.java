package ru.universityallexplled.teacherapplication;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.os.Bundle;

import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageButton;
import android.widget.ListView;
import android.widget.Toast;

import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;

import java.util.List;

import ru.universityallexplled.teacherapplication.Client.Controller.MainController;
import ru.universityallexplled.teacherapplication.Client.DtoModels.StudentDto;

public class StudentsFragment extends Fragment {

    MainController mc = new MainController();
    public static List<StudentDto> students;
    public static String errorMessage;
    public static Boolean gotDeleted;

    public StudentsFragment() {}

    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);
        if (students == null && errorMessage == null) {
            mc.getStudents(MainActivity.teacher.id, this);
        }
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(@NotNull LayoutInflater inflater,@Nullable ViewGroup container,
                             @Nullable Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_students, container, false);

        ImageButton buttonAdd = view.findViewById(R.id.button_add);;
        ImageButton buttonEdit = view.findViewById(R.id.button_edit);;
        ImageButton buttonUpd = view.findViewById(R.id.button_update);;
        ImageButton buttonDel = view.findViewById(R.id.button_delete);;
        ListView viewStudents;
        ArrayAdapter<StudentDto> StudentsAA;
        viewStudents = view.findViewById(R.id.view_students);

        if (errorMessage == null && students != null) {
            StudentsAA = new ArrayAdapter<>(getActivity(), android.R.layout.simple_list_item_single_choice, students);
            viewStudents.setAdapter(StudentsAA);
            viewStudents.setChoiceMode(ListView.CHOICE_MODE_SINGLE);

            buttonAdd.setOnClickListener(v -> {
                FragmentManager fragmentManager = getFragmentManager();
                FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();

                Bundle bundle = new Bundle();
                bundle.putSerializable(StudentDto.class.getSimpleName(), null);
                StudentFragment studentFragment = new StudentFragment();
                studentFragment.setArguments(bundle);

                fragmentTransaction.setCustomAnimations(R.anim.enter_from_right, R.anim.exit_to_left, R.anim.enter_from_left, R.anim.exit_to_right);
                fragmentTransaction.replace(R.id.fragment_container, studentFragment);
                fragmentTransaction.commit();
            });

            buttonEdit.setOnClickListener(v -> {
                if (viewStudents.getCheckedItemPositions().size() != 0) {
                    FragmentManager fragmentManager = getFragmentManager();
                    FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();

                    Bundle bundle = new Bundle();
                    StudentDto student = students.get(viewStudents.getCheckedItemPositions().keyAt(0));
                    bundle.putSerializable(StudentDto.class.getSimpleName(), student);
                    StudentFragment studentFragment = new StudentFragment();
                    studentFragment.setArguments(bundle);

                    fragmentTransaction.setCustomAnimations(R.anim.enter_from_right, R.anim.exit_to_left, R.anim.enter_from_left, R.anim.exit_to_right);
                    fragmentTransaction.replace(R.id.fragment_container, studentFragment);
                    fragmentTransaction.commit();

                } else {
                    Toast.makeText(getActivity(), "Нет выбранного элемента", Toast.LENGTH_SHORT).show();
                }
            });

            buttonDel.setOnClickListener(v -> {
                if (viewStudents.getCheckedItemPositions().size() != 0) {
                    AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());
                    builder.setMessage("Удалить этот элемент?");
                    builder.setCancelable(true);
                    builder.setPositiveButton("Да",
                            new DialogInterface.OnClickListener() {
                                public void onClick(DialogInterface dialog, int id) {
                                    StudentDto student = students.get(viewStudents.getCheckedItemPositions().keyAt(0));
                                    mc.deleteStudent(student, StudentsFragment.this);
                                }});
                    builder.setNegativeButton("Нет",
                            new DialogInterface.OnClickListener() {
                                public void onClick(DialogInterface dialog, int id) {
                                    dialog.cancel();
                                }});
                    AlertDialog alert11 = builder.create();
                    alert11.show();
                } else {
                    Toast.makeText(getActivity(), "Нет выбранного элемента", Toast.LENGTH_SHORT).show();
                }
            });

            buttonUpd.setOnClickListener(v -> {
                students = null;
                errorMessage = null;
                gotDeleted = null;
                onGetData();
                Toast.makeText(getActivity(), "Обновлено", Toast.LENGTH_SHORT).show();
            });
        } else if (errorMessage != null) {
            Toast.makeText(getActivity(), errorMessage, Toast.LENGTH_LONG).show();
        }
        return view;
    }

    public void onGetData() {
        FragmentManager fragmentManager = getFragmentManager();
        FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
        fragmentTransaction.replace(R.id.fragment_container, new StudentsFragment());
        fragmentTransaction.commit();
    }

    public void onDeleteStudent() {
        if (errorMessage == null && gotDeleted) {
            Toast.makeText(getActivity(), "Удалено", Toast.LENGTH_SHORT).show();
            onGetData();
        } else if (errorMessage != null) {
            Toast.makeText(getActivity(), errorMessage, Toast.LENGTH_LONG).show();
        }
    }
}