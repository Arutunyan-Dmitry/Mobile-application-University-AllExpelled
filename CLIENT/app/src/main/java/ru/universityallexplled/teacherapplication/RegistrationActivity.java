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

public class RegistrationActivity extends AppCompatActivity {

    public static String errorMessage;
    public static Boolean gotRequested;
    public static final Pattern VALID_EMAIL_ADDRESS_REGEX =
            Pattern.compile("^[A-Z0-9._%+-]+@[A-Z0-9.-]+\\.[A-Z]{2,6}$", Pattern.CASE_INSENSITIVE);
    public static final Pattern VALID_PASSWORD_REGEX =
            Pattern.compile("[a-zA-Z]+([+-]?(?=\\.\\d|\\d)(?:\\d+)?(?:\\.?\\d*))(?:[eE]([+-]?\\d+))?", Pattern.CASE_INSENSITIVE);

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_registration);
        MainController mc = new MainController();

        Button buttonRegister = findViewById(R.id.button_register);
        TextInputLayout teacher_name = findViewById(R.id.user_name);
        TextInputLayout teacher_surname = findViewById(R.id.user_surname);
        TextInputLayout teacher_middle_name = findViewById(R.id.user_middle_name);
        TextInputLayout teacher_email = findViewById(R.id.user_E_mail);
        TextInputLayout teacher_password = findViewById(R.id.user_password);
        TextInputLayout teacher_password_d = findViewById(R.id.user_password_again);

        buttonRegister.setOnClickListener(v -> {

            String name = teacher_name.getEditText().getText().toString();
            String surname = teacher_surname.getEditText().getText().toString();
            String middleName = teacher_middle_name.getEditText().getText().toString();
            String email = teacher_email.getEditText().getText().toString();
            String password = teacher_password.getEditText().getText().toString();
            String password_again = teacher_password_d.getEditText().getText().toString();

            if(!name.equals("") && !surname.equals("") && !middleName.equals("") && !email.equals("") &&
            !password.equals("") && !password_again.equals("")) {
                Matcher matcher = VALID_EMAIL_ADDRESS_REGEX.matcher(email);
                if(matcher.find()) {
                    matcher = VALID_PASSWORD_REGEX.matcher(password);
                    if(matcher.find()) {
                        if(password.equals(password_again)) {

                            TeacherDto teacher = new TeacherDto(null, name, surname, middleName,
                                    email, password);
                            mc.registerTeacher(teacher, this);

                        } else {
                            Toast.makeText(this, "Пароли не совпадают", Toast.LENGTH_SHORT).show();
                        }
                    } else {
                        Toast.makeText(this, "Пароль должен содержать буквы, символы и цифры", Toast.LENGTH_LONG).show();
                    }
                } else {
                    Toast.makeText(this, "Почта введена некорректно", Toast.LENGTH_SHORT).show();
                }
            } else {
                Toast.makeText(this, "Заполните все поля", Toast.LENGTH_SHORT).show();
            }
        });
    }

    public void onRegister() {
        if (errorMessage == null && gotRequested) {
            Intent intent = new Intent(this, SignInActivity.class);
            startActivity(intent);
        } else {
            Toast.makeText(this, errorMessage, Toast.LENGTH_LONG).show();
        }
    }
}