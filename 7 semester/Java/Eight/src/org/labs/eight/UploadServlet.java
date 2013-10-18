package org.labs.eight;

import org.apache.commons.fileupload.FileItem;
import org.apache.commons.fileupload.disk.DiskFileItemFactory;
import org.apache.commons.fileupload.servlet.ServletFileUpload;
import org.labs.dal.entities.Product;
import org.labs.dal.entities.Products;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.InputStream;
import java.util.List;

public class UploadServlet extends HttpServlet {

    @Override
    public void doGet(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        response.getWriter().println("<html><body>" +
                "<form method='POST' enctype='multipart/form-data'><input type='file' name='file' />\n" +
                "<input type='submit' /></form>" +
                "</body></html>");
    }

    @Override
    protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        try {
            List<FileItem> items = new ServletFileUpload(new DiskFileItemFactory()).parseRequest(req);
            for (FileItem item : items) {
                InputStream content = item.getInputStream();
                for (Product product : new ProductReader().read(content)) {
                    new Products().create(product);
                }
            }
            resp.getWriter().println("OK");
        } catch (Exception e) {
            throw new ServletException(e);
        }
    }
}