<%@ page import="org.labs.dal.entities.Products" %>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ taglib uri="http://tiles.apache.org/tags-tiles" prefix="tiles" %>
<%@ taglib uri="/my.tld" prefix="my" %>
<%@ taglib tagdir="/WEB-INF/tags" prefix="tags" %>

<%
    pageContext.setAttribute("products", new Products().getAll());
%>

<tiles:insertTemplate template="/base.jsp">
    <tiles:putAttribute name="content">
        <h1>Products</h1>

        <table class="table">
            <my:iterate data="${products}">
                <tags:product data="${data}" />
            </my:iterate>
        </table>
    </tiles:putAttribute>
</tiles:insertTemplate>