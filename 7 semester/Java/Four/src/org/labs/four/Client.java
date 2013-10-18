package org.labs.four;

public class Client {
    private int purse;

    private String name;

    public Client(String name) {
        this.name = name;
    }

    public void addPurseMoney(int v) {
            purse += v;
    }

    public void removePurseMoney(int v) {
            purse -= v;
    }

    public int getPurse() {
        return purse;
    }

    public String getName() {
        return name;
    }

}
