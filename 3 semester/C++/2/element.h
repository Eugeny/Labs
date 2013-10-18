#ifndef ELEMENT_H
#define ELEMENT_H

class Element {
	public:
		Element();
		virtual ~Element();

		// Returns Element instance with same content as original
		virtual Element* clone();

		// Prints Element's visual representation to stdout
		virtual void print();

		// Dumps Element's properties to stdout
		virtual void dump();
};

#endif