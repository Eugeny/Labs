#ifndef LINE_H
#define LINE_H

#include <element.h>

class Line : public Element {
	public:
		Line();
		Line(int l);
		virtual Line* clone();
		virtual void print();	
		virtual void dump();
	private:
		int length;
};

#endif