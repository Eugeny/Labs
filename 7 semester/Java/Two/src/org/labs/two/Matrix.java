package org.labs.two;

import java.io.Serializable;
import java.util.AbstractList;

public class Matrix<A extends AbstractList<Float>> implements Serializable {
    private final A elements;
    private final Class<A> arrayClass;
    private final int width;
    private final int height;

    public Matrix(Class<A> arrayClass, int width, int height, float fill) {
        this.arrayClass = arrayClass;
        this.width = width;
        this.height = height;

        try {
            elements = arrayClass.newInstance();
        } catch (Exception e) {
            throw new RuntimeException(e);
        }

        for (int i = 0; i < width * height; i++)
            elements.add(fill);
    }

    private int getIndex(int x, int y) {
        return y * width + x;
    }

    public void set(int x, int y, float v) {
        elements.set(getIndex(x, y), v);
    }

    public float get(int x, int y) {
        return elements.get(getIndex(x, y));
    }

    public int getWidth() {
        return width;
    }

    public int getHeight() {
        return height;
    }

    public void multiplyBy(float v) {
        for (int i = 0; i < width * height; i++)
            elements.set(i, elements.get(i) * v);
    }

    @Override
    public boolean equals(Object obj) {
        Matrix<A> matrix = (Matrix<A>) obj;
        if (getWidth() != matrix.getWidth() || getHeight() != matrix.getHeight())
            return false;
        for (int x = 0; x < matrix.getWidth(); x++)
            for (int y = 0; y < matrix.getWidth(); y++)
                if (get(x, y) != matrix.get(x, y))
                    return false;
        return true;
    }
}
