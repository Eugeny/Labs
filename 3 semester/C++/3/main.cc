#include <iostream>
#include "list.h"

using namespace std;

// Simple integer item
class Item {
	public:
		Item(int v) {
			value = v;
		}
		int value;
};

// Print list content
void dump(LinkedList<Item*>* lst) {
	LinkedListIter<Item*>* iter = lst->iter();
	for (; !iter->end(); iter->next()) {
		cout << iter->get()->value << " ";
	}
	cout << endl;
}

void odds(void* x) {
	int v = ((LinkedListEntry<Item*>*)x)->value->value;
	if (v % 2 == 1)
		cout << v << " ";
}

void evens(void* x) {
	int v = ((LinkedListEntry<Item*>*)x)->value->value;
	if (v % 2 == 0)
		cout << v << " ";
}

int main() {
	// Create list and insert items
	LinkedList<Item*>* l = new LinkedList<Item*>();
	cout << "5 -> 0\n";
	l->insert(new Item(5), 0);
	cout << "6 -> 1\n";
	l->insert(new Item(6), 1);
	cout << "9 -> 0\n";
	l->insert(new Item(9), 0);
	dump(l);

	// Print odd numbers
	cout << "Odd numbers: ";
	l->each(odds);
	cout << endl;

	// Print even numbers
	cout << "Even numbers: ";
	l->each(evens);
	cout << endl;

	// Remove one element
	cout << "del #1\n";
	l->remove(1);
	dump(l);

	cout << "del #0\n";
	// Remove another element
	l->remove(0);
	dump(l);

	// Clear list
	l->purge();
	delete l;
	return 0;
}