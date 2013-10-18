#ifndef _LISTITEM_H_
#define _LISTITEM_H_

template <class T>
class ListItem {
    public:
        ListItem* next;
        
        ListItem() {
            next = 0;
        }
        
        T get() {
            return _value;
        }
        
        void set(T val) {
            _value = val;
        }
    private:
        T _value;
};

#endif

