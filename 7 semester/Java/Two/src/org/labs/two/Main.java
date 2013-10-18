package org.labs.two;

import java.util.AbstractList;
import java.util.ArrayList;
import java.util.LinkedList;

public class Main {
    public static <A extends AbstractList<Float>> void Benchmark(Class<A> clazz, String title) {
        long timeStart = System.currentTimeMillis();
        Matrix<A> matrix = new Matrix<A>(clazz, 100, 100, 1);
        for (int i = 0; i < 10; i++)
            matrix.multiplyBy(10);
        long timeEnd = System.currentTimeMillis();
        System.out.printf("%s:\nTime per operation: %.3f ms\n", title, (timeEnd - timeStart) / 10f);
    }

    public static void main(String[] args) {
        Benchmark(ArrayList.class, "ArrayList");
        Benchmark(LinkedList.class, "LinkedList");
    }
}
