#ifndef _UTIL_H_
#define _UTIL_H_

#include <stdio.h>
#include "util/util.h"
#include "util/list.h"


class Util {
public:
    static int readInt(FILE* f);
    static void writeInt(FILE* f, int length);
    static char* readString(FILE* f);
    static void writeString(FILE* f, char* data);
    static int slen(char* s);
    static char* itoa(int i);
    
    template <class T>
    static List<T>* sorted(List<T>* o) {
        int l = o->length();
        int* used = new int[l];
        T* items = new T[l];
        
        for (int i = 0; i < l; i++) {
            items[i] = o->get(i);
            used[i] = 0;
        }
        
        List<T>* res = new List<T>();
        for (int i = 0; i < l; i++) {
            T max = 0;
            int maxidx = 0;
            for (int j = 0; j < l; j++)
                if (!used[j] && (!max || items[j]->gt(max))) {
                    max = items[j];
                    maxidx = j;
                }
            used[maxidx] = 1;
            res->add(max);
        }
        
        delete used;
        delete items;
        return res;
    }
};

#endif
