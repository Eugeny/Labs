package org.labs.four;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public class Main {
    public static void main(String[] args) {
        Logger logger = new Logger(System.out);
        List<Client> clients = new ArrayList<Client>();
        Bank bank = new Bank();

        for (int i = 1; i <= 10; i++) {
            Client client = new Client("Client " + i);
            client.addPurseMoney(100);
            clients.add(client);
            bank.registerClient(client);
        }

        for (int i = 1; i <= 3; i++)
            new Cashier(bank, logger).start();

        new Supervisor(bank, logger).start();

        while (true) {
            Collections.shuffle(clients);

            bank.enqueueOperation(PendingClientOperation.createRandom(clients.get(0)));

            try {
                Thread.sleep(500);
            } catch (InterruptedException e) {
            }
        }
    }
}
