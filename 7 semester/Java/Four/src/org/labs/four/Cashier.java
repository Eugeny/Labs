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
                ClientOperation operation = bank.getQueue().remove();
                Thread.sleep(10);
                operation.execute(bank, logger);
            } catch (InterruptedException e) {
                break;
            }
        }
    }
}
