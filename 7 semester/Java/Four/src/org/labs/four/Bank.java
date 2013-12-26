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
