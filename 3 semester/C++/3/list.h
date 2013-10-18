typedef void (*Iterator)(void*);


// Single item
template <class T>
class LinkedListEntry {
public:
    T value;
    LinkedListEntry<T>* next;
};


// Iterator
template <class T>
class LinkedListIter {
public:
    LinkedListIter<T>(LinkedListEntry<T>* l) {
        c = l;
        index = 0;
    }

    void next() {
        c = c->next;
        index++;
    }

    int end() {
        return (c == 0);
    }

    T get() {
        return c->value;
    }

    int index;
private:
    LinkedListEntry<T>* c;
};


// List itself
template <class T>
class LinkedList {
public:
    LinkedList<T>() {
        root = 0;
    }

    LinkedListEntry<T> getRoot() {
        return root;
    }

    LinkedListIter<T>* iter() {
        return new LinkedListIter<T>(root);
    }

    // Get element by index
    T get(int idx) {
        LinkedListEntry<T>* p = root;
        for (int i = 0; i < idx; i++)
            p = p->next;
        return p->value;
    }

    // List length
    int length() {
        LinkedListEntry<T>* p = root;
        int r = 0;
        for (; p; r++)
            p = p->next;
        return r;
    }

    // Find element's index
    int find(T val) {
        LinkedListEntry<T>* p = root;
        int r = 0;
        for (; p; r++) {
            if (p->value == val)
                return r;
            p = p->next;
        }
        return -1;
    }

    // Apply function to each element
    void each(Iterator f) {
        LinkedListEntry<T>* p = root;
        while (p) {
            f(p);
            p = p->next;
        }
    }

    // Insert element into place
    void insert(T val, int idx) {
        LinkedListEntry<T>* i = new LinkedListEntry<T>();
        i->value = val;
        i->next = 0;
        if (!root)
            root = i;
        else if (idx == 0) {
            i->next = root;
            root = i;
        } else {
            LinkedListEntry<T>* p = root;
            for (int ii = 0; ii < idx-1; ii++)
                p = p->next;
            LinkedListEntry<T>* tmp = p->next;
            p->next = i;
            i->next = tmp;
        }
    }

    // Remove element by index
    T remove(int idx) {
        if (idx < 0)
            return (T)0;

        T ret = (T)0;

        if (!root)
            return ret;
        else if (idx == 0) {
            ret = root->value;
            LinkedListEntry<T>* tmp = root;
            root = root->next;
            delete tmp;
        } else {
            LinkedListEntry<T>* p = root;
            for (int i = 0; i < idx-1; i++)
                p = p->next;

            LinkedListEntry<T>* tmp = p->next;
            p->next = p->next->next;
            ret = tmp->value;
            delete tmp;
        }
        return ret;
    }

    // Remove element by value
    T remove (T val) {
        return remove(find(val));
    }

    // Insert element to end
    void insertLast(T val) {
        insert(val, length());
    }

    void purge() {
        while (root) {
            delete root->value;
            LinkedListEntry<T>* t;
            t = root;
            root = root->next;
            delete t;
        }
    }

    ~LinkedList() {
        LinkedListEntry<T>* t;
        while (root) {
            t = root->next;
            delete root;
            root = t;
        }
    }

private:
    LinkedListEntry<T>* root;
};
