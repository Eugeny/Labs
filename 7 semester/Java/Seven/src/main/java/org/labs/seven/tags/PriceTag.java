package org.labs.seven.tags;

import javax.servlet.jsp.JspException;
import javax.servlet.jsp.tagext.SimpleTagSupport;
import java.io.IOException;

public class PriceTag extends SimpleTagSupport {
    private int price;

    public void setPrice(int price) {
        this.price = price;
    }

    @Override
    public void doTag() throws JspException, IOException {
        getJspContext().getOut().print(String.format("<span class='badge badge-success'>%d</span>", price));
    }
}
