#ifndef TEXT_H
#define TEXT_H

#include <iostream>
#include <element.h>

using namespace std;

class Text : public Element {
	public:
		Text(char*);
		virtual Text* clone();
		virtual void print();
		virtual void dump();

		// Sets new text content
		void setText(char* s);
	protected:
		char* text;
};

#endif