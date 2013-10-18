package org.labs.four;


public class Cashier extends Thread {
    private Bank bank;
    private Logger logger;

    public Cashier(Bank bank, Logger logger) {
        this.bank = bank;
        this.logger = logger;
    }

    @Override
    public void run() {
        while (true) {
            try {
                synchronized (bank.getQueue()) {
                    bank.getQueue().wait();
                }
                PendingClientOperation operation = bank.getQueue().remove();
                Thread.sleep(1000);
                boolean success = bank.performOperation(operation);
                logger.logOperation(operation, success);
            } catch (InterruptedException e) {
                break;
            }
        }
    }
}
