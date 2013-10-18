package org.labs.six;

import org.labs.dal.entities.Product;
import org.labs.dal.entities.Products;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;

public class ShopServlet extends HttpServlet {
    private static String productTemplate = "<b>%s</b> (%d) <a href='?delete=%d'>delete</a><br/>";
    private static String template = "<!DOCTYPE html>" +
            "<html>" +
            "<body>" +
            "<h1>Products</h1>" +
            "%s" +
            "<h1>Create product</h1>" +
            "<form method='POST'>" +
            "<label>Name</label><br/>" +
            "<input type='text' name='name' />" +
            "<br/>" +
            "<label>Price</label><br/>" +
            "<input type='text' name='price' />" +
            "<br/>" +
            "<input type='submit'/>" +
            "</form>" +
            "</body>" +
            "</html>";

    @Override
    public void doGet(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {

        if (request.getParameter("delete") != null) {
            long id = Long.parseLong(request.getParameter("delete"));
            new Products().delete(id);
        }

        StringBuilder productsList = new StringBuilder();
        for (Product product : new Products().getAll())
            productsList.append(String.format(productTemplate, product.getName(), product.getPrice(), product.getId()));

        PrintWriter out = response.getWriter();
        out.println(String.format(template, productsList));
    }

    @Override
    protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        Product product = new Product();
        product.setName(req.getParameter("name"));
        product.setPrice(Integer.parseInt(req.getParameter("price")));
        new Products().create(product);
        doGet(req, resp);
    }
}