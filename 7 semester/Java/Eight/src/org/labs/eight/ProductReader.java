package org.labs.eight;

import org.labs.dal.entities.Product;
import org.xml.sax.Attributes;
import org.xml.sax.SAXException;
import org.xml.sax.helpers.DefaultHandler;

import javax.xml.parsers.ParserConfigurationException;
import javax.xml.parsers.SAXParser;
import javax.xml.parsers.SAXParserFactory;
import java.io.IOException;
import java.io.InputStream;
import java.util.ArrayList;
import java.util.List;

public class ProductReader {
    public List<Product> read(InputStream stream) throws ParserConfigurationException, SAXException, IOException {
        SAXParser parser = SAXParserFactory.newInstance().newSAXParser();
        final List<Product> products = new ArrayList<Product>();
        DefaultHandler handler = new DefaultHandler() {
            @Override
            public void startElement(String uri, String localName, String qName, Attributes attributes) throws SAXException {
                if (qName.equals("product")) {
                    Product product = new Product();
                    product.setName(attributes.getValue("name"));
                    product.setPrice(Integer.parseInt(attributes.getValue("price")));
                    products.add(product);
                }
            }
        };

        parser.parse(stream, handler);
        return products;
    }
}
