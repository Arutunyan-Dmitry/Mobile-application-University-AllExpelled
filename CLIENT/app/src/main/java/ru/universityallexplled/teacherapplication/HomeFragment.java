package ru.universityallexplled.teacherapplication;

import android.annotation.SuppressLint;
import android.content.Intent;
import android.os.Bundle;

import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;

import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;

public class HomeFragment extends Fragment {

    public HomeFragment() {}

    @Override
    public void onCreate(Bundle savedInstanceState) {super.onCreate(savedInstanceState);}

    @SuppressLint("SetTextI18n")
    @Override
    public View onCreateView(@NotNull LayoutInflater inflater, @Nullable ViewGroup container,
                             @Nullable Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_home, container, false);

        Button buttonStart = view.findViewById(R.id.button_start);
        TextView info = view.findViewById(R.id.info_text);

        if (MainActivity.teacher != null) {
            info.setText("Здравствуйте, " + MainActivity.teacher.name + " " + MainActivity.teacher.middleName + "\n\n " +
                    "Для входа под другим именем нажмите кнопку ниже");
        }

        buttonStart.setOnClickListener(v -> {
            Intent intent = new Intent(getActivity(), SignInActivity.class);
            startActivity(intent);
            getActivity().finish();
        });

        return view;
    }
}