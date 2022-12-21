package ru.universityallexplled.teacherapplication;

import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;

import ru.universityallexplled.teacherapplication.HelperFragments.AEFragment;
import ru.universityallexplled.teacherapplication.HelperFragments.UFragment;

public class GreetingActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_greeting);

        MainActivity.isFirstRun = Boolean.TRUE;

        replaceFragmentWithDelay(R.id.u_container, R.anim.enter_from_left_shaking,500, new UFragment());
        replaceFragmentWithDelay(R.id.ae_container, R.anim.enter_from_right_shaking,800, new AEFragment());

        new Handler().postDelayed(new Runnable() {
            @Override
            public void run() {
                Intent intent = new Intent(GreetingActivity.this, MainActivity.class);
                startActivity(intent);
                GreetingActivity.this.finish();
            }
        }, 3000 );
    }

    private void replaceFragmentWithDelay(int id, int entre_animation, int delay, Fragment fragment) {
        new Handler().postDelayed(new Runnable() {
            @Override
            public void run() {
                FragmentManager fragmentManager = getSupportFragmentManager();
                FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
                fragmentTransaction.setCustomAnimations(entre_animation, R.anim.exit_to_left);
                fragmentTransaction.replace(id, fragment);
                fragmentTransaction.commit();
            }
        }, delay );
    }
}