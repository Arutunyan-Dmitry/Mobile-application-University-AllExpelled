package ru.universityallexplled.teacherapplication;

import android.os.Bundle;

import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;
import android.widget.Toast;

import com.google.android.material.textfield.TextInputLayout;

import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import ru.universityallexplled.teacherapplication.Client.Controller.MainController;
import ru.universityallexplled.teacherapplication.Client.DtoModels.StatementDto;
import ru.universityallexplled.teacherapplication.Client.DtoModels.StudentStatements;

public class StatementFragment extends Fragment {

    MainController mc = new MainController();
    public static String errorMessage;
    public static Boolean gotRequested;
    public static Pattern VALID_DATE_REGEX = Pattern.compile("^\\d{2}/|\\.|-\\d{2}/|\\.|-\\d{4}$", Pattern.CASE_INSENSITIVE);

    public StatementFragment() {}

    @Override
    public void onCreate(Bundle savedInstanceState) {super.onCreate(savedInstanceState);}

    @Override
    public View onCreateView(@NotNull LayoutInflater inflater, @Nullable ViewGroup container,
                             @Nullable Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_statement, container, false);

        Button buttonSave = view.findViewById(R.id.button_save);
        Button buttonCancel = view.findViewById(R.id.button_cancel);

        TextInputLayout statementNaming = view.findViewById(R.id.statement_name);
        TextInputLayout statementDate = view.findViewById(R.id.statement_date);

        ListView viewStatementStudents = view.findViewById(R.id.view_statement_students);
        ArrayAdapter<StudentStatements> statementStudentsAA;

        Bundle arguments = getArguments();
        StatementDto statement;
        if(arguments!=null) {
            statement = (StatementDto) arguments.getSerializable(StatementDto.class.getSimpleName());
            if (statement != null) {
                statementNaming.getEditText().setText(statement.getNaming());
                statementDate.getEditText().setText(statement.getDateCreate());
                List<StudentStatements> stst = new ArrayList<>(statement.getStudentStatements().values());
                statementStudentsAA = new ArrayAdapter<>(getActivity(), android.R.layout.simple_list_item_1, stst);
                viewStatementStudents.setAdapter(statementStudentsAA);
                viewStatementStudents.setChoiceMode(ListView.CHOICE_MODE_NONE);
            } else {
                viewStatementStudents.setVisibility(View.INVISIBLE);
            }
        }

        buttonSave.setOnClickListener(v -> {
            String naming = statementNaming.getEditText().getText().toString();
            String dateCreate = statementDate.getEditText().getText().toString();
            if (!naming.equals("") && !dateCreate.equals("")) {
                Matcher matcher = VALID_DATE_REGEX.matcher(dateCreate);
                if(matcher.find()) {

                    StatementDto st;
                    if (arguments != null) {
                        st = (StatementDto) arguments.getSerializable(StatementDto.class.getSimpleName());
                        if (st != null) {
                            st.setTeacherId(MainActivity.teacher.id);
                            st.setNaming(naming);
                            st.setDateCreate(dateCreate);
                        } else {
                            st = new StatementDto(
                                    null,
                                    MainActivity.teacher.id,
                                    naming,
                                    dateCreate,
                                    new HashMap<Integer, StudentStatements>()
                            );
                        }
                        mc.CreateOrUpdateStatement(st, StatementFragment.this);

                    } else {
                        Toast.makeText(getActivity(), "Ошибка передачи аргументов", Toast.LENGTH_SHORT).show();
                    }
                } else {
                    Toast.makeText(getActivity(), "Даты введены некорректно", Toast.LENGTH_SHORT).show();
                }
            } else {
                Toast.makeText(getActivity(), "Заполните форму", Toast.LENGTH_SHORT).show();
            }
        });

        buttonCancel.setOnClickListener(v -> {
            FragmentManager fragmentManager = getFragmentManager();
            FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
            StatementsFragment statementsFragment = new StatementsFragment();
            fragmentTransaction.setCustomAnimations(R.anim.enter_from_left, R.anim.exit_to_right, R.anim.enter_from_right, R.anim.exit_to_left);
            fragmentTransaction.replace(R.id.fragment_container, statementsFragment);
            fragmentTransaction.commit();
        });
        return view;
    }

    public void onCreateOrUpdate() {
        if (errorMessage == null && gotRequested) {
            Toast.makeText(getActivity(), "Сохранено", Toast.LENGTH_SHORT).show();
            StatementsFragment.statements = null;
            StatementsFragment.errorMessage = null;
            FragmentManager fragmentManager = getFragmentManager();
            FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
            StatementsFragment statementsFragment = new StatementsFragment();
            fragmentTransaction.replace(R.id.fragment_container, statementsFragment);
            fragmentTransaction.commit();
        } else if(errorMessage != null){
            Toast.makeText(getActivity(), errorMessage, Toast.LENGTH_LONG).show();
        }
    }
}