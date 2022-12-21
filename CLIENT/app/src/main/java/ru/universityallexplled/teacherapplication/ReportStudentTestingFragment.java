package ru.universityallexplled.teacherapplication;

import android.os.Bundle;

import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.Toast;

import com.google.android.material.textfield.TextInputLayout;

import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;

import java.util.regex.Matcher;
import java.util.regex.Pattern;

import ru.universityallexplled.teacherapplication.Client.Controller.MainController;
import ru.universityallexplled.teacherapplication.Client.DtoModels.SendMailDto;

public class ReportStudentTestingFragment extends Fragment {

    MainController mc = new MainController();
    public static String errorMessage;
    public static Boolean isSent;
    public static final Pattern VALID_DATE_REGEX = Pattern.compile("^\\d{2}/|\\.|-\\d{2}/|\\.|-\\d{4}$", Pattern.CASE_INSENSITIVE);

    public ReportStudentTestingFragment() {}

    @Override
    public void onCreate(Bundle savedInstanceState) {super.onCreate(savedInstanceState);}

    @Override
    public View onCreateView(@NotNull LayoutInflater inflater, @Nullable ViewGroup container,
                             @Nullable Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_report_student_testing, container, false);

        Button buttonPdf = view.findViewById(R.id.button_to_pdf);

        TextInputLayout dateFrom = view.findViewById(R.id.date_from);
        TextInputLayout dateTo = view.findViewById(R.id.date_to);

        buttonPdf.setOnClickListener(v -> {
            String dateFromStr = dateFrom.getEditText().getText().toString();
            String dateToStr = dateTo.getEditText().getText().toString();

            if(!dateFromStr.equals("") && !dateToStr.equals("")) {
                Matcher matcherFrom = VALID_DATE_REGEX.matcher(dateFromStr);
                Matcher matcherTo = VALID_DATE_REGEX.matcher(dateToStr);

                if(matcherFrom.find() && matcherTo.find()) {
                    SendMailDto mail = new SendMailDto(
                            MainActivity.teacher.email,
                            "Отчёт по студентам, испытаниям и ведомостям",
                            "С уважением, университет 'Все отчислены'",
                            dateFromStr + " " + dateToStr + " " + MainActivity.teacher.id
                    );
                    mc.sendMail(mail, ReportStudentTestingFragment.this);
                } else {
                    Toast.makeText(getActivity(), "Даты введены некорректно", Toast.LENGTH_SHORT).show();
                }
            }
        });
        return view;
    }

    public void onMessageSent() {
        if (errorMessage == null && isSent) {
            Toast.makeText(getActivity(), "Письмо отправлено", Toast.LENGTH_LONG).show();
            FragmentManager fragmentManager = getFragmentManager();
            FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
            fragmentTransaction.setCustomAnimations(R.anim.enter_from_left, R.anim.exit_to_right, R.anim.enter_from_right, R.anim.exit_to_left);
            fragmentTransaction.remove(ReportStudentTestingFragment.this);
            fragmentTransaction.commit();
        } else if(errorMessage != null){
            Toast.makeText(getActivity(), errorMessage, Toast.LENGTH_LONG).show();
        }
    }
}