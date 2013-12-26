package org.labs.four;

public class Supervisor extends Thread {
    private Bank bank;
    private Logger logger;

    public Supervisor(Bank bank, Logger logger) {
        this.bank = bank;
        this.logger = logger;
    }

    @Override
    public void run() {
        Integer amount = null;
        while (true) {
            for (Client client : bank.getClients())
                client.setStatus(Client.VerificationStatus.VERIFICATION_PENDING);

            int newAmount = 0;
            for (Client client : bank.getClients()) {
                client.setStatus(Client.VerificationStatus.VERIFIED);
                newAmount += client.getPurse() + bank.getAccount(client).getFunds();

                // Delay
                try {
                    Thread.sleep(50);
                } catch (InterruptedException e) {
                    break;
                }
            }

            for (Client client : bank.getClients())
                client.setStatus(Client.VerificationStatus.NONE);

            if (amount != null)
                logger.logStatus(newAmount == amount, newAmount);

            amount = newAmount;
        }
    }
}
