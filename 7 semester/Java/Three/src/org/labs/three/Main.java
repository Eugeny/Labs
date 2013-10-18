package org.labs.three;

import java.io.FileInputStream;
import java.io.InputStream;
import java.util.*;

public class Main {
    public static void main(String[] args) {
        Map<Character, Integer> freq = new HashMap<Character, Integer>();
        int length = 0;
        try {
            InputStream is = new FileInputStream("Main.java");
            length = is.available();
            while (is.available() > 0) {
                char c = (char) is.read();
                if (!freq.containsKey(c))
                    freq.put(c, 1);
                else
                    freq.put(c, freq.get(c) + 1);
            }

            is.close();
        } catch (Exception e) {
            e.printStackTrace();
        }

        List<Character> keys = new ArrayList<Character>();
        keys.addAll(freq.keySet());
        Collections.sort(keys);
        for (char key : keys) {
            System.out.printf("%s: %.3f%%\n", key, freq.get(key) * 1f / length);
        }
    }
}
