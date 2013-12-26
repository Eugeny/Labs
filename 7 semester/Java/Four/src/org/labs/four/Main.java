package org.labs.four;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public class Main {
    public static void main(String[] args) {
        Logger logger = new Logger(System.out);
        List<Client> clients = new ArrayList<Client>();
        Bank bank = new Bank();

        for (int i = 1; i <= 3; i++) {
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

            bank.enqueueOperation(ClientOperation.createRandom(clients.get(0), clients.get(1)));

            try {
                Thread.sleep(10);
            } catch (InterruptedException e) {
            }
        }
    }
}
