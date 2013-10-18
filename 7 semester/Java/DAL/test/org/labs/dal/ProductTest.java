package org.labs.dal;

import org.junit.Test;
import org.junit.runner.RunWith;
import org.junit.runners.JUnit4;
import org.labs.dal.entities.Product;
import org.labs.dal.entities.Products;

import static org.junit.Assert.assertEquals;

@RunWith(JUnit4.class)
public class ProductTest {
    @Test
    public void testDAO() throws Exception {
        Products dao = new Products();
        Product product = new Product();
        product.setName("A");
        product.setPrice(1);

        int oldCount = dao.getAll().size();

        dao.create(product);

        assertEquals(dao.getAll().size(), oldCount + 1);
        assertEquals(dao.get(product.getId()).getName(), product.getName());

        product.setName("B");

        dao.update(product);

        assertEquals(dao.get(product.getId()).getName(), product.getName());

        dao.delete(product);
        assertEquals(dao.getAll().size(), oldCount);
    }
}
