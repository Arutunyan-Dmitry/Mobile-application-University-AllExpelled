package ru.universityallexplled.teacherapplication;

import android.os.Bundle;

import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;

import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;

public class ReportsFragment extends Fragment {

    public ReportsFragment() {}

    @Override
    public void onCreate(Bundle savedInstanceState) {super.onCreate(savedInstanceState);}

    @Override
    public View onCreateView(@NotNull LayoutInflater inflater, @Nullable ViewGroup container,
                             @Nullable Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_reports, container, false);

        Button buttonList = view.findViewById(R.id.button_list);
        Button buttonRep = view.findViewById(R.id.button_rep);

        buttonList.setOnClickListener(v -> {
            FragmentManager fragmentManager = getFragmentManager();
            FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();

            ReportStudentDisciplineFragment reportStudentDisciplineFragment = new ReportStudentDisciplineFragment();
            fragmentTransaction.setCustomAnimations(R.anim.enter_from_left, R.anim.exit_to_right, R.anim.enter_from_right, R.anim.exit_to_left);
            fragmentTransaction.replace(R.id.list_container, reportStudentDisciplineFragment);
            fragmentTransaction.commit();
        });

        buttonRep.setOnClickListener(v -> {
            FragmentManager fragmentManager = getFragmentManager();
            FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();

            ReportStudentTestingFragment reportStudentTestingFragment = new ReportStudentTestingFragment();
            fragmentTransaction.setCustomAnimations(R.anim.enter_from_right, R.anim.exit_to_left, R.anim.enter_from_left, R.anim.exit_to_right);
            fragmentTransaction.replace(R.id.report_container, reportStudentTestingFragment);
            fragmentTransaction.commit();
        });

        return view;
    }
}