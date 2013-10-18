package org.labs.eight;

import javax.servlet.*;
import javax.servlet.http.HttpServletResponse;
import javax.xml.transform.Source;
import javax.xml.transform.Transformer;
import javax.xml.transform.TransformerFactory;
import javax.xml.transform.stream.StreamResult;
import javax.xml.transform.stream.StreamSource;
import java.io.IOException;
import java.io.StringReader;
import java.io.StringWriter;

public class XSLTFilter implements Filter {
    private String XSLT = "<?xml version=\"1.0\"?>\n" +
            "<xsl:stylesheet version=\"1.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\">" +
            "<xsl:template match=\"products\">" +
            "  <html>" +
            "  <body>" +
            "    <table>" +
            "      <tr>" +
            "        <th>Name</th>" +
            "        <th>Price</th>" +
            "      </tr>" +
            "      <xsl:for-each select=\"product\">" +
            "        <tr>" +
            "          <td><xsl:value-of select=\"@name\"/></td>" +
            "          <td><xsl:value-of select=\"@price\"/></td>" +
            "        </tr>" +
            "      </xsl:for-each>" +
            "    </table>" +
            "  </body>" +
            "  </html>" +
            "</xsl:template>" +
            "</xsl:stylesheet>";

    @Override
    public void init(FilterConfig filterConfig) throws ServletException {
    }

    @Override
    public void doFilter(ServletRequest servletRequest, ServletResponse servletResponse, FilterChain filterChain) throws IOException, ServletException {
        CharResponseWrapper wrapper = new CharResponseWrapper((HttpServletResponse) servletResponse);
        filterChain.doFilter(servletRequest, wrapper);

        try {
            Source xslt = new StreamSource(new StringReader(XSLT));
            Transformer transformer = TransformerFactory.newInstance().newTransformer(xslt);
            Source text = new StreamSource(new StringReader(wrapper.toString()));
            StringWriter sw = new StringWriter();
            transformer.transform(text, new StreamResult(sw));
            servletResponse.getWriter().print(sw.toString());
        } catch (Exception e) {
            throw new ServletException(e);
        }
    }

    @Override
    public void destroy() {
    }
}
