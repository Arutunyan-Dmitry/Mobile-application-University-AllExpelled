package ru.universityallexplled.teacherapplication;

import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import android.content.Intent;
import android.os.Bundle;

import com.google.android.material.bottomnavigation.BottomNavigationView;

import ru.universityallexplled.teacherapplication.Client.DtoModels.TeacherDto;

public class MainActivity extends AppCompatActivity {

    public static Boolean isFirstRun = Boolean.TRUE;
    public static TeacherDto teacher;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        BottomNavigationView bottomNavigationView = findViewById(R.id.bottomNavigationView);

        if (isFirstRun) {
            replaceFragment(new HomeFragment());
            isFirstRun = Boolean.FALSE;
        }

        bottomNavigationView.setOnItemSelectedListener(item -> {
            switch (item.getItemId()){
                case R.id.m_home:
                    replaceFragment(new HomeFragment());
                    break;
                case R.id.m_students:
                    replaceFragment(new StudentsFragment());
                    break;
                case R.id.m_testings:
                    replaceFragment(new TestingsFragment());
                    break;
                case R.id.m_statements:
                    replaceFragment(new StatementsFragment());
                    break;
                case R.id.m_reports:
                    replaceFragment(new ReportsFragment());
                    break;
            }
            return true;
        });
    }

    public TeacherDto getTeacher() { return teacher; }

    private void replaceFragment(Fragment fragment) {
        if (teacher == null && !fragment.getClass().getSimpleName().equals("HomeFragment")) {
            Intent intent = new Intent(MainActivity.this, SignInActivity.class);
            startActivity(intent);
            MainActivity.this.finish();
        } else {
            FragmentManager fragmentManager = getSupportFragmentManager();
            FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
            fragmentTransaction.setCustomAnimations(R.anim.enter_from_right, R.anim.exit_to_left, R.anim.enter_from_left, R.anim.exit_to_right);
            fragmentTransaction.replace(R.id.fragment_container, fragment);
            fragmentTransaction.commit();
        }
    }
}