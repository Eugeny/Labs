package org.labs.dal.entities;

import org.labs.dal.DAL;

import javax.persistence.EntityManager;
import javax.persistence.criteria.CriteriaBuilder;
import javax.persistence.criteria.CriteriaQuery;
import java.util.List;

public class Products {
    private final CriteriaBuilder cb;
    private final EntityManager em;

    public Products() {
        em = DAL.getEm();
        cb = DAL.getCb();
    }

    public void create(Product product) {
        em.getTransaction().begin();
        em.persist(product);
        em.getTransaction().commit();
    }

    public void update(Product product) {
        em.getTransaction().begin();
        em.persist(product);
        em.getTransaction().commit();
    }

    public void delete(Product product) {
        em.getTransaction().begin();
        em.remove(product);
        em.getTransaction().commit();
    }

    public void delete(long id) {
        em.getTransaction().begin();
        em.remove(get(id));
        em.getTransaction().commit();
    }


    public Product get(long id) {
        CriteriaQuery<Product> cq = cb.createQuery(Product.class);
        return em.createQuery(cq.where(cb.equal(cq.from(Product.class).get(Product_.id), id))).getSingleResult();
    }

    public List<Product> getAll() {
        CriteriaQuery<Product> cq = cb.createQuery(Product.class);
        return em.createQuery(cq.select(cq.from(Product.class))).getResultList();
    }
}
