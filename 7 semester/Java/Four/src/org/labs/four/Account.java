package org.labs.four;

public class Account {
    private int funds;

    public Account() {
        funds = 0;
    }

    public void deposit(int v) {
        funds += v;
    }

    public void withdraw(int v) {
        funds -= v;
    }

    public int getFunds() {
        return funds;
    }
}
