package org.labs.four;

import java.io.PrintStream;

public class Logger {
    private PrintStream stream;

    public Logger(PrintStream stream) {
        this.stream = stream;
    }

    public void logOperation(PendingClientOperation operation, boolean success) {
        stream.printf("%s: %s\n", operation.toString(), success ? "success" : "rejected");
    }

    public void logStatus(boolean good, int totalAmount) {
        stream.printf("--- Status: %s, total: %d\n", good ? "OK" : "error!", totalAmount);
    }
}
