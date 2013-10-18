package org.labs.eight;

import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpServletResponseWrapper;
import java.io.CharArrayWriter;
import java.io.PrintWriter;

/**
 * Wraps the response object to capture the text written to it.
 */
public class CharResponseWrapper extends HttpServletResponseWrapper {
    private CharArrayWriter output;

    public CharResponseWrapper(HttpServletResponse response) {
        super(response);
        this.output = new CharArrayWriter();
    }

    public String toString() {
        return output.toString();
    }

    public PrintWriter getWriter() {
        return new PrintWriter(output);
    }
}