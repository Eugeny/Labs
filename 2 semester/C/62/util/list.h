#ifndef _LIST_H_
#define _LIST_H_

#include "util/listitem.h"
//#include <iostream>
#include <stdlib.h>


template <class T>
class List {
    public:
        List() {
            _head = 0;
        }
        
        ListItem<T>* head() {
            return _head;
        }
                
        virtual ~List() {
            if (!_head)
                return;
                
            ListItem<T>* t = _head;
            ListItem<T>* x;
            while (t) {
                x = t;
                t = t->next;
                delete x;
            }
        }
        
        void add(T val) {
            ListItem<T>* li = new ListItem<T>();
            li->set(val);

            if (!_head)
                _head = li;
            else
                end()->next = li;
        }
        
        T get(int idx) {
            if (!_head)
                return 0;
            ListItem<T>* c = _head;
            for (int i = 0; i < idx; i++) 
                c = c->next;
            return c->get();
        }
        
        void remove(int idx) {
            if (!_head)
                return;
                
            ListItem<T>* t;
            if (idx == 0) {
                t = _head;
                _head = _head->next;
                free(t);
                return;
            }

            ListItem<T>* c = _head;
            for (int i = 0; i < idx-1; i++) 
                c = c->next;
            
            t = c->next;
            c->next = c->next->next;
            free(t);
        }



        int length() {
            if (!_head)
                return 0;
            
            int r = 1;
            ListItem<T>* c = _head;
            while (c->next) {
                r++;
                c = c->next;
            }
                
            return r;
        }
        
        ListItem<T>* end() {
            if (!_head)
                return 0;
            
            ListItem<T>* c = _head;
            while (c->next) 
                c = c->next;
                
            return c;
        }
        
    private:
        ListItem<T>* _head;
};

#endif
