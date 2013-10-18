package org.labs.two;

import java.io.BufferedWriter;
import java.io.IOException;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.util.AbstractList;

public class MatrixWriter<A extends AbstractList<Float>> {
    private final OutputStream stream;
    private final BufferedWriter writer;

    public MatrixWriter(OutputStream stream) {
        this.stream = stream;
        writer = new BufferedWriter(new OutputStreamWriter(stream));
    }

    public void write(Matrix<A> matrix) throws IOException {
        writer.write(Integer.toString(matrix.getWidth()) + "\n");
        writer.write(Integer.toString(matrix.getHeight()) + "\n");

        for (int x = 0; x < matrix.getWidth(); x++)
            for (int y = 0; y < matrix.getWidth(); y++)
                writer.write(Float.toString(matrix.get(x, y)) + "\n");
    }

    public void close() throws IOException {
        writer.close();
    }
}
