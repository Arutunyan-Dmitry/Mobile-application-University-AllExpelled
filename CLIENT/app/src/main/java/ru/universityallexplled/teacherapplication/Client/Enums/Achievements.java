package ru.universityallexplled.teacherapplication.Client.Enums;

import com.google.gson.annotations.SerializedName;

public enum Achievements {
    @SerializedName("0")
    НА (0),

    @SerializedName("1")
    Осв (1),

    @SerializedName("2")
    Неуд (2),

    @SerializedName("3")
    Удовл (3),

    @SerializedName("4")
    Хор (4),

    @SerializedName("5")
    Отл (5),

    @SerializedName("6")
    Незач (6),

    @SerializedName("7")
    Зач (7);

    Achievements(int i) {
    }
}
