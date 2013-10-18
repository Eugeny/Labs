package org.labs.seven.tags;

import javax.servlet.jsp.JspException;
import javax.servlet.jsp.tagext.SimpleTagSupport;
import java.io.IOException;
import java.io.StringWriter;
import java.util.Collection;

public class IteratorTag extends SimpleTagSupport {
    private Collection data;

    public void setData(Collection data) {
        this.data = data;
    }

    @Override
    public void doTag() throws JspException, IOException {
        StringWriter sw = new StringWriter();
        for (Object o : data) {
            getJspBody().getJspContext().setAttribute("data", o);
            getJspBody().invoke(sw);
        }

        getJspContext().getOut().write(sw.toString());
    }
}
