package org.labs.four;

import java.util.Collection;
import java.util.HashMap;
import java.util.Queue;
import java.util.Set;
import java.util.concurrent.ConcurrentLinkedQueue;
import java.util.concurrent.locks.Lock;
import java.util.concurrent.locks.ReentrantLock;

public class Bank {
    private HashMap<Client, Account> accounts = new HashMap<Client, Account>();

    private Lock supervisorLock = new ReentrantLock();

    private Queue<PendingClientOperation> queue = new ConcurrentLinkedQueue<PendingClientOperation>();
    public void registerClient(Client client) {
        if (!accounts.containsKey(client))
            accounts.put(client, new Account());
    }

    public void enqueueOperation(PendingClientOperation operation) {
        synchronized (queue) {
            queue.add(operation);
            queue.notify();
        }
    }

    public boolean validateOperation(PendingClientOperation operation) {
        if (operation.getOperationType() == PendingClientOperation.OperationType.Deposit) {
            if (operation.getAmount() > operation.getClient().getPurse())
                return false;
        }
        if (operation.getOperationType() == PendingClientOperation.OperationType.Withdrawal) {
            if (operation.getAmount() > accounts.get(operation.getClient()).getFunds())
                return false;
        }
        return true;
    }

    public boolean performOperation(PendingClientOperation operation) {
        Client client = operation.getClient();
        Account account = accounts.get(client);

        supervisorLock.lock();
        supervisorLock.unlock();

        synchronized (client) {
            synchronized (account) {
                if (validateOperation(operation)) {
                    if (operation.getOperationType() == PendingClientOperation.OperationType.Deposit) {
                        client.removePurseMoney(operation.getAmount());
                        account.deposit(operation.getAmount());
                    }
                    if (operation.getOperationType() == PendingClientOperation.OperationType.Withdrawal) {
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

    public Queue<PendingClientOperation> getQueue() {
        return queue;
    }

    public Lock getSupervisorLock() {
        return supervisorLock;
    }
}
