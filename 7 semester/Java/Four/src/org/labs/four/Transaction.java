package org.labs.four;

import java.util.ArrayList;
import java.util.List;

public class Transaction {
    private List<Object> resources = new ArrayList<Object>();

    public void addResource(Object resource) {
        resources.add(resource);
    }

    public List<Object> getResources() {
        return resources;
    }
}
