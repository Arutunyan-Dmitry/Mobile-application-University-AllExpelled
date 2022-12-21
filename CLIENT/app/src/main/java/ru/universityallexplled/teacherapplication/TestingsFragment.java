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
import android.widget.Button;
import android.widget.ImageButton;
import android.widget.ListView;
import android.widget.Toast;

import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;

import java.util.List;

import ru.universityallexplled.teacherapplication.Client.Controller.MainController;
import ru.universityallexplled.teacherapplication.Client.DtoModels.TestingDto;

public class TestingsFragment extends Fragment {

    MainController mc = new MainController();
    public static List<TestingDto> testings;
    public static String errorMessage;
    public static Boolean gotDeleted;

    public TestingsFragment() {}

    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);
        if (testings == null && errorMessage == null) {
            mc.getTestings(MainActivity.teacher.id, this);
        }
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {super.onCreate(savedInstanceState);}

    @Override
    public View onCreateView(@NotNull LayoutInflater inflater, @Nullable ViewGroup container,
                             @Nullable Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_testings, container, false);

        ImageButton buttonAdd = view.findViewById(R.id.button_add);
        ImageButton buttonEdit = view.findViewById(R.id.button_edit);
        ImageButton buttonUpd = view.findViewById(R.id.button_update);
        ImageButton buttonDel = view.findViewById(R.id.button_delete);

        Button buttonConnect = view.findViewById(R.id.button_connect);
        Button buttonDisconnect = view.findViewById(R.id.button_disconnect);

        ListView viewTestings = view.findViewById(R.id.view_testings);
        ArrayAdapter<TestingDto> TestingsAA;

        if (errorMessage == null && testings != null) {
            TestingsAA = new ArrayAdapter<>(getActivity(), android.R.layout.simple_list_item_single_choice, testings);
            viewTestings.setAdapter(TestingsAA);
            viewTestings.setChoiceMode(ListView.CHOICE_MODE_SINGLE);

            buttonAdd.setOnClickListener(v -> {
                FragmentManager fragmentManager = getFragmentManager();
                FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();

                Bundle bundle = new Bundle();
                bundle.putSerializable(TestingDto.class.getSimpleName(), null);
                TestingFragment testingFragment = new TestingFragment();
                testingFragment.setArguments(bundle);

                fragmentTransaction.setCustomAnimations(R.anim.enter_from_right, R.anim.exit_to_left, R.anim.enter_from_left, R.anim.exit_to_right);
                fragmentTransaction.replace(R.id.fragment_container, testingFragment);
                fragmentTransaction.commit();
            });

            buttonEdit.setOnClickListener(v -> {
                if (viewTestings.getCheckedItemPositions().size() != 0) {
                    FragmentManager fragmentManager = getFragmentManager();
                    FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();

                    Bundle bundle = new Bundle();
                    TestingDto testing = testings.get(viewTestings.getCheckedItemPositions().keyAt(0));
                    bundle.putSerializable(TestingDto.class.getSimpleName(), testing);
                    TestingFragment testingFragment = new TestingFragment();
                    testingFragment.setArguments(bundle);

                    fragmentTransaction.setCustomAnimations(R.anim.enter_from_right, R.anim.exit_to_left, R.anim.enter_from_left, R.anim.exit_to_right);
                    fragmentTransaction.replace(R.id.fragment_container, testingFragment);
                    fragmentTransaction.commit();

                } else {
                    Toast.makeText(getActivity(), "Нет выбранного элемента", Toast.LENGTH_SHORT).show();
                }
            });

            buttonDel.setOnClickListener(v -> {
                if (viewTestings.getCheckedItemPositions().size() != 0) {
                    AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());
                    builder.setMessage("Удалить этот элемент?");
                    builder.setCancelable(true);
                    builder.setPositiveButton("Да",
                            new DialogInterface.OnClickListener() {
                                public void onClick(DialogInterface dialog, int id) {
                                    TestingDto testing = testings.get(viewTestings.getCheckedItemPositions().keyAt(0));
                                    mc.deleteTesting(testing, TestingsFragment.this);
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
                testings = null;
                errorMessage = null;
                gotDeleted = null;
                onGetData();
                Toast.makeText(getActivity(), "Обновлено", Toast.LENGTH_SHORT).show();
            });

            buttonConnect.setOnClickListener(v -> {

                FragmentManager fragmentManager = getFragmentManager();
                FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();

                Bundle bundle = new Bundle();
                bundle.putBoolean("isConnection", Boolean.TRUE);
                StudentTestingFragment studentTestingFragment = new StudentTestingFragment();

                studentTestingFragment.setArguments(bundle);
                fragmentTransaction.setCustomAnimations(R.anim.enter_from_right, R.anim.exit_to_left, R.anim.enter_from_left, R.anim.exit_to_right);
                fragmentTransaction.replace(R.id.fragment_container, studentTestingFragment);
                fragmentTransaction.commit();

            });

            buttonDisconnect.setOnClickListener(v -> {

                FragmentManager fragmentManager = getFragmentManager();
                FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();

                Bundle bundle = new Bundle();
                bundle.putBoolean("isConnection", Boolean.FALSE);
                StudentTestingFragment studentTestingFragment = new StudentTestingFragment();

                studentTestingFragment.setArguments(bundle);
                fragmentTransaction.setCustomAnimations(R.anim.enter_from_right, R.anim.exit_to_left, R.anim.enter_from_left, R.anim.exit_to_right);
                fragmentTransaction.replace(R.id.fragment_container, studentTestingFragment);
                fragmentTransaction.commit();

            });
        } else if (errorMessage != null) {
            Toast.makeText(getActivity(), errorMessage, Toast.LENGTH_LONG).show();
        }
        return view;
    }

    public void onGetData() {
        FragmentManager fragmentManager = getFragmentManager();
        FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
        fragmentTransaction.replace(R.id.fragment_container, new TestingsFragment());
        fragmentTransaction.commit();
    }

    public void onDeleteTesting() {
        if (errorMessage == null && gotDeleted) {
            Toast.makeText(getActivity(), "Удалено", Toast.LENGTH_SHORT).show();
            onGetData();
        } else if (errorMessage != null) {
            Toast.makeText(getActivity(), errorMessage, Toast.LENGTH_LONG).show();
        }
    }
}