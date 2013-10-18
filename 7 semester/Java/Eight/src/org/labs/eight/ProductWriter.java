package org.labs.eight;

import org.labs.dal.entities.Product;
import org.w3c.dom.Document;
import org.w3c.dom.Element;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;
import javax.xml.transform.TransformerException;
import javax.xml.transform.TransformerFactory;
import javax.xml.transform.dom.DOMSource;
import javax.xml.transform.stream.StreamResult;
import java.io.StringWriter;
import java.util.List;

public class ProductWriter {
    public String write(List<Product> products) throws ParserConfigurationException, TransformerException {
        DocumentBuilder d = DocumentBuilderFactory.newInstance().newDocumentBuilder();
        Document document = d.newDocument();

        Element root = document.createElement("products");

        for (Product product : products) {
            Element node = document.createElement("product");
            node.setAttribute("name", product.getName());
            node.setAttribute("price", Integer.toString(product.getPrice()));
            root.appendChild(node);
        }

        StringWriter sw = new StringWriter();
        StreamResult sr = new StreamResult(sw);

        TransformerFactory.newInstance().newTransformer().transform(new DOMSource(root), sr);
        return sw.toString();
    }
}
