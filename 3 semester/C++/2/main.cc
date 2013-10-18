#include <iostream>
#include <element.h>
#include <headline.h>
#include <document.h>
#include <line.h>
#include <text.h>

using namespace std;

int main() {
	Document* d = new Document();
	d->push(new Headline("Header"));
	d->push(new Text("Lorem ipsum"));
	d->push(new Text("dolor sit amet."));
	d->push(new Line());
	d->push(new Text("More text"));
	d->push(new Text("here"));

	cout << "Output:\n";

	d->print();

	cout << "\nDump:\n";

	d->dump();
	
	return 0;
}