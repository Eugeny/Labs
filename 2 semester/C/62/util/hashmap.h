#ifndef _HASHMAP_H_
#define _HASHMAP_H_

#include "util/pair.h"
#include "util/list.h"
#include "util/comparable.h"
#include <typeinfo>
//#include <iostream>
#include <string.h>

template <class K, class V>
class HashMap {
    public:
        HashMap() {
            _list = new List<Pair<K,V>*>();
        }
        
        virtual ~HashMap() {
            delete _list;
        }
                
        V get(K key) {
            int idx = getIdx(key);
            if (idx == -1)
                return 0;
            Pair<K,V>* c = _list->get(idx);
            if (c)
                return c->getB();
                
            return 0;
        }
        
        ListItem<Pair<K,V>*>* head() {
            return _list->head();
        }
        
        int length() {
            return _list->length();
        }
        
        K getKeyByIndex(int idx) {
            ListItem<Pair<K,V>*>* h = head();
            for (int i = 0; i < idx; i++)
                h = h->next;
            return h->get()->getA();
        }

        V getByIndex(int idx) {
            ListItem<Pair<K,V>*>* h = head();
            for (int i = 0; i < idx; i++)
                h = h->next;
            return h->get()->getB();
        }
        
        void put(K key, V val) {
            Pair<K,V>* p = new Pair<K,V>(key,val);
            _list->add(p);
        }
        
        void remove(K key) {    
            int idx = getIdx(key);
            if (idx == -1)
                return;
            _list->remove(idx);
        }
        
        void clear() {
            while (head() != 0)
                _list->remove(0);
        }

    private:
        List<Pair<K,V>*>* _list;
 
        int keyIsClass() {
           return strlen(typeid(K).name()) > 3;
        }
        
        int getIdx(K key) {
            ListItem<Pair<K,V>*>* c = _list->head();
            int isClass = keyIsClass();
            int idx = 0;
            while (c) {
                if (isClass) {
                    if (*((Comparable*)(c->get()->getA())) == *((Comparable*)key))
                        break;
                } else
                    if (c->get()->getA() == key)
                        break;
                idx++;
                c = c->next;
            }
            if (!c)
                return -1;
            return idx;
        }
};

#endif
