package ru.universityallexplled.teacherapplication.Client.Enums;

import com.google.gson.annotations.SerializedName;

public enum TestingTypes {
    @SerializedName("0")
    Лабораторная (0),

    @SerializedName("1")
    Лекция (1),

    @SerializedName("2")
    Тест (2),

    @SerializedName("3")
    Другое (3);

    TestingTypes(int i) {
    }
}
