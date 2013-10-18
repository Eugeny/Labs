package org.labs.four;

import java.util.Random;

public class PendingClientOperation {
    private static Random random = new Random();
    private Client client;
    private OperationType operationType;
    private int amount;

    public PendingClientOperation(Client client, int amount, OperationType operationType) {
        this.client = client;
        this.amount = amount;
        this.operationType = operationType;
    }

    public static PendingClientOperation createRandom(Client client) {
        return new PendingClientOperation(
                client,
                random.nextInt(100),
                random.nextBoolean() ? OperationType.Deposit : OperationType.Withdrawal
        );
    }

    public int getAmount() {
        return amount;
    }

    public void setAmount(int amount) {
        this.amount = amount;
    }

    public OperationType getOperationType() {
        return operationType;
    }

    public void setOperationType(OperationType operationType) {
        this.operationType = operationType;
    }

    public Client getClient() {
        return client;
    }

    @Override
    public String toString() {
        return String.format("%s %s (%d)", client.getName(), operationType, amount);
    }

    public enum OperationType {
        Deposit, Withdrawal
    }
}
