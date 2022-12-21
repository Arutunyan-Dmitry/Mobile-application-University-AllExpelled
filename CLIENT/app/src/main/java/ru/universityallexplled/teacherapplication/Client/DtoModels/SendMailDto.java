package ru.universityallexplled.teacherapplication.Client.DtoModels;

import java.io.Serializable;

public class SendMailDto implements Serializable {
    public String mailAddress;
    public String subject;
    public String text;
    public String parametrs;

    public SendMailDto() {}

    public SendMailDto(String mailAddress, String subject, String text, String parametrs) {
        this.mailAddress = mailAddress;
        this.subject = subject;
        this.text = text;
        this.parametrs = parametrs;
    }

    public String getMailAddress() {return mailAddress;}
    public void setMailAddress(String mailAddress) {this.mailAddress = mailAddress;}

    public String getSubject() {return subject;}
    public void setSubject(String subject) {this.subject = subject;}

    public String getText() {return text;}
    public void setText(String text) {this.text = text;}

    public String getParametrs() {return parametrs;}
    public void setParametrs(String parametrs) {this.parametrs = parametrs;}
}
