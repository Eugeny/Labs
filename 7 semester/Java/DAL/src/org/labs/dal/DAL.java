package org.labs.dal;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;
import javax.persistence.criteria.CriteriaBuilder;

public class DAL {
    private static EntityManagerFactory emFactory;

    private static EntityManagerFactory getEmf() {
        if (emFactory == null) {
            emFactory = Persistence.createEntityManagerFactory("org.labs.dal");
        }
        return emFactory;
    }

    public static EntityManager getEm() {
        return getEmf().createEntityManager();
    }

    public static CriteriaBuilder getCb() {
        return getEmf().getCriteriaBuilder();
    }
}
