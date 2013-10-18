#include <iostream>
#include <document.h>

Document::Document() {
	// Init empty element set
	elc = 0;
	els = new Element*[256];
}

Document::Document(Document* orig) {
	// Clone all elements
	elc = orig->elc;
	els = new Element*[256];
	for (int i = 0; i < orig->elc; i++)
		els[i] = orig->els[i]->clone();
}

void Document::print() {
	for (int i = 0; i < elc; i++)
		els[i]->print();
}

void Document::dump() {
	for (int i = 0; i < elc; i++)
		els[i]->dump();
}

void Document::push(Element* e) {
	// Append new element to buffer
	els[elc++] = e;
}

Document::~Document() {
	// Remove all elements
	for (int i = 0; i < elc; i++)
		delete els[i];	
	// And dynamic array
	delete els;
}