package ru.universityallexplled.teacherapplication.Client.Enums;

import com.google.gson.annotations.SerializedName;

public enum Marks {
    @SerializedName("0")
    НП (0),

    @SerializedName("1")
    УП (1),

    @SerializedName("2")
    Неуд (2),

    @SerializedName("3")
    Удовл (3),

    @SerializedName("4")
    Хор (4),

    @SerializedName("5")
    Отл (5);

    Marks(int i) {
    }
}
