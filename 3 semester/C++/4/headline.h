#ifndef HEADLINE_H
#define HEADLINE_H

#include <text.h>

class Headline : public Text {
	public:
		Headline(char*);
		virtual Headline* clone();
		virtual void print();
		virtual void dump();
};

#endif