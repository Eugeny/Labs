package org.labs.four;

public class Client {
    private int purse;

    private String name;
    private VerificationStatus status;

    public Client(String name) {
        this.name = name;
        this.status = VerificationStatus.NONE;
    }

    public void addPurseMoney(int v) {
        purse += v;
    }

    public void removePurseMoney(int v) {
        purse -= v;
    }

    public int getPurse() {
        return purse;
    }

    public String getName() {
        return name;
    }

    public VerificationStatus getStatus() {
        return status;
    }

    public void setStatus(VerificationStatus status) {
        this.status = status;
    }

    public enum VerificationStatus {
        NONE,
        VERIFIED,
        VERIFICATION_PENDING
    }
}
