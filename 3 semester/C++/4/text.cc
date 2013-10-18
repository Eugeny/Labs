#include <text.h>
#include <iostream>

using namespace std;

Text::Text(char* t) {
	// Save the text
	setText(t);
}

void Text::print() {
	cout << text << endl;
}

void Text::setText(char *s) {
	text = s;
}

void Text::dump() { 
	cout << "<Text text=\"" << text << "\" />" << endl; 
}

Text* Text::clone() {
	return new Text(text);
}