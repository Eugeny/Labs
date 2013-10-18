#include <headline.h>
#include <iostream>

using namespace std;

Headline::Headline(char* c) : Text(c) {
	// Pass the string to parent constructor.
}

void Headline::print() {
	cout << "=== " << text << " ===\n";
}

void Headline::dump() { 
	cout << "<Headline text=\"" << text << "\" />" << endl; 
}

Headline* Headline::clone() {
	return new Headline(text);
}