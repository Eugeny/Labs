package org.labs.two;

import junit.framework.TestCase;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.junit.runners.JUnit4;

import java.io.*;
import java.util.ArrayList;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertTrue;

@RunWith(JUnit4.class)
public class TestMatrix extends TestCase {

    @Test
    public void testCreation() {
        Matrix m = new Matrix(ArrayList.class, 2, 2, 1);
        assertEquals(m.get(0, 0), 1, 0.01f);
        assertEquals(m.get(0, 1), 1, 0.01f);
        assertEquals(m.get(1, 0), 1, 0.01f);
        assertEquals(m.get(1, 1), 1, 0.01f);
    }

    @Test
    public void testMultiplication() {
        Matrix m = new Matrix(ArrayList.class, 2, 2, 1);
        m.multiplyBy(2);
        assertEquals(m.get(0, 0), 2, 0.01f);
        assertEquals(m.get(0, 1), 2, 0.01f);
        assertEquals(m.get(1, 0), 2, 0.01f);
        assertEquals(m.get(1, 1), 2, 0.01f);
    }

    @Test
    public void testIO () throws IOException {
        Matrix m = new Matrix(ArrayList.class, 2, 2, 1);
        m.set(0,1,2);
        m.set(1,0,3);

        ByteArrayOutputStream baos = new ByteArrayOutputStream();
        MatrixWriter<ArrayList<Float>> writer = new MatrixWriter<ArrayList<Float>>(baos);
        writer.write(m);
        writer.close();

        ByteArrayInputStream bais = new ByteArrayInputStream(baos.toByteArray());
        MatrixReader<ArrayList<Float>> reader = new MatrixReader(ArrayList.class, bais);
        Matrix m2 = reader.read();

        assertTrue(m2.equals(m));
    }


    @Test
    public void testSerialization () throws IOException, ClassNotFoundException {
        Matrix m = new Matrix(ArrayList.class, 2, 2, 1);
        m.set(0,1,2);
        m.set(1,0,3);

        ByteArrayOutputStream baos = new ByteArrayOutputStream();
        ObjectOutputStream oos = new ObjectOutputStream(baos);
        oos.writeObject(m);
        oos.close();

        ByteArrayInputStream bais = new ByteArrayInputStream(baos.toByteArray());
        ObjectInputStream ois = new ObjectInputStream(bais);
        Matrix m2 = (Matrix)ois.readObject();

        assertTrue(m2.equals(m));
    }
}