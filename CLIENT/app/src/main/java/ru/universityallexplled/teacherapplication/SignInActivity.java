package ru.universityallexplled.teacherapplication;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.widget.Button;
import android.widget.Toast;

import com.google.android.material.textfield.TextInputLayout;

import java.util.regex.Matcher;
import java.util.regex.Pattern;

import ru.universityallexplled.teacherapplication.Client.Controller.MainController;
import ru.universityallexplled.teacherapplication.Client.DtoModels.TeacherDto;

public class SignInActivity extends AppCompatActivity {

    public static String errorMessage;
    public static TeacherDto teacher;
    public static final Pattern VALID_EMAIL_ADDRESS_REGEX =
            Pattern.compile("^[A-Z0-9._%+-]+@[A-Z0-9.-]+\\.[A-Z]{2,6}$", Pattern.CASE_INSENSITIVE);
    public static final Pattern VALID_PASSWORD_REGEX =
            Pattern.compile("[a-zA-Z]+([+-]?(?=\\.\\d|\\d)(?:\\d+)?(?:\\.?\\d*))(?:[eE]([+-]?\\d+))?", Pattern.CASE_INSENSITIVE);

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_sign_in);
        MainController mc = new MainController();

        Button buttonSingIn = findViewById(R.id.button_sign_in);
        Button buttonRegister = findViewById(R.id.button_registration);

        TextInputLayout login = findViewById(R.id.E_mail);
        TextInputLayout password = findViewById(R.id.Password);

        buttonSingIn.setOnClickListener(v -> {
            MainActivity.isFirstRun = Boolean.TRUE;
            String Login = login.getEditText().getText().toString();
            String Password = password.getEditText().getText().toString();

            if (!Login.equals("") && !Password.equals("")) {
                if(Login.equals("admin") && Password.equals("admin")) {
                    AdminActivity.isUpdated = Boolean.FALSE;
                    Intent intent = new Intent(this, AdminActivity.class);
                    startActivity(intent);
                    SignInActivity.this.finish();
                } else {
                    Matcher matcher = VALID_EMAIL_ADDRESS_REGEX.matcher(Login);
                    if (matcher.find()) {
                        matcher = VALID_PASSWORD_REGEX.matcher(Password);
                        if (matcher.find()) {

                            mc.logInTeacher(Login, Password, this);

                        } else {
                            Toast.makeText(this, "Пароль введён некорректно", Toast.LENGTH_SHORT).show();
                        }
                    } else {
                        Toast.makeText(this, "Почта введена некорректно", Toast.LENGTH_SHORT).show();
                    }
                }
            } else {
                Toast.makeText(this, "Заполните все поля", Toast.LENGTH_SHORT).show();
            }
        });

        buttonRegister.setOnClickListener(v -> {
            Intent intent = new Intent(this, RegistrationActivity.class);
            startActivity(intent);
        });
    }

    public void onSignIn() {
        if (errorMessage != null) {
            Toast.makeText(this, errorMessage, Toast.LENGTH_LONG).show();
        } else {
            Toast.makeText(this, "Вход выполнен", Toast.LENGTH_SHORT).show();
            StudentsFragment.students = null;
            StudentsFragment.errorMessage = null;
            StatementsFragment.statements = null;
            StatementsFragment.errorMessage = null;
            TestingsFragment.errorMessage = null;
            TestingsFragment.testings = null;
            MainActivity.teacher = teacher;
            Intent intent = new Intent(this, MainActivity.class);
            startActivity(intent);
            SignInActivity.this.finish();
        }
    }
}