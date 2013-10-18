#include <iostream>
#include <document.cc>
#include <fstream>
#include "stdio.h"
#include "line.h"
#include "headline.h"
#include "text.h"

using namespace std;

int main() {
	cout << "Writing to log.txt\n";

	freopen("log.txt", "w", stdout);

    Document* d = new Document();
    (*d) += new Headline("Header");
    (*d) += new Text("Lorem ipsum");
    (*d) += new Text("dolor sit amet.");
    (*d) += new Line();
    (*d) += new Text("More text");
    (*d) += new Text("here");

    cout << "Document:\n";

    d->print();

    cout << "\nRemoving all text\n";

    d->remove(1);
    d->remove(1);
    d->remove(2);
    d->remove(2);

    d->print();

    cout << "\nElement #1 is: \n";
    (*d)[0]->print();

    fclose(stdout);
}