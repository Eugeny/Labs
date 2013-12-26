package org.labs.four;

import java.util.Random;

public class ClientOperation {
    private static Random random = new Random();
    private Client client;
    private OperationType operationType;
    private int amount;
    private Client benificiar;

    public ClientOperation(Client client, int amount, OperationType operationType, Client benificiar) {
        this.client = client;
        this.amount = amount;
        this.operationType = operationType;
        this.benificiar = benificiar;
    }

    public static ClientOperation createRandom(Client client, Client benificiar) {
        return new ClientOperation(
                client,
                random.nextInt(100),
                random.nextBoolean() ? OperationType.Deposit : OperationType.Withdrawal,
                benificiar);
    }

    public void execute(Bank bank, Logger logger) {
        Transaction tx = new Transaction();
        tx.addResource(client);
        Account account = bank.getAccount(client);
        if (getOperationType() == OperationType.Withdrawal)
            tx.addResource(account);
        Account targetAccount = bank.getAccount(benificiar);
        if (getOperationType() == OperationType.Deposit)
            tx.addResource(targetAccount);

        logger.logOperation(this, "beginning");
        bank.beginTransaction(tx);
        logger.logOperation(this, "started");

        try {
            int purse = client.getPurse();
            if (getOperationType() == ClientOperation.OperationType.Deposit) {
                if (client.getStatus() != benificiar.getStatus())
                    return;
                if (amount > purse)
                    return;
                client.removePurseMoney(amount);
                targetAccount.deposit(amount);
            }
            if (getOperationType() == ClientOperation.OperationType.Withdrawal) {
                if (amount > account.getFunds())
                    return;
                account.withdraw(amount);
                client.addPurseMoney(amount);
            }
            Thread.sleep(500);
        } catch (InterruptedException e) {
            e.printStackTrace();
        } finally {
            logger.logOperation(this, "complete");
            bank.endTransaction(tx);
        }
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
        return String.format("%s -> %s %s (%d)",
                client.getName(),
                (operationType == OperationType.Deposit ? benificiar.getName() : client.getName()),
                operationType,
                amount);
    }

    public Client getBenificiar() {
        return benificiar;
    }

    public enum OperationType {
        Deposit, Withdrawal
    }
}
