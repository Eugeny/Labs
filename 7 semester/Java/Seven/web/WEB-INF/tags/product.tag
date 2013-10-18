<%@ tag %>
<%@ attribute name="data" required="true" type="org.labs.dal.entities.Product" rtexprvalue="true" %>
<%@ taglib uri="/my.tld" prefix="my" %>
<%@ taglib tagdir="/WEB-INF/tags" prefix="tags" %>

<tr>
    <td>
        ${data.name}
    </td>
    <td>
        <my:priceTag price="${data.price}"/>
    </td>
    <td>
        <tags:delete data="${data}" />
    </td>
</tr>