package ru.universityallexplled.teacherapplication;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.widget.Toast;

import org.apache.poi.xwpf.usermodel.XWPFDocument;
import org.jetbrains.annotations.Nullable;

import java.io.IOException;
import java.io.OutputStream;

public class WordSaveDialogActivity extends AppCompatActivity {

    public static XWPFDocument doc;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_word_save_dialog);
        createAndSave();
    }

    public void createAndSave(){
        Intent intent = new Intent(Intent.ACTION_CREATE_DOCUMENT);
        intent.addCategory(Intent.CATEGORY_OPENABLE);
        intent.setType("application/msword");
        intent.putExtra(Intent.EXTRA_TITLE, "Отчёт.doc");
        startActivityForResult(intent, 1);
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, @Nullable Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if (requestCode == 1) {
            if(resultCode == RESULT_OK) {
                Uri uri = data.getData();
                try {
                    OutputStream outputStream = getContentResolver().openOutputStream(uri);
                    doc.write(outputStream);
                    outputStream.close();
                    Toast.makeText(this, "Файл сохранён", Toast.LENGTH_LONG).show();
                } catch (IOException e) {
                    Toast.makeText(this, e.getMessage(), Toast.LENGTH_LONG).show();
                }
            }
        }
        WordSaveDialogActivity.this.finish();
    }
}