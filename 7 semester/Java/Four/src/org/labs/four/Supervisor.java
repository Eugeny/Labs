package org.labs.four;

public class Supervisor extends Thread {
    private Bank bank;
    private Logger logger;

    public Supervisor(Bank bank, Logger logger) {
        this.bank = bank;
        this.logger = logger;
    }

    private int getTotalAmount() {
        bank.getSupervisorLock().lock();
        int total = 0;
        for (Account account : bank.getAccounts())
            total += account.getFunds();
        for (Client client : bank.getClients())
            total += client.getPurse();
        bank.getSupervisorLock().unlock();
        return total;
    }

    @Override
    public void run() {
        int amount = getTotalAmount();
        while (true) {
            try {
                Thread.sleep(1000);
            } catch (InterruptedException e) {
                break;
            }

            int newAmount = getTotalAmount();
            logger.logStatus(newAmount == amount, newAmount);
        }
    }
}
