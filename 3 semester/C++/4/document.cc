#include "list.h"
#include "element.h"
#include <iostream>

using namespace std;


class Document : public LinkedList<Element*> {
public:
	void print() {
        LinkedListIter<Element*>* iter = this->iter();
        for (; !iter->end(); iter->next()) {
                iter->get()->print();
        }
	}	
};