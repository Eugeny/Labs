package org.labs.four;

import java.util.*;
import java.util.concurrent.ConcurrentLinkedQueue;

public class Bank {
    private Map<Client, Account> accounts = new HashMap<Client, Account>();
    private Queue<ClientOperation> queue = new ConcurrentLinkedQueue<ClientOperation>();
    private List<Transaction> activeTransactions = new ArrayList<Transaction>();

    public void registerClient(Client client) {
        if (!accounts.containsKey(client))
            accounts.put(client, new Account());
    }

    public void enqueueOperation(ClientOperation operation) {
        synchronized (queue) {
            queue.add(operation);
            queue.notify();
        }
    }

    public void beginTransaction(Transaction tx) {
        while (true) {
            Transaction blockingTransaction = null;
            synchronized (activeTransactions) {
                for (Transaction atx : activeTransactions)
                    for (Object resource : atx.getResources())
                        if (tx.getResources().contains(resource)) {
                            blockingTransaction = atx;
                            break;
                        }
                if (blockingTransaction == null) {
                    activeTransactions.add(tx);
                    break;
                }
            }
            try {
                synchronized (blockingTransaction) {
                    blockingTransaction.wait();
                }
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
    }

    public void endTransaction(Transaction tx) {
        synchronized (activeTransactions) {
            activeTransactions.remove(tx);
        }
        synchronized (tx) {
            tx.notifyAll();
        }
    }

    public boolean validateOperation(ClientOperation operation) {
        if (operation.getOperationType() == ClientOperation.OperationType.Deposit) {
            if (operation.getClient().getStatus() != operation.getBenificiar().getStatus())
                return false;
            if (operation.getAmount() > operation.getClient().getPurse())
                return false;
        }
        if (operation.getOperationType() == ClientOperation.OperationType.Withdrawal) {
            if (operation.getAmount() > accounts.get(operation.getClient()).getFunds())
                return false;
        }
        return true;
    }

    public boolean performOperation(ClientOperation operation) {
        Client client = operation.getClient();
        Account account = accounts.get(client);

        synchronized (client) {
            synchronized (account) {
                if (validateOperation(operation)) {
                    if (operation.getOperationType() == ClientOperation.OperationType.Deposit) {
                        client.removePurseMoney(operation.getAmount());
                        Account targetAccount = getAccount(operation.getBenificiar());
                        synchronized (targetAccount) {
                            targetAccount.deposit(operation.getAmount());
                        }
                    }
                    if (operation.getOperationType() == ClientOperation.OperationType.Withdrawal) {
                        client.addPurseMoney(operation.getAmount());
                        account.withdraw(operation.getAmount());
                    }
                    return true;
                } else {
                    return false;
                }
            }
        }
    }

    public Set<Client> getClients() {
        return accounts.keySet();
    }

    public Collection<Account> getAccounts() {
        return accounts.values();
    }

    public Account getAccount(Client client) {
        return accounts.get(client);
    }

    public Queue<ClientOperation> getQueue() {
        return queue;
    }
}
