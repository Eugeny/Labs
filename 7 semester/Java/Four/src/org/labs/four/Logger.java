package org.labs.four;

import java.io.PrintStream;

public class Logger {
    private PrintStream stream;

    public Logger(PrintStream stream) {
        this.stream = stream;
    }

    public void logOperation(ClientOperation operation, String status) {
        stream.printf("%s: %s\n", operation.toString(), status);
    }

    public void logStatus(boolean good, int totalAmount) {
        stream.printf("--- Status: %s, total: %d\n", good ? "OK" : "error!", totalAmount);
        if (!good)
            System.exit(1);
    }
}
