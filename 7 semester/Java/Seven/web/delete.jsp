<%@ page import="org.labs.dal.entities.Products" %>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>

<%
    new Products().delete(Long.parseLong(request.getParameter("id")));
    response.sendRedirect("index.jsp");
%>