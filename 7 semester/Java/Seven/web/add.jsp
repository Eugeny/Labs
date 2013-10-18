<%@ page import="org.labs.dal.entities.Product" %>
<%@ page import="org.labs.dal.entities.Products" %>
<%@ page contentType="text/html;charset=UTF-8" language="java" %><!DOCTYPE html>
<%@ taglib uri="http://tiles.apache.org/tags-tiles" prefix="tiles" %>

<%
    if (request.getMethod().equals("POST")) {
        Product product = new Product();
        product.setName(request.getParameter("name"));
        product.setPrice(Integer.parseInt(request.getParameter("price")));
        new Products().create(product);
        response.sendRedirect("index.jsp");
    }
%>

<tiles:insertTemplate template="/base.jsp">
    <tiles:putAttribute name="content">

        <form class="form-horizontal" method="POST">
            <div class="form-group">
                <label class="col-lg-2 control-label">Name</label>
                <div class="col-lg-4">
                    <input type="text" name="name" class="form-control" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-lg-2 control-label">Price</label>
                <div class="col-lg-4">
                    <input type="number" name="price" class="form-control" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-lg-2 control-label"></label>
                <div class="col-lg-4">
                    <input type="submit" class="btn btn-primary" value="Add" />
                </div>
            </div>
        </form>

    </tiles:putAttribute>
</tiles:insertTemplate>