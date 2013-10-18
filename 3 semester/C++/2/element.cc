#include <element.h>
#include <iostream>

using namespace std;

Element::Element() { }
Element::~Element() { }
Element* Element::clone() { return NULL; }
void Element::print() { }
void Element::dump() { cout << "<Element>" << endl; }
