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
import ru.universityallexplled.teacherapplication.Client.DtoModels.StatementDto;

public class StatementsFragment extends Fragment {

    MainController mc = new MainController();
    public static List<StatementDto> statements;
    public static String errorMessage;
    public static Boolean gotDeleted;

    public StatementsFragment() {}

    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);
        if (statements == null && errorMessage == null) {
            mc.getStatements(MainActivity.teacher.id, this);
        }
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(@NotNull LayoutInflater inflater, @Nullable ViewGroup container,
                             @Nullable Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_statements, container, false);

        ImageButton buttonAdd = view.findViewById(R.id.button_add);
        ImageButton buttonEdit = view.findViewById(R.id.button_edit);
        ImageButton buttonUpd = view.findViewById(R.id.button_update);
        ImageButton buttonDel = view.findViewById(R.id.button_delete);

        Button buttonConnect = view.findViewById(R.id.button_connect);
        Button buttonDisconnect = view.findViewById(R.id.button_disconnect);

        ListView viewStatements = view.findViewById(R.id.view_statements);
        ArrayAdapter<StatementDto> StatementsAA;

        if (errorMessage == null && statements != null) {
            StatementsAA = new ArrayAdapter<>(getActivity(), android.R.layout.simple_list_item_single_choice, statements);
            viewStatements.setAdapter(StatementsAA);
            viewStatements.setChoiceMode(ListView.CHOICE_MODE_SINGLE);

            buttonAdd.setOnClickListener(v -> {
                FragmentManager fragmentManager = getFragmentManager();
                FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();

                Bundle bundle = new Bundle();
                bundle.putSerializable(StatementDto.class.getSimpleName(), null);
                StatementFragment statementFragment = new StatementFragment();
                statementFragment.setArguments(bundle);

                fragmentTransaction.setCustomAnimations(R.anim.enter_from_right, R.anim.exit_to_left, R.anim.enter_from_left, R.anim.exit_to_right);
                fragmentTransaction.replace(R.id.fragment_container, statementFragment);
                fragmentTransaction.commit();
            });

            buttonEdit.setOnClickListener(v -> {
                if (viewStatements.getCheckedItemPositions().size() != 0) {

                    FragmentManager fragmentManager = getFragmentManager();
                    FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();

                    Bundle bundle = new Bundle();
                    StatementDto statement = statements.get(viewStatements.getCheckedItemPositions().keyAt(0));
                    bundle.putSerializable(StatementDto.class.getSimpleName(), statement);
                    StatementFragment statementFragment = new StatementFragment();
                    statementFragment.setArguments(bundle);

                    fragmentTransaction.setCustomAnimations(R.anim.enter_from_right, R.anim.exit_to_left, R.anim.enter_from_left, R.anim.exit_to_right);
                    fragmentTransaction.replace(R.id.fragment_container, statementFragment);
                    fragmentTransaction.commit();

                } else {
                    Toast.makeText(getActivity(), "Нет выбранного элемента", Toast.LENGTH_SHORT).show();
                }
            });

            buttonDel.setOnClickListener(v -> {
                if (viewStatements.getCheckedItemPositions().size() != 0) {
                    AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());
                    builder.setMessage("Удалить этот элемент?");
                    builder.setCancelable(true);
                    builder.setPositiveButton("Да",
                            new DialogInterface.OnClickListener() {
                                public void onClick(DialogInterface dialog, int id) {
                                    StatementDto statement = statements.get(viewStatements.getCheckedItemPositions().keyAt(0));
                                    mc.deleteStatement(statement, StatementsFragment.this);
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
                statements = null;
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
                StudentStatementFragment studentStatementFragment = new StudentStatementFragment();

                studentStatementFragment.setArguments(bundle);
                fragmentTransaction.setCustomAnimations(R.anim.enter_from_right, R.anim.exit_to_left, R.anim.enter_from_left, R.anim.exit_to_right);
                fragmentTransaction.replace(R.id.fragment_container, studentStatementFragment);
                fragmentTransaction.commit();
            });

            buttonDisconnect.setOnClickListener(v -> {
                FragmentManager fragmentManager = getFragmentManager();
                FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();

                Bundle bundle = new Bundle();
                bundle.putBoolean("isConnection", Boolean.FALSE);
                StudentStatementFragment studentStatementFragment = new StudentStatementFragment();

                studentStatementFragment.setArguments(bundle);
                fragmentTransaction.setCustomAnimations(R.anim.enter_from_right, R.anim.exit_to_left, R.anim.enter_from_left, R.anim.exit_to_right);
                fragmentTransaction.replace(R.id.fragment_container, studentStatementFragment);
                fragmentTransaction.commit();
            });
        } else if (errorMessage != null){
            Toast.makeText(getActivity(), errorMessage, Toast.LENGTH_LONG).show();
        }
        return view;
    }

    public void onGetData() {
        FragmentManager fragmentManager = getFragmentManager();
        FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
        fragmentTransaction.replace(R.id.fragment_container, new StatementsFragment());
        fragmentTransaction.commit();
    }

    public void onDeleteStatement() {
        if (errorMessage == null && gotDeleted) {
            Toast.makeText(getActivity(), "Удалено", Toast.LENGTH_SHORT).show();
            onGetData();
        } else if (errorMessage != null){
            Toast.makeText(getActivity(), errorMessage, Toast.LENGTH_LONG).show();
        }
    }
}