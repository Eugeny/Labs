package org.labs.two;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.AbstractList;

public class MatrixReader<A extends AbstractList<Float>> {
    private Class<A> clazz;
    private final InputStream stream;
    private final BufferedReader reader;

    public MatrixReader(Class<A> clazz, InputStream stream) {
        this.clazz = clazz;
        this.stream = stream;
        reader = new BufferedReader(new InputStreamReader(stream));
    }

    public Matrix<A> read() throws IOException {
        int w = Integer.parseInt(reader.readLine());
        int h = Integer.parseInt(reader.readLine());
        Matrix<A> matrix = new Matrix<A>(clazz, w, h, 0);

        for (int x = 0; x < matrix.getWidth(); x++)
            for (int y = 0; y < matrix.getWidth(); y++)
                matrix.set(x, y, Float.parseFloat(reader.readLine()));

        return matrix;
    }

    public void close() throws IOException {
        stream.close();
    }
}
