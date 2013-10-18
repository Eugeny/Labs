<%@ tag body-content="empty" %>
<%@ attribute name="data" required="true" type="org.labs.dal.entities.Product" rtexprvalue="true" %>

<a href="delete.jsp?id=${data.id}" class="btn btn-danger">Delete</a>
