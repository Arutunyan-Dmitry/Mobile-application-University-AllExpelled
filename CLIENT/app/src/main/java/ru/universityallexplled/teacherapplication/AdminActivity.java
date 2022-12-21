package ru.universityallexplled.teacherapplication;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.widget.ArrayAdapter;
import android.widget.ImageButton;
import android.widget.ListView;
import android.widget.Toast;

import java.util.List;

import ru.universityallexplled.teacherapplication.Client.Controller.MainController;
import ru.universityallexplled.teacherapplication.Client.DtoModels.TeacherDto;

public class AdminActivity extends AppCompatActivity {

    MainController mc = new MainController();
    public static List<TeacherDto> teachers;
    public static String errorMessage;
    public static boolean isUpdated;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_admin);

        if(!isUpdated) {
            mc.getAllTeachers(this);
        }

        ImageButton logOut = findViewById(R.id.button_admin_logout);
        ListView viewTeachers = findViewById(R.id.view_teachers);
        ArrayAdapter<TeacherDto> teachersAA;

        if (errorMessage == null) {
            if (teachers != null) {
                teachersAA = new ArrayAdapter<>(this, android.R.layout.simple_list_item_1, teachers);
                viewTeachers.setAdapter(teachersAA);
                viewTeachers.setChoiceMode(ListView.CHOICE_MODE_NONE);

            }

            logOut.setOnClickListener(v -> {
                Intent intent = new Intent(this, SignInActivity.class);
                startActivity(intent);
                AdminActivity.this.finish();
            });
        } else {
            Toast.makeText(AdminActivity.this, errorMessage, Toast.LENGTH_LONG).show();
        }
    }

    public void onGetData() {
        isUpdated = Boolean.TRUE;
        Intent intent = new Intent(this, AdminActivity.class);
        overridePendingTransition(0, 0);
        startActivity(intent);
        AdminActivity.this.finish();
    }
}